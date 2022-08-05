using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    public partial class NewsPictureMap : IEntityTypeConfiguration<NewsPicture>
    {
        //public NewsPictureMap()
        //{
        //    ToTable("NewsPicture");
        //    HasKey(pp => pp.Id);
        //}

        public void Configure(EntityTypeBuilder<NewsPicture> builder)
        {
            builder.ToTable("NewsPicture");
            builder.HasKey(b => b.Id);
           
        }
    }
}