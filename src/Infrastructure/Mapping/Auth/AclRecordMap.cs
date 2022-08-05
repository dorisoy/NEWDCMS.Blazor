//using System.Data.Entity.ModelConfiguration;
using DCMS.Domain.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.Auth
{
	public partial class AclRecordMap : IEntityTypeConfiguration<AclRecord>
    {
        public void Configure(EntityTypeBuilder<AclRecord> builder)
        {
            builder.ToTable("AclRecord");
            builder.HasKey(affiliate => affiliate.Id);
            builder.Property(lp => lp.EntityName).IsRequired().HasMaxLength(400);
        }
    }
}