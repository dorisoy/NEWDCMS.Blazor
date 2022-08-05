using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 用于表示盘点任务(部分)
    /// </summary>
    public class InventoryPartTaskBillMap : IEntityTypeConfiguration<InventoryPartTaskBill>
    {
        public void Configure(EntityTypeBuilder<InventoryPartTaskBill> builder)
        {
            builder.ToTable("InventoryPartTaskBills");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.Operations);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);
            builder.Ignore(c => c.Remark);

           
        }
    }


    /// <summary>
    /// 用于表示盘点任务(部分)项目
    /// </summary>
    public partial class InventoryPartTaskItemMap : IEntityTypeConfiguration<InventoryPartTaskItem>
    {

        //public InventoryPartTaskItemMap()
        //{
        //    ToTable("InventoryPartTaskItems");
        //    HasKey(o => o.Id);

        //    HasRequired(sao => sao.InventoryPartTaskBill)
        //    .WithMany(sa => sa.InventoryPartTaskItems)
        //    .HasForeignKey(sao => sao.InventoryPartTaskBillId);
        //}

        public void Configure(EntityTypeBuilder<InventoryPartTaskItem> builder)
        {
            builder.ToTable("InventoryPartTaskItems");
            builder.HasKey(b => b.Id);

            builder.HasOne(sao => sao.InventoryPartTaskBill)
            .WithMany(sa => sa.Items)
            .HasForeignKey(sao => sao.InventoryPartTaskBillId).IsRequired();
           
        }
    }


}
