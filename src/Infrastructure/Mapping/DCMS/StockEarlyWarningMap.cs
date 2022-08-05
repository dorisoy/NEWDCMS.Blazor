using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    public partial class StockEarlyWarningMap : IEntityTypeConfiguration<StockEarlyWarning>
    {
        //public StockEarlyWarningMap()
        //{
        //    ToTable("StockEarlyWarnings");
        //    HasKey(o => o.Id);

        //    HasRequired(sw => sw.WareHouse)
        //    .WithMany(sw => sw.StockEarlyWarnings)
        //    .HasForeignKey(sw => sw.WareHouseId);
        //}

        public void Configure(EntityTypeBuilder<StockEarlyWarning> builder)
        {
            builder.ToTable("StockEarlyWarnings");
            builder.HasKey(b => b.Id);


            builder.HasOne(sw => sw.WareHouse)
            .WithMany(sw => sw.StockEarlyWarnings)
            .HasForeignKey(sw => sw.WareHouseId).IsRequired();

           
        }
    }
}
