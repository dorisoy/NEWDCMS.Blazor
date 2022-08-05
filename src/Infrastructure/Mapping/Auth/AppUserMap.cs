using DCMS.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Auth
{

	public class AppUserMap : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			builder.ToTable(name: "Users");
			builder.Property(e => e.Id).ValueGeneratedOnAdd();
		}
	}

	public class AppRoleMap : IEntityTypeConfiguration<AppRole>
	{
		public void Configure(EntityTypeBuilder<AppRole> builder)
		{
			builder.ToTable(name: "Roles");
		}
	}


	public class UserRoleMap : IEntityTypeConfiguration<IdentityUserRole<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
		{
			builder.ToTable("UserRoles");
		}
	}

	public class UserClaimMap : IEntityTypeConfiguration<IdentityUserClaim<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
		{
			builder.ToTable("UserClaims");
		}
	}

	public class UserLoginMap : IEntityTypeConfiguration<IdentityUserLogin<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
		{
			builder.ToTable("UserLogins");
		}
	}

	public class AppRoleClaimMap : IEntityTypeConfiguration<AppRoleClaim>
	{
		public void Configure(EntityTypeBuilder<AppRoleClaim> builder)
		{
			builder.ToTable(name: "RoleClaims");

			builder.HasOne(d => d.Role)
			.WithMany(p => p.RoleClaims)
			.HasForeignKey(d => d.RoleId)
			.OnDelete(DeleteBehavior.Cascade);
		}
	}

	public class UserTokenMap : IEntityTypeConfiguration<IdentityUserToken<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
		{
			builder.ToTable("UserTokens");
		}
	}

}
