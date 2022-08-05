using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.Main
{
    public partial class ProductFlavorMap : IEntityTypeConfiguration<ProductFlavor>
    {
        //public ProductFlavorMap()
        //{
        //    ToTable("ProductFlavors");
        //    HasKey(o => o.Id);
        //}

        public void Configure(EntityTypeBuilder<ProductFlavor> builder)
        {
            builder.ToTable("ProductFlavors");
            builder.HasKey(b => b.Id);
           
        }

    }
}
