using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 采购退货单
    /// </summary>
    public partial class PurchaseReturnBillMap : IEntityTypeConfiguration<PurchaseReturnBill>
    {
        public void Configure(EntityTypeBuilder<PurchaseReturnBill> builder)
        {
            builder.ToTable("PurchaseReturnBills");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.Operations);
            builder.Ignore(c => c.PaymentStatus);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);
            builder.Ignore(c => c.Deleted);

           
        }
    }

    /// <summary>
    /// 采购退货单明细
    /// </summary>
    public partial class PurchaseReturnItemMap : IEntityTypeConfiguration<PurchaseReturnItem>
    {

        //public PurchaseReturnItemMap()
        //{
        //    ToTable("PurchaseReturnItems");
        //    HasKey(o => o.Id);
        //    HasRequired(o => o.PurchaseReturnBill)
        //        .WithMany(m => m.PurchaseReturnItems)
        //        .HasForeignKey(o => o.PurchaseReturnBillId);
        //}


        public void Configure(EntityTypeBuilder<PurchaseReturnItem> builder)
        {
            builder.ToTable("PurchaseReturnItems");
            builder.HasKey(b => b.Id);

            builder.HasOne(o => o.PurchaseReturnBill)
                .WithMany(m => m.Items)
                .HasForeignKey(o => o.PurchaseReturnBillId).IsRequired();

           
        }
    }

    /// <summary>
    ///  付款账户（付款单据科目映射表）
    /// </summary>
    public partial class PurchaseReturnBillAccountingMap : IEntityTypeConfiguration<PurchaseReturnBillAccounting>
    {
        //public PurchaseReturnBillAccountingMap()
        //{
        //    ToTable("PurchaseReturnBill_Accounting_Mapping");

        //    HasRequired(o => o.AccountingOption)
        //         .WithMany()
        //         .HasForeignKey(o => o.AccountingOptionId);

        //    HasRequired(o => o.PurchaseReturnBill)
        //        .WithMany(m => m.PurchaseReturnBillAccountings)
        //        .HasForeignKey(o => o.PurchaseReturnBillId);

        //}

        public void Configure(EntityTypeBuilder<PurchaseReturnBillAccounting> builder)
        {
            builder.ToTable("PurchaseReturnBill_Accounting_Mapping")
               .Property(entity => entity.BillId);


            builder.HasKey(mapping => new { mapping.BillId, mapping.AccountingOptionId });

            builder.Property(mapping => mapping.BillId);
            builder.Property(mapping => mapping.AccountingOptionId);

            builder.HasOne(mapping => mapping.AccountingOption)
                .WithMany()
                .HasForeignKey(mapping => mapping.AccountingOptionId)
                .IsRequired();

            builder.HasOne(mapping => mapping.PurchaseReturnBill)
               .WithMany(customer => customer.PurchaseReturnBillAccountings)
                .HasForeignKey(mapping => mapping.BillId)
                .IsRequired();


           
        }
    }
}
