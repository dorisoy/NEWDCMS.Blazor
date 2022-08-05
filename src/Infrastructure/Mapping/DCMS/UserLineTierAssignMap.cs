using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    public partial class UserLineTierAssignMap : IEntityTypeConfiguration<UserLineTierAssign>
    {
        //public UserLineTierAssignMap()
        //{
        //    ToTable("LineTierUserMapping");
        //    HasKey(pc => pc.Id);


        //    HasRequired(pc => pc.LineTier)
        //        .WithMany()
        //        .HasForeignKey(pc => pc.LineTierId);

        //}

        public void Configure(EntityTypeBuilder<UserLineTierAssign> builder)
        {
            builder.ToTable("LineTierUserMapping");
            builder.HasKey(b => b.Id);

            builder.HasOne(pc => pc.LineTier)
                .WithMany()
                .HasForeignKey(pc => pc.LineTierId).IsRequired();

           
        }
    }
}
