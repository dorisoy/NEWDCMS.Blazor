using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 商品属性映射
    /// </summary>
    public partial class ProductAttributeMap : IEntityTypeConfiguration<ProductAttribute>
    {
        //public ProductAttributeMap()
        //{
        //    ToTable("ProductAttribute");
        //    HasKey(pa => pa.Id);
        //    Property(pa => pa.Name).IsRequired();
        //}

        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.ToTable("ProductAttribute");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).IsRequired();
           
        }
    }
}