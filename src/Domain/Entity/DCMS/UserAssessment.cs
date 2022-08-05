using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DCMS.Domain.Main
{

	/// <summary>
	/// 业务员考核目标
	/// </summary>
	public class UserAssessment : AuditableEntity<int>
	{
		public int Year { get; set; }
		public string Name { get; set; }
	}

	/// <summary>
	/// 业务员考核目标项
	/// </summary>
	public class UserAssessmentsItems : AuditableEntity<int>
	{
		public int UserId { get; set; }

		public string UserName { get; set; }
		public int AssessmentId { get; set; }

		public decimal? Jan { get; set; }

		public decimal? Feb { get; set; }

		public decimal? Mar { get; set; }

		public decimal? Apr { get; set; }

		public decimal? May { get; set; }

		public decimal? Jun { get; set; }

		public decimal? Jul { get; set; }
 
		public decimal? Aug { get; set; }

		public decimal? Sep { get; set; }

		public decimal? Oct { get; set; }

		public decimal? Nov { get; set; }

		public decimal? Dec { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime CreatedOnUtc { get; set; }
		/// <summary>
		/// 是否启用
		/// </summary>
		[Column(TypeName = "BIT(1)")]
		public bool Active { get; set; }
	}
}
