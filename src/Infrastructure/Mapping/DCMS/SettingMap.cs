using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping
{

    public partial class SettingMap : IEntityTypeConfiguration<DCMS.Domain.Setting>
    {
        //public SettingMap()
        //{
        //    ToTable("Settings");
        //    HasKey(o => o.Id);
        //}

        public void Configure(EntityTypeBuilder<DCMS.Domain.Setting> builder)
        {
            builder.ToTable("Settings");
            builder.HasKey(b => b.Id);
           
        }
    }
}