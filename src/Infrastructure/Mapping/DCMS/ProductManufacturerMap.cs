using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    public partial class ProductManufacturerMap : IEntityTypeConfiguration<ProductManufacturer>
    {
        public void Configure(EntityTypeBuilder<ProductManufacturer> builder)
        {

            builder.ToTable("Product_Manufacturer_Mapping");
            builder.HasKey(mapping => new { mapping.ProductId, mapping.ManufacturerId });

            builder.Property(mapping => mapping.ProductId);
            builder.Property(mapping => mapping.ManufacturerId);

            builder.HasOne(mapping => mapping.Manufacturer)
                .WithMany()
                .HasForeignKey(mapping => mapping.ManufacturerId)
                .IsRequired();

            builder.HasOne(mapping => mapping.Product)
               .WithMany(customer => customer.ProductManufacturers)
                .HasForeignKey(mapping => mapping.ProductId)
                .IsRequired();

           
        }

    }
}