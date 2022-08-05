using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 统计类别
    /// </summary>
    public partial class StatisticalTypeMap : IEntityTypeConfiguration<StatisticalTypes>
    {
        //public StatisticalTypeMap()
        //{
        //    ToTable("StatisticalTypes");
        //    HasKey(o => o.Id);
        //}

        public void Configure(EntityTypeBuilder<StatisticalTypes> builder)
        {
            builder.ToTable("StatisticalTypes");
            builder.HasKey(b => b.Id);
           
        }
    }


}
