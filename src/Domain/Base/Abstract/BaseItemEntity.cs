using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DCMS.Domain
{
	/// <summary>
	/// 单据项目抽象基类
	/// </summary>
	public abstract class BaseItemEntity<TId> : IBaseItemEntity<TId>
	{
		public TId Id { get; set; }

		/// <summary>
		/// 主单据ID
		/// </summary>
		public int BillId { get; set; }

		/// <summary>
		/// 租户标识
		/// </summary>
		public int StoreId { get; set; }

		/// <summary>
		/// 明细备注
		/// </summary>
		public string Remark { get; set; }


		/// <summary>
		/// 单位
		/// </summary>
		public int UnitId { get; set; }

		/// <summary>
		/// 商品Id
		/// </summary>
		public int ProductId { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		public int Quantity { get; set; }
		/// <summary>
		/// 价格
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// 金额
		/// </summary>
		public decimal Amount { get; set; }

		/// <summary>
		/// 利润
		/// </summary>
		public decimal Profit { get; set; }

		/// <summary>
		/// 成本价(历史成本价)
		/// </summary>
		public decimal CostPrice { get; set; }


		//(注意：结转成本后，已审核业务单据中的成本价，将会被替换成结转后的全月平均价!)
		/// <summary>
		/// 成本金额
		/// </summary>
		public decimal CostAmount { get; set; }

		/// <summary>
		/// 成本利润率 =利润/成本费用×100%
		/// </summary>
		public decimal CostProfitRate { get; set; }

		/// <summary>
		/// 是否赠品 2019-07-24
		/// </summary>
		[Column(TypeName = "BIT(1)")]
		public bool IsGifts { get; set; } = false;

		/// <summary>
		/// 赠品类型
		/// </summary>
		public int GiftType { get; set; }
	}

	/// <summary>
	/// 单据项目抽象基类
	/// </summary>
	public abstract class BaseFinanceItemEntity<TId> : IBaseItemEntity<TId>
	{
		public TId Id { get; set; }

		/// <summary>
		/// 单据ID
		/// </summary>
		public int BillId { get; set; }

		/// <summary>
		/// 租户标识
		/// </summary>
		public int StoreId { get; set; }

		/// <summary>
		/// 明细备注
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
		/// 金额
		/// </summary>
		public decimal Amount { get; set; }

	}
}




