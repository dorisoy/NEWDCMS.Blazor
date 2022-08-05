using DCMS.Domain.CRM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.CRM
{

    /// <summary>
    /// 终端映射
    /// </summary>
    public partial class TerminalMap : IEntityTypeConfiguration<Terminal>
    {
        public void Configure(EntityTypeBuilder<Terminal> builder)
        {
            builder.ToTable("CRM_Terminals");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.DataTypeId);
           
        }
    }

}
