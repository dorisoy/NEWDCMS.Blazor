using DCMS.Domain.Main;

namespace DCMS.Application.Models
{

    #region 实体类

    /// <summary>
    /// 用于表示库存
    /// </summary>
    public class StockModel : Stock
    {

    }

    /// <summary>
    /// 用于表示出入库业务记录
    /// </summary>
    public class StockInOutRecordModel : StockInOutRecord
    {


    }



    public class StockInOutRecordQueryModel : StockInOutRecordQuery
    {

    }


    /// <summary>
    /// 用于表示库存流水(更改历史)记录
    /// </summary>
    public class StockFlowModel : StockFlow
    {



    }


    public class StockFlowQueryModel : StockFlowQuery
    {


    }


    /// <summary>
    /// 用于表示出入库记录和流水映射
    /// </summary>
    public class StockInOutRecordStockFlowModel : StockInOutRecordStockFlow
    {
    }

    /// <summary>
    /// 用于表示商品库存信息
    /// </summary>
    public class ProductStockItemModel : ProductStockItem
    {
    }


    /// <summary>
    /// 用于商品出入库明细记录
    /// </summary>
    public class StockInOutDetailsModel : StockInOutDetails
    {


    }

    #endregion

    #region 查询分析用

    public class StockQueryModel : StockQuery
    {


    }


    /// <summary>
    /// 实时库存查询
    /// </summary>
    public class RealTimeStockQueryModel : RealTimeStockQuery
    {


    }




    #endregion
}
