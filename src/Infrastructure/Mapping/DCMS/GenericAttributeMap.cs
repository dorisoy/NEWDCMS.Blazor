using DCMS.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.Main
{

    public partial class GenericAttributeMap : IEntityTypeConfiguration<GenericAttribute>
    {
        //public GenericAttributeMap()
        //{
        //    ToTable("GenericAttributes");
        //    HasKey(ga => ga.Id);

        //    Property(ga => ga.KeyGroup).IsRequired().HasMaxLength(400);
        //    Property(ga => ga.Key).IsRequired().HasMaxLength(400);
        //    Property(ga => ga.Value).IsRequired();
        //}

        public void Configure(EntityTypeBuilder<GenericAttribute> builder)
        {
            builder.ToTable("GenericAttributes");
            builder.HasKey(b => b.Id);
            builder.Property(ga => ga.KeyGroup).IsRequired().HasMaxLength(400);
            builder.Property(ga => ga.Key).IsRequired().HasMaxLength(400);
            builder.Property(ga => ga.Value).IsRequired();
           
        }
    }
}