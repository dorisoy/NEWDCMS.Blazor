using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 用于表示调拨单
    /// </summary>
    public class AllocationBillMap : IEntityTypeConfiguration<AllocationBill>
    {

        public void Configure(EntityTypeBuilder<AllocationBill> builder)
        {
            builder.ToTable("AllocationBills");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.Operations);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);
           
        }
    }


    /// <summary>
    /// 用于表示调拨单项目
    /// </summary>
    public partial class AllocationItemMap : IEntityTypeConfiguration<AllocationItem>
    {

        //public AllocationItemMap()
        //{
        //    ToTable("AllocationItems");
        //    HasKey(o => o.Id);

        //    HasRequired(sao => sao.AllocationBill)
        //    .WithMany(sa => sa.AllocationItems)
        //    .HasForeignKey(sao => sao.AllocationBillId);
        //}

        public void Configure(EntityTypeBuilder<AllocationItem> builder)
        {
            builder.ToTable("AllocationItems");
            builder.HasKey(b => b.Id);

            builder.HasOne(sao => sao.AllocationBill)
            .WithMany(sa => sa.Items)
            .HasForeignKey(sao => sao.AllocationBillId).IsRequired();

           
        }
    }


}
