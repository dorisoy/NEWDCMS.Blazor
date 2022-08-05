using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    /// <summary>
    /// 换货单
    /// </summary>
    public partial class ExchangeBillMap : IEntityTypeConfiguration<ExchangeBill>
    {

        public void Configure(EntityTypeBuilder<ExchangeBill> builder)
        {
            builder.ToTable("ExchangeBills");
            builder.HasKey(b => b.Id);

            builder.Ignore(o => o.Operations);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);

           
        }
    }
    public partial class ExchangeItemMap : IEntityTypeConfiguration<ExchangeItem>
    {
        public void Configure(EntityTypeBuilder<ExchangeItem> builder)
        {
            builder.ToTable("ExchangeItems");
            builder.HasKey(b => b.Id);

            builder.HasOne(o => o.ExchangeBill)
                .WithMany(m => m.Items)
                .HasForeignKey(o => o.ExchangeBillId).IsRequired();



           
        }
    }


}
