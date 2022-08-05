using DCMS.Domain;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace DCMS.Domain.Main
{
    /// <summary>
    /// 用于表示收款单据
    /// </summary>
    public class CashReceiptBill : BaseBillEntity<int,CashReceiptItem>
    {
        public CashReceiptBill()
        {
            BillType = BillTypeEnum.CashReceiptBill;
        }

        private ICollection<CashReceiptBillAccounting> _cashReceiptBillAccountings;

        /// <summary>
        /// 收款人
        /// </summary>
        public int Payeer { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public int CustomerId { get; set; }


        /// <summary>
        /// 打印数
        /// </summary>
        public int? PrintNum { get; set; } = 0;

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
        /// (导航)收款账户
        /// </summary>
        
        public virtual ICollection<CashReceiptBillAccounting> CashReceiptBillAccountings
        {
            get { return _cashReceiptBillAccountings ?? (_cashReceiptBillAccountings = new List<CashReceiptBillAccounting>()); }
            protected set { _cashReceiptBillAccountings = value; }
        }

        /// <summary>
        /// 上交状态
        /// </summary>
        [Column(TypeName = "BIT(1)")]
        public bool? HandInStatus { get; set; } = false;

        /// <summary>
        /// 上交时间
        /// </summary>
        public DateTime? HandInDate { get; set; }


        /// <summary>
        /// 记账凭证
        /// </summary>
        public int VoucherId { get; set; }

        /// <summary>
        /// 欠款
        /// </summary>
        public decimal OweCash { get; set; }

        /// <summary>
        /// 应收
        /// </summary>
        public decimal ReceivableAmount { get; set; }

        /// <summary>
        /// 优惠
        /// </summary>
        public decimal PreferentialAmount { get; set; }
    }

    /// <summary>
    /// 用于表示收款单据项目
    /// </summary>
    public class CashReceiptItem : AuditableEntity<int>
    {

        /// <summary>
        /// 收款单Id
        /// </summary>
        public int CashReceiptBillId { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary>
        public string BillNumber { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        public int BillId { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public int BillTypeId { get; set; }
        public BillTypeEnum BillTypeEnum
        {
            get { return (BillTypeEnum)BillTypeId; }
            set { BillTypeId = (int)value; }
        }

        /// <summary>
        /// 开单日期(这里是单据的开出时间)
        /// </summary>
        public DateTime MakeBillDate { get; set; }

        /// <summary>
        /// 单据金额
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        ///优惠金额
        /// </summary>
        public decimal? DiscountAmount { get; set; }

        /// <summary>
        /// 已收金额
        /// </summary>
        public decimal? PaymentedAmount { get; set; }

        /// <summary>
        /// 尚欠金额
        /// </summary>
        public decimal? ArrearsAmount { get; set; }

        /// <summary>
        /// 本次优惠金额
        /// </summary>
        public decimal? DiscountAmountOnce { get; set; }

        /// <summary>
        /// 本次收款金额
        /// </summary>
        public decimal? ReceivableAmountOnce { get; set; }

        /// <summary>
        /// 收款后尚欠金额	
        /// </summary>
        public decimal? AmountOwedAfterReceipt { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }


        public virtual CashReceiptBill CashReceiptBill { get; set; }

    }


    /// <summary>
    ///  收款账户（收款单据科目映射表）
    /// </summary>
    public class CashReceiptBillAccounting : BaseAccountEntity<int>
    {
        /// <summary>
        /// 客户
        /// </summary>
        public int TerminalId { get; set; }

        //(导航) 会计科目
        public virtual AccountingOption AccountingOption { get; set; }
        //(导航) 收款单据
        public virtual CashReceiptBill CashReceiptBill { get; set; }
    }

}
