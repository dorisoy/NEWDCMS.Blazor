using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 用于表示库存商品组合单
    /// </summary>
    public class CombinationProductBillMap : IEntityTypeConfiguration<CombinationProductBill>
    {
        public void Configure(EntityTypeBuilder<CombinationProductBill> builder)
        {
            builder.ToTable("CombinationProductBills");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.Operations);
            builder.Ignore(c => c.BillType);
            builder.Ignore(c => c.BillTypeId);
            builder.Ignore(c => c.Deleted);

           
        }
    }


    /// <summary>
    /// 用于表示库存商品组合单项目
    /// </summary>
    public partial class CombinationProductItemMap : IEntityTypeConfiguration<CombinationProductItem>
    {

        //public CombinationProductItemMap()
        //{
        //    ToTable("CombinationProductItems");
        //    HasKey(o => o.Id);

        //    HasRequired(sao => sao.CombinationProductBill)
        //    .WithMany(sa => sa.CombinationProductItems)
        //    .HasForeignKey(sao => sao.CombinationProductBillId);
        //}

        public void Configure(EntityTypeBuilder<CombinationProductItem> builder)
        {
            builder.ToTable("CombinationProductItems");
            builder.HasKey(b => b.Id);

            builder.HasOne(sao => sao.CombinationProductBill)
                .WithMany(sa => sa.Items)
                .HasForeignKey(sao => sao.CombinationProductBillId)
                .IsRequired();

           
        }
    }


}
