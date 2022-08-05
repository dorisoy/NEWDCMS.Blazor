using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.Main
{
    /// <summary>
    /// 促销活动
    /// </summary>
    public partial class CampaignMap : IEntityTypeConfiguration<Campaign>
    {

        public void Configure(EntityTypeBuilder<Campaign> builder)
        {
            builder.ToTable("Campaigns");
            //builder.ToTable(nameof(Campaign));
            builder.HasKey(b => b.Id);
           
        }
    }


    /// <summary>
    ///  促销活动渠道映射
    /// </summary>
    public partial class CampaignChannelMap : IEntityTypeConfiguration<CampaignChannel>
    {

        public void Configure(EntityTypeBuilder<CampaignChannel> builder)
        {
            builder.ToTable("CampaignChannel");

            builder.HasKey(mapping => new { mapping.CampaignId, mapping.ChannelId });
            builder.Property(mapping => mapping.CampaignId);
            builder.Property(mapping => mapping.ChannelId);

            builder.HasOne(mapping => mapping.Campaign)
                .WithMany(customer => customer.CampaignChannels)
                .HasForeignKey(mapping => mapping.CampaignId)
                .IsRequired();

            builder.HasOne(mapping => mapping.Channel)
                .WithMany()
                .HasForeignKey(mapping => mapping.ChannelId)
                .IsRequired();

           
        }
    }


    /// <summary>
    ///  活动购买商品
    /// </summary>
    public partial class CampaignBuyProductMap : IEntityTypeConfiguration<CampaignBuyProduct>
    {
        public void Configure(EntityTypeBuilder<CampaignBuyProduct> builder)
        {
            builder.ToTable("CampaignBuyProducts");
            builder.HasKey(b => b.Id);

            builder.HasOne(b => b.Campaign)
             .WithMany(b => b.BuyProducts)
             .HasForeignKey(b => b.CampaignId)
             .OnDelete(DeleteBehavior.Cascade);

           
        }
    }


    /// <summary>
    /// 活动赠送商品
    /// </summary>
    public partial class CampaignGiveProductMap : IEntityTypeConfiguration<CampaignGiveProduct>
    {

        public void Configure(EntityTypeBuilder<CampaignGiveProduct> builder)
        {
            builder.ToTable("CampaignGiveProducts");
            builder.HasKey(b => b.Id);

            builder.HasOne(b => b.Campaign)
             .WithMany(b => b.GiveProducts)
             .HasForeignKey(b => b.CampaignId)
             .OnDelete(DeleteBehavior.Cascade);

           
        }
    }


}
