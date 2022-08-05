using DCMS.Domain.Main;

namespace DCMS.Application.Models
{
    /// <summary>
    /// 装车调度单
    /// </summary>
    public class DispatchBillModel : DispatchBill
    {


    }

    /// <summary>
    /// 调度单明细
    /// </summary>
    public class DispatchItemModel : DispatchItem
    {


    }


    /// <summary>
    /// 用于表示单据签收
    /// </summary>
    public class DeliverySignModel : DeliverySign
    {


    }


    /// <summary>
    /// 用于表示送货签收(销售订单、退货订单，换货单，销售单（车销），费用支出单)
    /// </summary>
    public class DeliverySignUpdateModel : DeliverySignUpdate
    {

    }
}
