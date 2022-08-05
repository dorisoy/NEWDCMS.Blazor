using DCMS.Domain;


namespace DCMS.Domain.Main
{
    public class ProductSetting : ISettings
    {
        /// <summary>
        /// 商品小单位规格映射
        /// </summary>
        public int SmallUnitSpecificationAttributeOptionsMapping { get; set; }

        /// <summary>
        /// 商品中单位规格映射
        /// </summary>
        public int StrokeUnitSpecificationAttributeOptionsMapping { get; set; }

        /// <summary>
        /// 商品大单位规格映射
        /// </summary>
        public int BigUnitSpecificationAttributeOptionsMapping { get; set; }
    }
}
