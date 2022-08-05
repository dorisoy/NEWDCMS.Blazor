using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 用于表示盘点盈亏单
    /// </summary>
    public class InventoryProfitLossBillMap : IEntityTypeConfiguration<InventoryProfitLossBill>
    {
        public void Configure(EntityTypeBuilder<InventoryProfitLossBill> builder)
        {
            builder.ToTable("InventoryProfitLossBills");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.Operations);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);
            //builder.Ignore(c => c.Deleted);

           
        }
    }


    /// <summary>
    /// 用于表示盘点盈亏单项目
    /// </summary>
    public partial class InventoryProfitLossItemMap : IEntityTypeConfiguration<InventoryProfitLossItem>
    {

        //public InventoryProfitLossItemMap()
        //{
        //    ToTable("InventoryProfitLossItems");
        //    HasKey(o => o.Id);

        //    HasRequired(sao => sao.InventoryProfitLossBill)
        //    .WithMany(sa => sa.InventoryProfitLossItems)
        //    .HasForeignKey(sao => sao.InventoryProfitLossBillId);
        //}

        public void Configure(EntityTypeBuilder<InventoryProfitLossItem> builder)
        {
            builder.ToTable("InventoryProfitLossItems");
            builder.HasKey(b => b.Id);

            builder.HasOne(sao => sao.InventoryProfitLossBill)
            .WithMany(sa => sa.Items)
            .HasForeignKey(sao => sao.InventoryProfitLossBillId).IsRequired();

           
        }
    }


}
