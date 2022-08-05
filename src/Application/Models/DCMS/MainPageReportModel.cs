using DCMS.Domain.Main;

namespace DCMS.Application.Models
{


    #region 仪表盘

    /// <summary>
    /// 仪表盘
    /// </summary>
    public class DashboardReportModel : DashboardReport
    {

    }

    #endregion

    #region 当月销量
    /// <summary>
    /// 当月销量
    /// </summary>
    public class MonthSaleReportModel : MonthSaleReport
    {


    }
    #endregion

    #region 当月销量进度
    /// <summary>
    /// 当月销量进度
    /// </summary>
    public class SalePercentReportModel : SalePercentReport
    {

    }
    #endregion

    #region 当日销量
    /// <summary>
    /// 当日销量
    /// </summary>
    public class BussinessVisitStoreReportModel : BussinessVisitStoreReport
    {


    }
    #endregion

    #region 所有经销商统计（用于Manage站点）

    /// <summary>
    /// 经销商信息统计（manage主页上面的4个）
    /// </summary>
    public class AllStoreDashboardModel : AllStoreDashboard
    {

    }

    /// <summary>
    /// 所有经销商销售信息
    /// </summary>
    public class AllStoreSaleInformationModel : AllStoreSaleInformation
    {


    }

    /// <summary>
    /// 订单总计
    /// </summary>
    public class AllStoreOrderTotalModel : AllStoreOrderTotal
    {



    }

    /// <summary>
    /// 未完成订单
    /// </summary>
    public class AllStoreUnfinishedOrderModel : AllStoreUnfinishedOrder
    {



    }


    #endregion



    /// <summary>
    /// 待处理事项统计
    /// </summary>
    public class PendingCountModel : PendingCount
    {

    }
}
