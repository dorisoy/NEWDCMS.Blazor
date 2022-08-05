using DCMS.Domain.TSS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.TSS
{
    public class FeedbackMap : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.ToTable("Feedback");
            builder.HasKey(b => b.Id);

            builder.Property(mapping => mapping.FeedbackTyoe);

           
        }
    }

    public class MarketFeedbackMap : IEntityTypeConfiguration<MarketFeedback>
    {
        public void Configure(EntityTypeBuilder<MarketFeedback> builder)
        {
            builder.ToTable("MarketFeedback");
            builder.HasKey(b => b.Id);
           
        }
    }
}
