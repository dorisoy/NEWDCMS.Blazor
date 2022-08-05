using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 用于表示商品报损单
    /// </summary>
    public class ScrapProductBillMap : IEntityTypeConfiguration<ScrapProductBill>
    {
        public void Configure(EntityTypeBuilder<ScrapProductBill> builder)
        {
            builder.ToTable("ScrapProductBills");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.Operations);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);
            builder.Ignore(c => c.Deleted);

           
        }
    }


    /// <summary>
    /// 商品报损单项目
    /// </summary>
    public partial class ScrapProductItemMap : IEntityTypeConfiguration<ScrapProductItem>
    {

        //public ScrapProductItemMap()
        //{
        //    ToTable("ScrapProductItems");
        //    HasKey(o => o.Id);

        //    HasRequired(sao => sao.ScrapProductBill)
        //    .WithMany(sa => sa.ScrapProductItems)
        //    .HasForeignKey(sao => sao.ScrapProductBillId);
        //}

        public void Configure(EntityTypeBuilder<ScrapProductItem> builder)
        {
            builder.ToTable("ScrapProductItems");
            builder.HasKey(b => b.Id);

            builder.HasOne(sao => sao.ScrapProductBill)
             .WithMany(sa => sa.Items)
             .HasForeignKey(sao => sao.ScrapProductBillId).IsRequired();

           
        }
    }


}
