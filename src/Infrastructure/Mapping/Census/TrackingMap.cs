using DCMS.Domain.Census;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Census
{
    public partial class TrackingMap : IEntityTypeConfiguration<Tracking>
    {
        //public TrackingMap()
        //{
        //    ToTable("Tracking");
        //    HasKey(o => o.Id);
        //}

        public void Configure(EntityTypeBuilder<Tracking> builder)
        {
            builder.ToTable("Tracking");
            builder.HasKey(b => b.Id);
           
        }
    }
}
