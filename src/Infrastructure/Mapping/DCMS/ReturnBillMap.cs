using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    /// <summary>
    /// 退货单
    /// </summary>
    public partial class ReturnBillMap : IEntityTypeConfiguration<ReturnBill>
    {


        public void Configure(EntityTypeBuilder<ReturnBill> builder)
        {
            builder.ToTable("ReturnBills");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.Operations);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);
            builder.Ignore(c => c.ReceivedStatus);
            builder.HasOne(s => s.ReturnReservationBill)
                .WithMany()
                .HasForeignKey(s => s.ReturnReservationBillId).IsRequired();


           
        }
    }

    /// <summary>
    /// 退货单明细
    /// </summary>
    public partial class ReturnItemMap : IEntityTypeConfiguration<ReturnItem>
    {

        //public ReturnItemMap()
        //{
        //    ToTable("ReturnItems");
        //    HasKey(o => o.Id);

        //    HasRequired(o => o.ReturnBill)
        //        .WithMany(m => m.ReturnItems)
        //        .HasForeignKey(o => o.ReturnBillId);

        //    Ignore(c => c.BusinessUserId);
        //    Ignore(c => c.DeliveryUserId);
        //    Ignore(c => c.MakeUserId);
        //    Ignore(c => c.AuditedUserId);
        //    Ignore(c => c.ReversedUserId);
        //    Ignore(c => c.ProductName);
        //    Ignore(c => c.CategoryId);
        //    Ignore(c => c.PercentageType);
        //}
        public void Configure(EntityTypeBuilder<ReturnItem> builder)
        {
            builder.ToTable("ReturnItems");
            builder.HasKey(b => b.Id);

            builder.HasOne(o => o.ReturnBill)
                .WithMany(m => m.Items)
                .HasForeignKey(o => o.ReturnBillId).IsRequired();



           
        }
    }

    /// <summary>
    ///  收款账户（退货单据科目映射表）
    /// </summary>
    public partial class ReturnBillAccountingMap : IEntityTypeConfiguration<ReturnBillAccounting>
    {
        public void Configure(EntityTypeBuilder<ReturnBillAccounting> builder)
        {
            builder.ToTable("ReturnBill_Accounting_Mapping");

            builder.HasKey(mapping => new { mapping.BillId, mapping.AccountingOptionId });

            builder.Property(mapping => mapping.BillId);
            builder.Property(mapping => mapping.AccountingOptionId);

            builder.HasOne(mapping => mapping.AccountingOption)
                .WithMany()
                .HasForeignKey(mapping => mapping.AccountingOptionId)
                .IsRequired();

            builder.HasOne(mapping => mapping.ReturnBill)
               .WithMany(customer => customer.ReturnBillAccountings)
                .HasForeignKey(mapping => mapping.BillId)
                .IsRequired();



           
        }
    }


}
