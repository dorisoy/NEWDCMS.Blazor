using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.Main
{

    public partial class DispatchBillMap : IEntityTypeConfiguration<DispatchBill>
    {

        public void Configure(EntityTypeBuilder<DispatchBill> builder)
        {
            builder.ToTable("DispatchBills");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.Operations);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);
            builder.Ignore(c => c.Remark);
            builder.Ignore(c => c.Deleted);

           
        }

    }

    public partial class DispatchItemMap : IEntityTypeConfiguration<DispatchItem>
    {

        //public DispatchItemMap()
        //{
        //    ToTable("DispatchItems");
        //    HasKey(o => o.Id);

        //    HasRequired(o => o.DispatchBill)
        //        .WithMany(m => m.DispatchItems)
        //        .HasForeignKey(o => o.DispatchBillId);

        //}

        public void Configure(EntityTypeBuilder<DispatchItem> builder)
        {
            builder.ToTable("DispatchItems");
            builder.HasKey(b => b.Id);

            builder.HasOne(o => o.DispatchBill)
                .WithMany(m => m.Items)
                .HasForeignKey(o => o.DispatchBillId).IsRequired();

           
        }

    }

}
