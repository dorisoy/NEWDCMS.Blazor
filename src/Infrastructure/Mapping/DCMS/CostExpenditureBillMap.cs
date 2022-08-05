using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 用于表示费用支出单据
    /// </summary>
    public partial class CostExpenditureBillMap : IEntityTypeConfiguration<CostExpenditureBill>
    {

        public void Configure(EntityTypeBuilder<CostExpenditureBill> builder)
        {
            builder.ToTable("CostExpenditureBills");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.Operations);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);
            builder.Ignore(c => c.ReceivedStatus);
           
        }
    }


    /// <summary>
    /// 费用支出单据项目
    /// </summary>
    public partial class CostExpenditureItemMap : IEntityTypeConfiguration<CostExpenditureItem>
    {

        //public CostExpenditureItemMap()
        //{
        //    ToTable("CostExpenditureItems");
        //    HasKey(o => o.Id);

        //    HasRequired(sao => sao.CostExpenditureBill)
        //    .WithMany(sa => sa.CostExpenditureItems)
        //    .HasForeignKey(sao => sao.CostExpenditureBillId);
        //}

        public void Configure(EntityTypeBuilder<CostExpenditureItem> builder)
        {
            builder.ToTable("CostExpenditureItems");
            builder.HasKey(b => b.Id);

            builder.HasOne(sao => sao.CostExpenditureBill)
            .WithMany(sa => sa.Items)
            .HasForeignKey(sao => sao.CostExpenditureBillId).IsRequired();

           
        }
    }


    /// <summary>
    ///  费用支出账户（收款单据科目映射表）
    /// </summary>
    public partial class CostExpenditureBillAccountingMap : IEntityTypeConfiguration<CostExpenditureBillAccounting>
    {
        //public CostExpenditureBillAccountingMap()
        //{
        //    ToTable("CostExpenditureBill_Accounting_Mapping");

        //    HasRequired(o => o.AccountingOption)
        //         .WithMany()
        //         .HasForeignKey(o => o.AccountingOptionId);

        //    HasRequired(o => o.CostExpenditureBill)
        //        .WithMany(m => m.CostExpenditureBillAccountings)
        //        .HasForeignKey(o => o.CostExpenditureBillId);

        //}

        public void Configure(EntityTypeBuilder<CostExpenditureBillAccounting> builder)
        {
            builder.ToTable("CostExpenditureBill_Accounting_Mapping")
               .Property(entity => entity.BillId);
         

            builder.HasKey(mapping => new { mapping.BillId, mapping.AccountingOptionId });

            builder.Property(mapping => mapping.BillId);
            builder.Property(mapping => mapping.AccountingOptionId);

            builder.HasOne(mapping => mapping.AccountingOption)
                .WithMany()
                .HasForeignKey(mapping => mapping.AccountingOptionId)
                .IsRequired();

            builder.HasOne(mapping => mapping.CostExpenditureBill)
               .WithMany(customer => customer.CostExpenditureBillAccountings)
                .HasForeignKey(mapping => mapping.BillId)
                .IsRequired();



           
        }

    }

}
