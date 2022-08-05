using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    public partial class GiveQuotaMap : IEntityTypeConfiguration<GiveQuota>
    {
        //public GiveQuotaMap()
        //{
        //    ToTable("GiveQuota");
        //    HasKey(o => o.Id);

        //    //this.HasMany(g => g.GiveQuotaOptions)
        //    //    .WithRequired(g => g.GiveQuota);
        //}

        public void Configure(EntityTypeBuilder<GiveQuota> builder)
        {
            builder.ToTable("GiveQuota");
            builder.HasKey(b => b.Id);
           
        }
    }

    public partial class GiveQuotaOptionMap : IEntityTypeConfiguration<GiveQuotaOption>
    {
        //public GiveQuotaOptionMap()
        //{
        //    ToTable("GiveQuotaOption");
        //    HasKey(o => o.Id);

        //    HasRequired(g => g.GiveQuota)
        //        .WithMany(g => g.GiveQuotaOptions)
        //        .HasForeignKey(q => q.GiveQuotaId);
        //}
        public void Configure(EntityTypeBuilder<GiveQuotaOption> builder)
        {
            builder.ToTable("GiveQuotaOption");
            builder.HasKey(b => b.Id);

            builder.HasOne(g => g.GiveQuota)
                .WithMany(g => g.GiveQuotaOptions)
                .HasForeignKey(q => q.GiveQuotaId);

           
        }
    }


    public partial class GiveQuotaRecordsMap : IEntityTypeConfiguration<GiveQuotaRecords>
    {
        //public GiveQuotaRecordsMap()
        //{
        //    ToTable("GiveQuotaRecords");
        //    HasKey(o => o.Id);

        //    Ignore(o => o.GiveType);
        //}
        public void Configure(EntityTypeBuilder<GiveQuotaRecords> builder)
        {
            builder.ToTable("GiveQuotaRecords");
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.GiveType);

           
        }
    }

}
