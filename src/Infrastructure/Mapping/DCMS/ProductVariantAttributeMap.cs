using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    /// <summary>
    /// 商品变体属性映射
    /// </summary>
    public partial class ProductVariantAttributeMap : IEntityTypeConfiguration<ProductVariantAttribute>
    {
        //public ProductVariantAttributeMap()
        //{
        //    ToTable("Products_ProductAttribute_Mapping");
        //    HasKey(pva => pva.Id);
        //    Ignore(pva => pva.AttributeControlType);

        //    HasRequired(pva => pva.Product)
        //        .WithMany(p => p.ProductVariantAttributes)
        //        .HasForeignKey(pva => pva.ProductId);

        //    HasRequired(pva => pva.ProductAttribute)
        //        .WithMany()
        //        .HasForeignKey(pva => pva.ProductAttributeId);
        //}



        public void Configure(EntityTypeBuilder<ProductVariantAttribute> builder)
        {
            //builder.HasKey(b => b.Id);
            //builder.ToTable(DCMSMappingDefaults.ProductProductAttributeTable);
            //builder.HasKey(mapping => new { mapping.ProductId, mapping.ProductAttributeId });
            //builder.Ignore(pva => pva.AttributeControlType);


            //builder.Property(mapping => mapping.ProductId).HasColumnName("ProductId");
            //builder.Property(mapping => mapping.ProductAttributeId).HasColumnName("ProductAttributeId");

            //builder.HasOne(mapping => mapping.Product)
            //    .WithMany(p => p.ProductVariantAttributes)
            //    .HasForeignKey(mapping => mapping.ProductId)
            //    .IsRequired();

            //builder.HasOne(mapping => mapping.ProductAttribute)
            //   .WithMany()
            //    .HasForeignKey(mapping => mapping.ProductAttributeId)
            //    .IsRequired();

            //


            builder.ToTable("ProductVariantAttribute");
            builder.HasKey(mapping => mapping.Id);

            builder.HasOne(mapping => mapping.Product)
                .WithMany(product => product.ProductVariantAttributes)
                .HasForeignKey(mapping => mapping.ProductId)
                .IsRequired();

            builder.HasOne(mapping => mapping.ProductAttribute)
                .WithMany()
                .HasForeignKey(mapping => mapping.ProductAttributeId)
                .IsRequired();

            builder.Ignore(pam => pam.AttributeControlType);

           
        }
    }
}