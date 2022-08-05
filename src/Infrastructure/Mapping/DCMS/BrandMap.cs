using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.Main
{
    public partial class BrandMap : IEntityTypeConfiguration<Brand>
    {
        //public BrandMap()
        //{
        //    ToTable("Brands");
        //    HasKey(o => o.Id);

        //}

        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");
            builder.HasKey(b => b.Id);
           
        }
    }


}
