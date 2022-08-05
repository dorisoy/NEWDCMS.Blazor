using DCMS.Domain.Main;
using System.Collections.Generic;

namespace DCMS.Application.Models
{
    /// <summary>
    /// 用于表示商品实体
    /// </summary>
    public class ProductModel : Product
    {
        public ProductModel()
        {
            BigProductPrices = new ProductPriceModel();
            StrokeProductPrices = new ProductPriceModel();
            SmallProductPrices = new ProductPriceModel();
            Prices = new List<UnitPricesModel>();
            Units = new Dictionary<string, int>();
            StockQuantities = new List<StockQuantityModel>();
            CostPrices = new Dictionary<int, decimal>();
        }


        public int CombinationId { get; set; } = 0;

        /// <summary>
        /// 品牌
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 规格属性小单位
        /// </summary>
        public string SmallUnitName { get; set; }
        /// <summary>
        /// 规格属性中单位
        /// </summary>
        public string StrokeUnitName { get; set; }

        /// <summary>
        /// 规格属性大单位
        /// </summary>
        public string BigUnitName { get; set; } 
     

        //是否启用生产日期配置
        public bool IsShowCreateDate { get; set; }


        public string PercentageCalCulateMethods { get; set; }
        public string PercentageSales { get; set; }
        public string PercentageReturns { get; set; }

        /// <summary>
        /// 单位价格字典
        /// </summary>
        public Dictionary<string, string> UnitPriceDicts { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 当前库存数量
        /// </summary>
        public int CurrentStock { get; set; }

        /// <summary>
        /// 生产日期列表
        /// </summary>
        public List<string> ProductTimes { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public int ProductId { get; set; } = 0;

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 商品类别名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 商品SKU编码
        /// </summary>
        public string ProductSKU { get; set; }


        /// <summary>
        /// 条形码
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public int UnitId { get; set; } = 0;

        /// <summary>
        /// 单位字典
        /// </summary>
        public Dictionary<string, int> Units { get; set; }

        //单位名称
        public string UnitName { get; set; }

        /// <summary>
        /// 单位换算
        /// </summary>
        public string UnitConversion { get; set; }


        /// <summary>
        /// 实时库存数量
        /// </summary>
        public int? StockQty { get; set; } = 0;

        /// <summary>
        /// 可用库存数量
        /// </summary>
        public int? UsableQuantity { get; set; } = 0;
        public string UsableQuantityConversion;

        /// <summary>
        /// 预占库存数量
        /// </summary>
        public int? CurrentQuantity { get; set; } = 0;
        public string CurrentQuantityConversion;

        /// <summary>
        ///预占库存数量 
        /// </summary>
        public int? OrderQuantity { get; set; } = 0;
        public string OrderQuantityConversion;

        /// <summary>
        /// 锁定库存数量
        /// </summary>
        public int? LockQuantity { get; set; } = 0;
        public string LockQuantityConversion;


        public SpecificationAttributeOptionModel SmallOption { get; set; }
        public SpecificationAttributeOptionModel StrokeOption { get; set; }
        public SpecificationAttributeOptionModel BigOption { get; set; }


        public ProductPriceModel BigProductPrices { get; set; }
        public ProductPriceModel StrokeProductPrices { get; set; }
        public ProductPriceModel SmallProductPrices { get; set; }

        /// <summary>
        /// 单位价格
        /// </summary>
        public List<UnitPricesModel> Prices { get; set; }


        /// <summary>
        /// 成本价（预设进价、平均进价）
        /// </summary>
        public Dictionary<int, decimal> CostPrices { get; set; }

        /// <summary>
        /// 商品所有库存量
        /// </summary>
        public List<StockQuantityModel> StockQuantities { get; set; }

    }


    public class ProductUpdateModel : ProductUpdate
    {

    }


    public class UnitPricesModel
    {

        /// <summary>
        /// 单位Id
        /// </summary>
        public int UnitId { get; set; } = 0;

        public ProductPriceModel ProductPrice { get; set; }

        /// <summary>
        /// 单位换算
        /// </summary>
        public string UnitConversion { get; set; }
    }


    public class StockQuantityModel
    {
        /// <summary>
        /// 可用库存数量
        /// </summary>
        public int UsableQuantity { get; set; } = 0;
        /// <summary>
        /// 现货库存数量
        /// </summary>
        public int CurrentQuantity { get; set; } = 0;

        public int WareHouseId { get; set; } = 0;
    }
}
