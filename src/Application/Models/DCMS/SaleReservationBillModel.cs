using DCMS.Domain.Main;
using System.Collections.Generic;
using System.Linq;

namespace DCMS.Application.Models
{
    /// <summary>
    /// 销售订单
    /// </summary>
    public class SaleReservationBillModel : SaleReservationBill, IModel<int>
    {
        public int UniqueId { get; set; }
        public new List<SaleReservationItemModel> Items { get; set; }
    }

    /// <summary>
    /// 销售订单明细
    /// </summary>
    public class SaleReservationItemModel :  SaleReservationItem, IModel<int>
    {
        public int UniqueId { get; set; }
        public string ProductName { get; set; }
        public ProductModel Product { get; set; }
        public string BarCode { get; set; }
        public string UnitName { get; set; }
        public string UnitConvert { get; set; }
    }


    /// <summary>
    ///  收款账户（销售订单科目映射表）
    /// </summary>
    public class SaleReservationBillAccountingModel : SaleReservationBillAccounting
    {

    }

    public class SaleReservationBillUpdateModel : SaleReservationBillUpdate
    {

    }
}
