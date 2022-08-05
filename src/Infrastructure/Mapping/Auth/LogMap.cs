using DCMS.Domain.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DCMS.Infrastructure.Mapping.Auth
{
	public partial class LogMap : IEntityTypeConfiguration<Log>
    {

        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Log");
            builder.HasKey(b => b.Id);
            builder.Property(l => l.ShortMessage).IsRequired();
            builder.Property(l => l.IpAddress).HasMaxLength(200);

            builder.HasOne(logItem => logItem.User)
                      .WithMany()
                      .HasForeignKey(logItem => logItem.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

           
        }
    }
}