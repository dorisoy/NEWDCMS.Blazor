using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 用于表示费用合同单据
    /// </summary>
    public partial class CostContractBillMap : IEntityTypeConfiguration<CostContractBill>
    {
        public void Configure(EntityTypeBuilder<CostContractBill> builder)
        {
            builder.ToTable("CostContractBills");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.Operations);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);
           
        }
    }


    /// <summary>
    /// 费用合同单据项目
    /// </summary>
    public partial class CostContractItemMap : IEntityTypeConfiguration<CostContractItem>
    {

        //public CostContractItemMap()
        ////{
        ////    ToTable("CostContractItems");
        ////    HasKey(o => o.Id);

        ////    HasRequired(sao => sao.CostContractBill)
        ////    .WithMany(sa => sa.CostContractItems)
        ////    .HasForeignKey(sao => sao.CostContractBillId);
        ////}

        public void Configure(EntityTypeBuilder<CostContractItem> builder)
        {
            builder.ToTable("CostContractItems");
            builder.HasKey(b => b.Id);

            builder.HasOne(sao => sao.CostContractBill)
          .WithMany(sa => sa.Items)
          .HasForeignKey(sao => sao.CostContractBillId).IsRequired();

           
        }
    }
}
