using DCMS.Domain.Census;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping
{

    public partial class TraditionMap : IEntityTypeConfiguration<Tradition>
    {
        //public TraditionMap()
        //{
        //    ToTable("Tradition");
        //    HasKey(o => o.Id);
        //}


        public void Configure(EntityTypeBuilder<Tradition> builder)
        {
            builder.ToTable("Tradition");
            builder.HasKey(b => b.Id);
           
        }
    }
}