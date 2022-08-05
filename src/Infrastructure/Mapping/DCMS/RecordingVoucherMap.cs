using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 用于表示记账凭证
    /// </summary>
    public partial class RecordingVoucherMap : IEntityTypeConfiguration<RecordingVoucher>
    {
        public void Configure(EntityTypeBuilder<RecordingVoucher> builder)
        {
            builder.ToTable("RecordingVouchers");
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.Generate);
            builder.Ignore(c => c.BillType);
            
            builder.Ignore(c => c.Remark);

            builder.Property(mapping => mapping.BillTypeId);
            builder.Property(mapping => mapping.Deleted);

           
        }
    }


    /// <summary>
    /// 用于表示凭证项目
    /// </summary>
    public partial class VoucherItemMap : IEntityTypeConfiguration<VoucherItem>
    {

        public void Configure(EntityTypeBuilder<VoucherItem> builder)
        {
            builder.ToTable("VoucherItems");
            builder.HasKey(b => b.Id);
            //
            builder.Ignore(b => b.AccountingCode);

            builder.HasOne(sao => sao.RecordingVoucher)
            .WithMany(sa => sa.Items)
            .HasForeignKey(sao => sao.RecordingVoucherId).IsRequired();

           
        }
    }



}
