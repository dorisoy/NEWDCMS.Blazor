using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    /// <summary>
    /// 渠道映射
    /// </summary>
    public partial class RankMap : IEntityTypeConfiguration<Rank>
    {
        //public RankMap()
        //{
        //    ToTable("Ranks");
        //    HasKey(o => o.Id);
        //}

        public void Configure(EntityTypeBuilder<Rank> builder)
        {
            builder.ToTable("Ranks");
            builder.HasKey(b => b.Id);
           
        }
    }
}
