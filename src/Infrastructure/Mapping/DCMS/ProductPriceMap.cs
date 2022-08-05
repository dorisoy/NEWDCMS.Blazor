using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.Main
{
    public partial class ProductPriceMap : IEntityTypeConfiguration<ProductPrice>
    {
        //public ProductPriceMap()
        //{
        //    ToTable("ProductPrices");
        //    HasKey(o => o.Id);

        //    //商品和价格关系为1对多 
        //    HasRequired(bp => bp.Product)
        //        .WithMany(n => n.ProductPrices)
        //        .HasForeignKey(bp => bp.ProductId).WillCascadeOnDelete(true);
        //}

        public void Configure(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.ToTable("ProductPrices");
            builder.HasKey(b => b.Id);

            builder.HasOne(bp => bp.Product)
                .WithMany(n => n.ProductPrices)
                .HasForeignKey(bp => bp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

           
        }
    }
}
