using System.ComponentModel.DataAnnotations.Schema;


namespace DCMS.Domain.Main
{

	/// <summary>
	/// 用于表示预付款单据
	/// </summary>
	public class AdvancePaymentBill : BaseBillEntity<int,AdvancePaymentBillAccounting>
	{

		public AdvancePaymentBill()
		{
			BillType = BillTypeEnum.AdvancePaymentBill;
		}

		/// <summary>
		/// 付款人
		/// </summary>
		public int Draweer { get; set; }

		/// <summary>
		/// 供应商
		/// </summary>
		public int ManufacturerId { get; set; }

		/// <summary>
		/// 付款类型(预付款)
		/// </summary>
		public int PaymentType { get; set; }


		/// <summary>
		/// 打印数
		/// </summary>
		public int? PrintNum { get; set; } = 0;


		/// <summary>
		/// 预付款账户
		/// </summary>
		public int? AccountingOptionId { get; set; } = 0;

		/// <summary>
		/// 预付款金额
		/// </summary>
		public decimal? AdvanceAmount { get; set; }

		/// <summary>
		/// 操作源
		/// </summary>
		public int? Operation { get; set; } = 0;
		public OperationEnum Operations
		{
			get { return (OperationEnum)Operation; }
			set { Operation = (int)value; }
		}

		/// <summary>
		/// 记账凭证
		/// </summary>
		public int VoucherId { get; set; }
	}

	/// <summary>
	///  预付款账户（预付款单据科目映射表）
	/// </summary>
	public class AdvancePaymentBillAccounting : BaseAccountEntity<int>
	{
		/// <summary>
		/// 供应商
		/// </summary>
		public int ManufacturerId { get; set; }

		/// <summary>
		/// 副本
		/// </summary>
		[Column(TypeName = "BIT(1)")]
		public bool Copy { get; set; }

		//(导航) 会计科目
		
		public virtual AccountingOption AccountingOption { get; set; }
		//(导航) 预付款单据
		
		public virtual AdvancePaymentBill AdvancePaymentBill { get; set; }
	}

}
