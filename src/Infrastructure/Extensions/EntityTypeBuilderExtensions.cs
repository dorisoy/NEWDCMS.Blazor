using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DCMS.Infrastructure
{

	/// <summary>
	/// 自定义查询过滤器扩展
	/// </summary>
	public static class EntityTypeBuilderExtensions
	{

		/// <summary>
		/// 查询过滤器
		/// if (typeof(ITrackSoftDelete).IsAssignableFrom(entityType.ClrType))
		//modelBuilder.Entity(entityType.ClrType).AddQueryFilter<ITrackSoftDelete>(e => IsSoftDeleteFilterEnabled == false || e.IsDeleted == false);
		/// if (typeof(ITrackTenant).IsAssignableFrom(entityType.ClrType))
		//modelBuilder.Entity(entityType.ClrType).AddQueryFilter<ITrackTenant>(e => e.TenantId == MyTenantId);
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entityTypeBuilder"></param>
		/// <param name="expression"></param>
		internal static void AddQueryFilter<T>(this EntityTypeBuilder entityTypeBuilder, Expression<Func<T, bool>> expression)
		{
			var parameterType = Expression.Parameter(entityTypeBuilder.Metadata.ClrType);
			var expressionFilter = ReplacingExpressionVisitor.Replace(
				expression.Parameters.Single(), parameterType, expression.Body);

			var currentQueryFilter = entityTypeBuilder.Metadata.GetQueryFilter();
			if (currentQueryFilter != null)
			{
				var currentExpressionFilter = ReplacingExpressionVisitor.Replace(
					currentQueryFilter.Parameters.Single(), parameterType, currentQueryFilter.Body);
				expressionFilter = Expression.AndAlso(currentExpressionFilter, expressionFilter);
			}

			var lambdaExpression = Expression.Lambda(expressionFilter, parameterType);
			entityTypeBuilder.HasQueryFilter(lambdaExpression);
		}
	}
}
