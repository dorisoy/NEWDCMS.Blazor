using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.Main
{
    public class WareHouseMap : IEntityTypeConfiguration<WareHouse>
    {
        public void Configure(EntityTypeBuilder<WareHouse> builder)
        {
            builder.ToTable("WareHouses");
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.WareHouseAccess);
           
        }

    }

}
