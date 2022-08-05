using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    public partial class PictureMap : IEntityTypeConfiguration<Picture>
    {
        //public PictureMap()
        //{
        //    ToTable("Pictures");
        //    HasKey(p => p.Id);
        //    Property(p => p.PictureBinary).IsMaxLength();
        //    Property(p => p.MimeType).IsRequired().HasMaxLength(40);
        //    Property(p => p.SeoFilename).HasMaxLength(300);
        //}

        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.ToTable("Pictures");
            builder.HasKey(b => b.Id);
            //builder.Property(p => p.PictureBinary).IsMaxLength();
            builder.Property(p => p.MimeType).IsRequired().HasMaxLength(40);
            builder.Property(p => p.SeoFilename).HasMaxLength(300);
           
        }
    }
}