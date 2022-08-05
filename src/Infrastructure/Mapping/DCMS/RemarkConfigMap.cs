using DCMS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    public partial class RemarkConfigMap : IEntityTypeConfiguration<RemarkConfig>
    {
        //public RemarkConfigMap()
        //{
        //    ToTable("RemarkConfigs");
        //    HasKey(o => o.Id);
        //}

        public void Configure(EntityTypeBuilder<RemarkConfig> builder)
        {
            builder.ToTable("RemarkConfigs");
            builder.HasKey(b => b.Id);
           
        }
    }
}
