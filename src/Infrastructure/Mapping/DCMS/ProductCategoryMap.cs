using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.Main
{
    /// <summary>
    /// 类别映射
    /// </summary>
    public partial class CategoryMap : IEntityTypeConfiguration<Category>
    {
        //public CategoryMap()
        //{
        //    ToTable("Categories");
        //    HasKey(o => o.Id);
        //}

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(b => b.Id);
           
        }
    }


    /// <summary>
    ///  商品类别映射
    /// </summary>
    public partial class ProductCategoryMap : IEntityTypeConfiguration<ProductCategory>
    {
        //public ProductCategoryMap()
        //{
        //    ToTable("Products_Category_Mapping");

        //    HasRequired(o => o.Category)
        //         .WithMany()
        //         .HasForeignKey(o => o.CategoryId);

        //    HasRequired(o => o.Product)
        //        .WithMany(m => m.ProductCategories)
        //        .HasForeignKey(o => o.ProductId);

        //}

        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {

            builder.ToTable("Products_Category_Mapping");
            builder.HasKey(mapping => new { mapping.CategoryId, mapping.ProductId });

            builder.Property(mapping => mapping.CategoryId);
            builder.Property(mapping => mapping.ProductId);

            builder.HasOne(mapping => mapping.Category)
                .WithMany()
                .HasForeignKey(mapping => mapping.CategoryId)
                .IsRequired();

            builder.HasOne(mapping => mapping.Product)
               .WithMany(customer => customer.ProductCategories)
                .HasForeignKey(mapping => mapping.ProductId)
                .IsRequired();



           
        }
    }

}
