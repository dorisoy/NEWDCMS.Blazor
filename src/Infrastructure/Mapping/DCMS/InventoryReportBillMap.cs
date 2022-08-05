using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 用于门店库存上报
    /// </summary>
    public partial class InventoryReportBillMap : IEntityTypeConfiguration<InventoryReportBill>
    {

        public void Configure(EntityTypeBuilder<InventoryReportBill> builder)
        {
            builder.ToTable("InventoryReportBills");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.Operations);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);
            builder.Ignore(c => c.MakeUserId);
            builder.Ignore(c => c.AuditedUserId);
            builder.Ignore(c => c.AuditedDate);
            builder.Ignore(c => c.AuditedStatus);

           
        }
    }

    /// <summary>
    /// 商品明细
    /// </summary>
    public partial class InventoryReportItemMap : IEntityTypeConfiguration<InventoryReportItem>
    {

        //public InventoryReportItemMap()
        //{
        //    ToTable("InventoryReportItems");
        //    HasKey(o => o.Id);

        //    HasRequired(o => o.InventoryReportBill)
        //        .WithMany(m => m.InventoryReportItems)
        //        .HasForeignKey(o => o.InventoryReportBillId);

        //}

        public void Configure(EntityTypeBuilder<InventoryReportItem> builder)
        {
            builder.ToTable("InventoryReportItems");
            builder.HasKey(b => b.Id);
            builder.HasOne(o => o.InventoryReportBill)
             .WithMany(m => m.Items)
             .HasForeignKey(o => o.InventoryReportBillId);
           
        }
    }

    /// <summary>
    ///  库存明细
    /// </summary>
    public partial class InventoryReportStoreQuantityMap : IEntityTypeConfiguration<InventoryReportStoreQuantity>
    {
        //public InventoryReportStoreQuantityMap()
        //{
        //    ToTable("InventoryReportStoreQuantities");

        //    HasKey(o => o.Id);

        //    HasRequired(o => o.InventoryReportItem)
        //        .WithMany(m => m.InventoryReportStoreQuantities)
        //        .HasForeignKey(o => o.InventoryReportItemId);

        //}

        public void Configure(EntityTypeBuilder<InventoryReportStoreQuantity> builder)
        {
            builder.ToTable("InventoryReportStoreQuantities");
            builder.HasKey(b => b.Id);

            builder.HasOne(o => o.InventoryReportItem)
              .WithMany(m => m.InventoryReportStoreQuantities)
              .HasForeignKey(o => o.InventoryReportItemId).IsRequired();

           
        }
    }


    /// <summary>
    /// 上报汇总表
    /// </summary>
    public partial class InventoryReportSummaryMap : IEntityTypeConfiguration<InventoryReportSummary>
    {

        //public InventoryReportSummaryMap()
        //{
        //    ToTable("InventoryReportSummaries");
        //    HasKey(o => o.Id);

        //}

        public void Configure(EntityTypeBuilder<InventoryReportSummary> builder)
        {
            builder.ToTable("InventoryReportSummaries");
            builder.HasKey(b => b.Id);
           
        }
    }



}
