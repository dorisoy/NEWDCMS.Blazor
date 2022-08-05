using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    /// <summary>
    /// 提供商映射
    /// </summary>
    public partial class ManufacturerMap : IEntityTypeConfiguration<Manufacturer>
    {
        //public ManufacturerMap()
        //{
        //    ToTable("Manufacturer");
        //    HasKey(m => m.Id);
        //    Property(m => m.Name).IsRequired().HasMaxLength(400);
        //}

        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.ToTable("Manufacturer");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(400);
           
        }
    }
}