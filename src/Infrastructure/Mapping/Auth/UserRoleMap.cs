using DCMS.Domain.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.Auth
{
	public partial class UserRoleMapV3 : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasKey(b => b.Id);
            builder.Property(cr => cr.Name).IsRequired().HasMaxLength(255);
            builder.Property(cr => cr.SystemName).HasMaxLength(255);

            builder.Ignore(user => user.Modules);
           
        }
    }
}