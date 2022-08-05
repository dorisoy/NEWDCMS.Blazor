using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.Main
{

    public partial class CombinationMap : IEntityTypeConfiguration<Combination>
    {
        //public CombinationMap()
        //{
        //    ToTable("Combinations");
        //    HasKey(o => o.Id);

        //    HasRequired(tp => tp.Product)
        //        .WithMany()
        //        .HasForeignKey(tp => tp.ProductId);

        //}
        public void Configure(EntityTypeBuilder<Combination> builder)
        {
            builder.ToTable("Combinations");
            builder.HasKey(b => b.Id);

            builder.HasOne(tp => tp.Product)
                .WithMany()
                .HasForeignKey(tp => tp.ProductId);

           
        }
    }

    public partial class ProductCombinationMap : IEntityTypeConfiguration<ProductCombination>
    {
        //public ProductCombinationMap()
        //{
        //    ToTable("ProductCombinations");
        //    HasKey(o => o.Id);

        //    HasRequired(tp => tp.Combination)
        //        .WithMany(p => p.ProductCombinations)
        //        .HasForeignKey(tp => tp.CombinationId);

        //    HasRequired(tp => tp.Product)
        //        .WithMany()
        //        .HasForeignKey(tp => tp.ProductId);
        //}

        public void Configure(EntityTypeBuilder<ProductCombination> builder)
        {
            builder.ToTable("ProductCombinations");
            builder.HasKey(b => b.Id);

            builder.HasOne(tp => tp.Combination)
                .WithMany(p => p.ProductCombinations)
                .HasForeignKey(tp => tp.CombinationId);

            builder.HasOne(tp => tp.Product)
                .WithMany()
                .HasForeignKey(tp => tp.ProductId);

           
        }
    }


}
