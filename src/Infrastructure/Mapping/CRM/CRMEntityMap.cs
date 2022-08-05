using DCMS.Domain.CRM;
using DCMS.Domain.OCMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.CRM
{

    public partial class CRM_RELATIONMap : IEntityTypeConfiguration<CRM_RELATION>
    {
        public void Configure(EntityTypeBuilder<CRM_RELATION> builder)
        {
            builder.ToTable("CRM_Relations");
            builder.HasKey(b => b.Id);
           
        }
    }

    public partial class CRM_RETURNMap : IEntityTypeConfiguration<CRM_RETURN>
    {
        public void Configure(EntityTypeBuilder<CRM_RETURN> builder)
        {
            builder.ToTable("CRM_Returns");
            builder.HasKey(b => b.Id);
           
        }
    }

    public partial class CRM_ORGMap : IEntityTypeConfiguration<CRM_ORG>
    {
        public void Configure(EntityTypeBuilder<CRM_ORG> builder)
        {
            builder.ToTable("CRM_Orgs");
            builder.HasKey(b => b.Id);
           
        }
    }

    public partial class CRM_BPMap : IEntityTypeConfiguration<CRM_BP>
    {
        public void Configure(EntityTypeBuilder<CRM_BP> builder)
        {
            builder.ToTable("CRM_Bps");
            builder.HasKey(b => b.Id);
           
        }
    }

    public partial class CRM_ZSNTM0040Map : IEntityTypeConfiguration<CRM_ZSNTM0040>
    {
        public void Configure(EntityTypeBuilder<CRM_ZSNTM0040> builder)
        {
            builder.ToTable("CRM_Zsntm0040");
            builder.HasKey(b => b.Id);
        }
    }

    public partial class CRM_HEIGHT_CONFMap : IEntityTypeConfiguration<CRM_HEIGHT_CONF>
    {
        public void Configure(EntityTypeBuilder<CRM_HEIGHT_CONF> builder)
        {
            builder.ToTable("CRM_HeightConfs");
            builder.HasKey(b => b.Id);
        }
    }

    public partial class CRM_BUSTATyMap : IEntityTypeConfiguration<CRM_BUSTAT>
    {
        public void Configure(EntityTypeBuilder<CRM_BUSTAT> builder)
        {
            builder.ToTable("CRM_Bustats");
            builder.HasKey(b => b.Id);
        }
    }
  
}
