using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 销售单
    /// </summary>
    public partial class SaleBillMap : IEntityTypeConfiguration<SaleBill>
    {

        public void Configure(EntityTypeBuilder<SaleBill> builder)
        {
            builder.ToTable("SaleBills");
            builder.HasKey(b => b.Id);

            builder.HasOne(s => s.SaleReservationBill)
                .WithMany()
                .HasForeignKey(s => s.SaleReservationBillId).IsRequired();

            builder.Ignore(c => c.Operations);
            //builder.Ignore(c => c.TaxAmount);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);
            builder.Ignore(c => c.ReceivedStatus);

           
        }
    }

    /// <summary>
    /// 销售单明细
    /// </summary>
    public partial class SaleItemMap : IEntityTypeConfiguration<SaleItem>
    {

        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");
            builder.HasKey(b => b.Id);

            builder.HasOne(o => o.SaleBill)
                .WithMany(m => m.Items)
                .HasForeignKey(o => o.SaleBillId).IsRequired();



           
        }
    }

    /// <summary>
    ///  收款账户（收款单据科目映射表）
    /// </summary>
    public partial class SaleBillAccountingMap : IEntityTypeConfiguration<SaleBillAccounting>
    {
        public void Configure(EntityTypeBuilder<SaleBillAccounting> builder)
        {

            builder.ToTable("SaleBill_Accounting_Mapping")
                .Property(entity => entity.BillId);

            builder.HasKey(mapping => new {mapping.BillId, mapping.AccountingOptionId });
            builder.Property(mapping => mapping.BillId);
            builder.Property(mapping => mapping.AccountingOptionId);

            builder.HasOne(mapping => mapping.AccountingOption)
                .WithMany()
                .HasForeignKey(mapping => mapping.AccountingOptionId)
                .IsRequired();

            builder.HasOne(mapping => mapping.SaleBill)
               .WithMany(customer => customer.SaleBillAccountings)
                .HasForeignKey(mapping => mapping.BillId)
                .IsRequired();

           
        }
    }



    /// <summary>
    /// 用于表示送货签收
    /// </summary>
    public partial class DeliverySignMap : IEntityTypeConfiguration<DeliverySign>
    {

        //public DeliverySignMap()
        //{
        //    ToTable("DeliverySigns");
        //    HasKey(o => o.Id);

        //}

        public void Configure(EntityTypeBuilder<DeliverySign> builder)
        {
            builder.ToTable("DeliverySigns");
            builder.HasKey(b => b.Id);
           
        }
    }

    /// <summary>
    /// 留存凭证照片
    /// </summary>
    public partial class RetainPhotoMap : IEntityTypeConfiguration<RetainPhoto>
    {

        //public RetainPhotoMap()
        //{
        //    ToTable("RetainPhotos");
        //    HasKey(o => o.Id);

        //    HasRequired(o => o.DeliverySign)
        //        .WithMany(m => m.RetainPhotos)
        //        .HasForeignKey(o => o.DeliverySignId);

        //}
        public void Configure(EntityTypeBuilder<RetainPhoto> builder)
        {
            builder.ToTable("RetainPhotos");
            builder.HasKey(b => b.Id);

            builder.HasOne(o => o.DeliverySign)
                .WithMany(m => m.RetainPhotos)
                .HasForeignKey(o => o.DeliverySignId).IsRequired();

           
        }
    }


}
