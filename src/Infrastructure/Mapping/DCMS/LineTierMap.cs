using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    public partial class LineTierMap : IEntityTypeConfiguration<LineTier>
    {
        //public LineTierMap()
        //{
        //    ToTable("LineTiers");
        //    HasKey(pc => pc.Id);

        //}

        public void Configure(EntityTypeBuilder<LineTier> builder)
        {
            builder.ToTable("LineTiers");
            builder.HasKey(b => b.Id);
           
        }
    }

    public partial class LineTierOptionMap : IEntityTypeConfiguration<LineTierOption>
    {
        //public LineTierOptionMap()
        //{
        //    ToTable("LineTierOptions");
        //    HasKey(pc => pc.Id);

        //    HasRequired(l => l.LineTier)
        //        .WithMany(lo => lo.LineTierOptions)
        //        .HasForeignKey(l => l.LineTierId);
        //}

        public void Configure(EntityTypeBuilder<LineTierOption> builder)
        {
            builder.ToTable("LineTierOptions");
            builder.HasKey(b => b.Id);
            builder.HasOne(l => l.LineTier)
                .WithMany(lo => lo.LineTierOptions)
                .HasForeignKey(l => l.LineTierId)
                .IsRequired();
           
        }
    }
}
