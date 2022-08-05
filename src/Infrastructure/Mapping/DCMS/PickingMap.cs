using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    public partial class PickingMap : IEntityTypeConfiguration<PickingBill>
    {
        public void Configure(EntityTypeBuilder<PickingBill> builder)
        {
            builder.ToTable("Pickings");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);
           
        }
    }

    public partial class PickingItemMap : IEntityTypeConfiguration<PickingItem>
    {

        //public PickingItemMap()
        //{
        //    ToTable("PickingItems");
        //    HasKey(o => o.Id);
        //}
        public void Configure(EntityTypeBuilder<PickingItem> builder)
        {
            builder.ToTable("PickingItems");
            builder.HasKey(b => b.Id);
           
        }
    }

}
