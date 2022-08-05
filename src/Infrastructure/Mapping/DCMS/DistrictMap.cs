using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 片区映射
    /// </summary>
    public partial class DistrictMap : IEntityTypeConfiguration<District>
    {
        //public DistrictMap()
        //{
        //    ToTable("Districts");
        //    HasKey(o => o.Id);
        //}

        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.ToTable("Districts");
            builder.HasKey(b => b.Id);
           
        }
    }
}
