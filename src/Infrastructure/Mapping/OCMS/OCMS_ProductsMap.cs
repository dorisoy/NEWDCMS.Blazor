using DCMS.Domain.OCMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.OCMS
{
    public partial class OCMS_ProductsMap : IEntityTypeConfiguration<OCMS_Products>
    {
        public void Configure(EntityTypeBuilder<OCMS_Products> builder)
        {
            builder.ToTable("OCMS_Products");
            builder.HasKey(b => b.Id);

           
        }
    }
}
