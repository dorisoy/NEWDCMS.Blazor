using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DCMS.Domain
{
	/// <summary>
	/// 单据抽象基类
	/// </summary>
	public abstract class BaseBillEntity<TId, Item> : IBaseBillEntity<TId, Item> where Item : IEntity
	{
		/// <summary>
		/// 单据明细
		/// </summary>
		private ICollection<Item> _Items;


		public TId Id { get; set; }

		/// <summary>
		/// 租户标识
		/// </summary>
		public int StoreId { get; set; }

		/// <summary>
		/// 单号
		/// </summary>
		public string BillNumber { get; set; }

		/// <summary>
		/// 单据备注
		/// </summary>
		public string Remark { get; set; }


		/// <summary>
		/// 开单人
		/// </summary>
		public int MakeUserId { get; set; }

		/// <summary>
		/// 单据类型
		/// </summary>
		public int? BillTypeId { get; set; }

		/// <summary>
		/// 开单人名称
		/// </summary>
		public string CreatedBy { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatedOn { get; set; }

		/// <summary>
		/// 修改人
		/// </summary>
		public string LastModifiedBy { get; set; }

		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime? LastModifiedOn { get; set; }

		/// <summary>
		/// 审核人
		/// </summary>
		public int? AuditedUserId { get; set; } = 0;

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
		public int? ReversedUserId { get; set; } = 0;

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
		public bool Deleted { get; set; } = false;

		/// <summary>
		/// 生成单号
		/// </summary>
		/// <returns></returns>
		public string GenerateNumber()
		{
			//var number = CommonHelper.GetBillNumber(CommonHelper.GetEnumDescription(BillType).Split(',')[1], StoreId);
			var number = "";
			BillNumber = number;
			return number;
		}

		public BillTypeEnum BillType
		{
			get { return (BillTypeEnum)BillTypeId; }
			set { BillTypeId = (int)value; }
		}

		/// <summary>
		/// (导航)项目
		/// </summary>
		public virtual ICollection<Item> Items
		{
			get { return _Items ?? (_Items = new List<Item>()); }
			protected set { _Items = value; }
		}

		
	}


	/// <summary>
	/// 单据抽象基类
	/// </summary>
	public abstract class BaseBillEntity<TId> : IBaseBillEntity<TId> 
	{


		public TId Id { get; set; }

		/// <summary>
		/// 租户标识
		/// </summary>
		public int StoreId { get; set; }

		/// <summary>
		/// 单号
		/// </summary>
		public string BillNumber { get; set; }

		/// <summary>
		/// 单据备注
		/// </summary>
		public string Remark { get; set; }


		/// <summary>
		/// 开单人
		/// </summary>
		public int MakeUserId { get; set; }

		/// <summary>
		/// 单据类型
		/// </summary>
		public int? BillTypeId { get; set; }

		/// <summary>
		/// 开单人名称
		/// </summary>
		public string CreatedBy { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatedOn { get; set; }

		/// <summary>
		/// 修改人
		/// </summary>
		public string LastModifiedBy { get; set; }

		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime? LastModifiedOn { get; set; }

		/// <summary>
		/// 审核人
		/// </summary>
		public int? AuditedUserId { get; set; } = 0;

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
		public int? ReversedUserId { get; set; } = 0;

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
		public bool Deleted { get; set; } = false;

		/// <summary>
		/// 生成单号
		/// </summary>
		/// <returns></returns>
		public string GenerateNumber()
		{
			//var number = CommonHelper.GetBillNumber(CommonHelper.GetEnumDescription(BillType).Split(',')[1], StoreId);
			var number = "";
			BillNumber = number;
			return number;
		}

		public BillTypeEnum BillType
		{
			get { return (BillTypeEnum)BillTypeId; }
			set { BillTypeId = (int)value; }
		}

	}
}




