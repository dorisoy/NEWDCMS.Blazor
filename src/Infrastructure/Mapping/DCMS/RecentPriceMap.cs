using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    ///  最近售价
    /// </summary>
    public partial class RecentPriceMap : IEntityTypeConfiguration<RecentPrice>
    {
        //public RecentPriceMap()
        //{
        //    ToTable("RecentPrices");
        //    HasKey(o => o.Id);

        //    HasRequired(p => p.Product)
        //        .WithMany()
        //        .HasForeignKey(p => p.ProductId);
        //}


        public void Configure(EntityTypeBuilder<RecentPrice> builder)
        {
            builder.ToTable("RecentPrices");
            builder.HasKey(b => b.Id);

            builder.HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .IsRequired();

           
        }
    }


}
