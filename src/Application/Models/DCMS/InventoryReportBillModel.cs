using DCMS.Domain.Main;


namespace DCMS.Application.Models
{

    /// <summary>
    /// 用于门店库存上报
    /// </summary>
    public class InventoryReportBillModel : InventoryReportBill
    {


    }

    /// <summary>
    /// 上报关联商品
    /// </summary>
    public class InventoryReportItemModel : InventoryReportItem
    {


    }


    /// <summary>
    /// 商品关联库存量
    /// </summary>
    public class InventoryReportStoreQuantityModel : InventoryReportStoreQuantity
    {



    }


    /// <summary>
    /// 上报汇总表
    /// </summary>
    public class InventoryReportSummaryModel : InventoryReportSummary
    {



    }


    /*
     * InventoryReportBill，ReportItem，ReportStoreQuantity 用于记录，InventoryReportSummary 用于汇总
     *  每次上报时更新或者插入上报汇总表（InventoryReportSummary）
        汇总时需要用到上报汇总表（ 这里的结构应在 ViewModel 绑定  ）：

        //客户
        public int TerminalId { get; set; }
        public string TerminalName { get; set; }
        //商品
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        //单位换算
        public string UnitConversion { get; set; }

        //期初时间
        public DateTime  BeginDate  { get; set; }
        //期末时间
        public DateTime  EndDate  { get; set; }


        //期初库存量
        public int  BeginStoreQuantity  { get; set; }
        //期末库存量
        public int  EndStoreQuantity { get; set; }


        //采购量
        public int  PurchaseQuantity { get; set; }


        //销售量
        public int  SaleSum { get; set; }


        1. 第一次上报商品 时，期末时间未空，有期初，采购量为上报采购量， 期初库存量 = 采购量。
        2. 第二次/之后任何一笔上报认为之前一笔的期末，本期采购量为 = 期初采购量+本次采购量，期初库存不变，期末库存 =  本次上报库存， 期末时间为=本次上报时间
           销售量=  期初库存+ 本期采购（期初采购量+本次采购量）- 期末库存（本次上报库存）
     */

}
