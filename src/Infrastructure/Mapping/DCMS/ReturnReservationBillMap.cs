using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    /// <summary>
    /// 退货订单
    /// </summary>
    public partial class ReturnReservationBillMap : IEntityTypeConfiguration<ReturnReservationBill>
    {

        public void Configure(EntityTypeBuilder<ReturnReservationBill> builder)
        {
            builder.ToTable("ReturnReservationBills");
            builder.HasKey(b => b.Id);

            builder.Ignore(c => c.Operations);
            //builder.Ignore(c => c.TaxAmount);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);

           
        }
    }

    /// <summary>
    /// 退货订单明细
    /// </summary>
    public partial class ReturnReservationItemMap : IEntityTypeConfiguration<ReturnReservationItem>
    {

        //public ReturnReservationItemMap()
        //{
        //    ToTable("ReturnReservationItems");
        //    HasKey(o => o.Id);

        //    HasRequired(o => o.ReturnReservationBill)
        //        .WithMany(m => m.ReturnReservationItems)
        //        .HasForeignKey(o => o.ReturnReservationBillId);

        //}
        public void Configure(EntityTypeBuilder<ReturnReservationItem> builder)
        {
            builder.ToTable("ReturnReservationItems");
            builder.HasKey(b => b.Id);

            builder.HasOne(o => o.ReturnReservationBill)
                .WithMany(m => m.Items)
                .HasForeignKey(o => o.ReturnReservationBillId).IsRequired();



           
        }
    }

    /// <summary>
    ///  收款账户（退货订单据科目映射表）
    /// </summary>
    public partial class ReturnReservationBillAccountingMap : IEntityTypeConfiguration<ReturnReservationBillAccounting>
    {
        //public ReturnReservationBillAccountingMap()
        //{
        //    ToTable("ReturnReservationBill_Accounting_Mapping");

        //    HasRequired(o => o.AccountingOption)
        //         .WithMany()
        //         .HasForeignKey(o => o.AccountingOptionId);

        //    HasRequired(o => o.ReturnReservationBill)
        //        .WithMany(m => m.ReturnReservationBillAccountings)
        //        .HasForeignKey(o => o.ReturnReservationBillId);

        //}

        public void Configure(EntityTypeBuilder<ReturnReservationBillAccounting> builder)
        {
            builder.ToTable("ReturnReservationBill_Accounting_Mapping")
               .Property(entity => entity.BillId);


            builder.HasKey(mapping => new { mapping.BillId, mapping.AccountingOptionId });

            builder.Property(mapping => mapping.BillId);
            builder.Property(mapping => mapping.AccountingOptionId);

            builder.HasOne(mapping => mapping.AccountingOption)
                .WithMany()
                .HasForeignKey(mapping => mapping.AccountingOptionId)
                .IsRequired();

            builder.HasOne(mapping => mapping.ReturnReservationBill)
               .WithMany(customer => customer.ReturnReservationBillAccountings)
                .HasForeignKey(mapping => mapping.BillId)
                .IsRequired();



           
        }
    }

}
