using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DCMS.Domain.Auth;

namespace DCMS.Infrastructure.Mapping.Auth
{
	public partial class ActivityLogTypeMap : IEntityTypeConfiguration<ActivityLogType>
    {
        public void Configure(EntityTypeBuilder<ActivityLogType> builder)
        {
            builder.ToTable("ActivityLogType");
            builder.HasKey(b => b.Id);
            builder.Property(alt => alt.SystemKeyword).IsRequired().HasMaxLength(100);
            builder.Property(alt => alt.Name).IsRequired().HasMaxLength(200);

        }
    }
}
