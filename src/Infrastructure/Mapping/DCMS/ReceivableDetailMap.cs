using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    /// <summary>
    /// 应收款记录表映射
    /// </summary>
    public class ReceivableDetailMap : IEntityTypeConfiguration<ReceivableDetail>
    {
        //public ReceivableDetailMap()
        //{
        //    ToTable("ReceivableDetails");
        //    HasKey(o => o.Id);
        //}

        public void Configure(EntityTypeBuilder<ReceivableDetail> builder)
        {
            builder.ToTable("ReceivableDetails");
            builder.HasKey(b => b.Id);
           
        }
    }
}
