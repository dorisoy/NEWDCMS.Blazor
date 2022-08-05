using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    /// <summary>
    /// 销售订单
    /// </summary>
    public partial class SaleReservationBillMap : IEntityTypeConfiguration<SaleReservationBill>
    {

        public void Configure(EntityTypeBuilder<SaleReservationBill> builder)
        {
            builder.ToTable("SaleReservationBills");
            builder.HasKey(b => b.Id);

            builder.Ignore(o => o.Operations);
            //builder.Ignore(c => c.TaxAmount);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);

           
        }
    }

    /// <summary>
    /// 销售订单明细
    /// </summary>
    public partial class SaleReservationItemMap : IEntityTypeConfiguration<SaleReservationItem>
    {
        public void Configure(EntityTypeBuilder<SaleReservationItem> builder)
        {
            builder.ToTable("SaleReservationItems");
            builder.HasKey(b => b.Id);

            builder.HasOne(o => o.SaleReservationBill)
                .WithMany(m => m.Items)
                .HasForeignKey(o => o.SaleReservationBillId).IsRequired();

  
           
        }
    }

    /// <summary>
    ///  收款账户（收款单据科目映射表）
    /// </summary>
    public partial class SaleReservationBillAccountingMap : IEntityTypeConfiguration<SaleReservationBillAccounting>
    {
        //public SaleReservationBillAccountingMap()
        //{
        //    ToTable("SaleReservationBill_Accounting_Mapping");

        //    HasRequired(o => o.AccountingOption)
        //         .WithMany()
        //         .HasForeignKey(o => o.AccountingOptionId);

        //    HasRequired(o => o.SaleReservationBill)
        //        .WithMany(m => m.SaleReservationBillAccountings)
        //        .HasForeignKey(o => o.SaleReservationBillId);

        //}

        public void Configure(EntityTypeBuilder<SaleReservationBillAccounting> builder)
        {
            builder.ToTable("SaleReservationBill_Accounting_Mapping")
               .Property(entity => entity.BillId);

            builder.HasKey(mapping => new { mapping.BillId, mapping.AccountingOptionId });

            builder.Property(mapping => mapping.BillId);
            builder.Property(mapping => mapping.AccountingOptionId);

            builder.HasOne(mapping => mapping.AccountingOption)
                .WithMany()
                .HasForeignKey(mapping => mapping.AccountingOptionId)
                .IsRequired();

            builder.HasOne(mapping => mapping.SaleReservationBill)
               .WithMany(customer => customer.SaleReservationBillAccountings)
                .HasForeignKey(mapping => mapping.BillId)
                .IsRequired();



           
        }
    }

}
