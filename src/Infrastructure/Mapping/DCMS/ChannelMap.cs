using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    /// <summary>
    /// 渠道映射
    /// </summary>
    public partial class ChannelMap : IEntityTypeConfiguration<Channel>
    {
        //public ChannelMap()
        //{
        //    ToTable("Channels");
        //    HasKey(o => o.Id);
        //}

        public void Configure(EntityTypeBuilder<Channel> builder)
        {
            builder.ToTable("Channels");
            builder.HasKey(b => b.Id);
           
        }
    }
}
