using DCMS.Domain.SKD;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.SKD
{
    public partial class EmailAccountMap : IEntityTypeConfiguration<EmailAccount>
    {

        public void Configure(EntityTypeBuilder<EmailAccount> builder)
        {
            builder.ToTable("EmailAccount");
            builder.HasKey(b => b.Id);

            builder.Property(ea => ea.Email).IsRequired().HasMaxLength(255);
            builder.Property(ea => ea.DisplayName).HasMaxLength(255);
            builder.Property(ea => ea.Host).IsRequired().HasMaxLength(255);
            builder.Property(ea => ea.Username).IsRequired().HasMaxLength(255);
            builder.Property(ea => ea.Password).IsRequired().HasMaxLength(255);

            builder.Ignore(ea => ea.FriendlyName);

           
        }
    }
}