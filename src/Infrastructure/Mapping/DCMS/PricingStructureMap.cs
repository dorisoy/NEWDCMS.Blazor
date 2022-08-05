using DCMS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    public partial class PricingStructureMap : IEntityTypeConfiguration<PricingStructure>
    {
        //public PricingStructureMap()
        //{
        //    ToTable("PricingStructures");
        //    HasKey(o => o.Id);

        //}

        public void Configure(EntityTypeBuilder<PricingStructure> builder)
        {
            builder.ToTable("PricingStructures");
            builder.HasKey(b => b.Id);
           
        }
    }

}
