using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 应收款映射
    /// </summary>
    public partial class ReceivableMap : IEntityTypeConfiguration<Receivable>
    {
        //public ReceivableMap()
        //{
        //    ToTable("Receivables");
        //    HasKey(o => o.Id);
        //}

        public void Configure(EntityTypeBuilder<Receivable> builder)
        {
            builder.ToTable("Receivables");
            builder.HasKey(b => b.Id);
           
        }
    }
}
