using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DCMS.Domain
{

	/// <summary>
	/// 用于表示单据
	/// </summary>
	/// <typeparam name="TId"></typeparam>
	/// <typeparam name="Item"></typeparam>
	public interface IBaseBillEntity<TId, Item> : IBaseBillEntity<TId>, IEntity<TId>
	{
		/// <summary>
		/// 单据编号
		/// </summary>
		public string BillNumber { get; set; }

		/// <summary>
		/// 制单人
		/// </summary>
		public int MakeUserId { get; set; }

		/// <summary>
		/// 审核人
		/// </summary>
		public int? AuditedUserId { get; set; }

		/// <summary>
		/// 状态(审核)
		/// </summary>
		[Column(TypeName = "BIT(1)")]
		public bool AuditedStatus { get; set; }

		/// <summary>
		/// 审核时间
		/// </summary>
		public DateTime? AuditedDate { get; set; }

		/// <summary>
		/// 红冲人
		/// </summary>
		public int? ReversedUserId { get; set; } 

		/// <summary>
		/// 红冲状态
		/// </summary>
		[Column(TypeName = "BIT(1)")]
		public bool ReversedStatus { get; set; }

		/// <summary>
		/// 红冲时间
		/// </summary>
		public DateTime? ReversedDate { get; set; }

		/// <summary>
		/// 删除标志位
		/// </summary>
		[Column(TypeName = "BIT(1)")]
		public bool Deleted { get; set; } 

		/// <summary>
		/// 单据类型
		/// </summary>
		public int? BillTypeId { get; set; }
	

	}
	public interface IBaseBillEntity<TId> : IAuditableEntity<TId>
	{
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark { get; set; }

	}


	/// <summary>
	/// 用于表示单据明细
	/// </summary>
	/// <typeparam name="TId"></typeparam>
	public interface IBaseItemEntity<TId> : IEntity<TId>
	{
		/// <summary>
		/// 租户标识
		/// </summary>
		int StoreId { get; set; }

		/// <summary>
		/// 单据ID
		/// </summary>
		int BillId { get; set; }

		/// <summary>
		/// 明细备注
		/// </summary>
		public string Remark { get; set; }

	}
}
