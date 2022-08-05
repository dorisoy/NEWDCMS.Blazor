using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;

namespace DCMS.Domain
{

	/// <summary>
	/// 标记索引
	/// </summary>
	/// <typeparam name="TId"></typeparam>
	[Index(nameof(StoreId))]

	public abstract class AuditableEntity<TId> : IAuditableEntity<TId>
	{
		[Key]
		public TId Id { get; set; }

		/// <summary>
		/// 租户ID 
		/// </summary>
		[Required]
		public int StoreId { get; set; }

		/// <summary>
		/// 创建人
		/// </summary>
		public string CreatedBy { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatedOn { get; set; }

		/// <summary>
		/// 最后修改人
		/// </summary>
		public string LastModifiedBy { get; set; }

		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime? LastModifiedOn { get; set; }
	}
}