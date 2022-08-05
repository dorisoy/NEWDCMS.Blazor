using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DCMS.Domain.CRM;



namespace DCMS.Data.Mapping.CRM
{
	public partial class StoreMap : IEntityTypeConfiguration<Store>
	{
		public void Configure(EntityTypeBuilder<Store> builder)
		{
			builder.ToTable("CRM_Stores");
			builder.HasKey(b => b.Id);

			builder.Property(s => s.Name).IsRequired().HasMaxLength(400);
			builder.Property(s => s.Url).IsRequired().HasMaxLength(400);
			builder.Property(s => s.SecureUrl).HasMaxLength(400);
			builder.Property(s => s.Hosts).HasMaxLength(1000);

		}
	}
}