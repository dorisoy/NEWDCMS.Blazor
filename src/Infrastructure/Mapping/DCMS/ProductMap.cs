using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 商品映射
    /// </summary>
    public partial class ProductMap : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(b => b.Id);
            builder.Property(o => o.ManageInventoryMethodId).IsRequired();
            builder.Ignore(o => o.ManageInventoryMethod);
            builder.Ignore(o => o.LowStockActivity);
            builder.Ignore(o => o.Brand);
           
        }
    }



    public partial class ProductPictureMap : IEntityTypeConfiguration<ProductPicture>
    {

        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {

            builder.ToTable("ProductPicture");
            builder.HasKey(mapping => new { mapping.ProductId, mapping.PictureId });

            builder.Property(mapping => mapping.ProductId);
            builder.Property(mapping => mapping.PictureId);

            builder.HasOne(mapping => mapping.Picture)
                .WithMany()
                .HasForeignKey(mapping => mapping.PictureId)
                .IsRequired();

            builder.HasOne(mapping => mapping.Product)
               .WithMany(customer => customer.ProductPictures)
                .HasForeignKey(mapping => mapping.ProductId)
                .IsRequired();



           
        }
    }

}
