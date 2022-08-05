using DCMS.Domain.CSMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.CSMS
{

    public partial class TerminalSignReportMap : IEntityTypeConfiguration<TerminalSignReport>
    {
        public void Configure(EntityTypeBuilder<TerminalSignReport> builder)
        {
            builder.ToTable("TerminalSignReport");
            builder.HasKey(b => b.Id);
           
        }
    }

    public partial class OrderDetailMap : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetail");
            builder.HasKey(b => b.Id);
           
        }
    }
}
