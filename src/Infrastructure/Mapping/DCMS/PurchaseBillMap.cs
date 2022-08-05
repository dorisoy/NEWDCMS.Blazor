using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 采购单
    /// </summary>
    public partial class PurchaseBillMap : IEntityTypeConfiguration<PurchaseBill>
    {

        public void Configure(EntityTypeBuilder<PurchaseBill> builder)
        {
            builder.ToTable("PurchaseBills");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.Operations);
            builder.Ignore(c => c.PaymentStatus);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);
           

        }
    }

    /// <summary>
    /// 采购单明细
    /// </summary>
    public partial class PurchaseItemMap : IEntityTypeConfiguration<PurchaseItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseItem> builder)
        {
            builder.ToTable("PurchaseItems");
            builder.HasKey(b => b.Id);
            builder.HasOne(o => o.PurchaseBill)
                .WithMany(m => m.Items)
                .HasForeignKey(o => o.BillId).IsRequired();


            builder.Ignore(c => c.Profit);
            builder.Ignore(c => c.CostProfitRate);
            builder.Ignore(c => c.IsGifts);


           
        }
    }

    /// <summary>
    ///  付款账户（付款单据科目映射表）
    /// </summary>
    public partial class PurchaseBillAccountingMap : IEntityTypeConfiguration<PurchaseBillAccounting>
    {
        public void Configure(EntityTypeBuilder<PurchaseBillAccounting> builder)
        {
            builder.ToTable("PurchaseBill_Accounting_Mapping")
               .Property(entity => entity.BillId);
    

            builder.HasKey(mapping => new { mapping.BillId, mapping.AccountingOptionId });

            builder.Property(mapping => mapping.BillId);
            builder.Property(mapping => mapping.AccountingOptionId);

            builder.HasOne(mapping => mapping.AccountingOption)
                .WithMany()
                .HasForeignKey(mapping => mapping.AccountingOptionId)
                .IsRequired();

            builder.HasOne(mapping => mapping.PurchaseBill)
               .WithMany(customer => customer.PurchaseBillAccountings)
                .HasForeignKey(mapping => mapping.BillId)
                .IsRequired();



           
        }
    }

    /// <summary>
    /// 采购单
    /// </summary>
    public partial class ProductSkuChangeHistoryMap : IEntityTypeConfiguration<ProductSkuChangeHistory>
    {

        public void Configure(EntityTypeBuilder<ProductSkuChangeHistory> builder)
        {
            builder.ToTable("ProductSkuChangeHistory");
            builder.HasKey(b => b.Id);
           
        }
    }
}
