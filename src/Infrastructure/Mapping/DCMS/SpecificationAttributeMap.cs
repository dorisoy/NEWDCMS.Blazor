using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    /// <summary>
    /// 规格属性映射
    /// </summary>
    public partial class SpecificationAttributeMap : IEntityTypeConfiguration<SpecificationAttribute>
    {
        //public SpecificationAttributeMap()
        //{
        //    ToTable("SpecificationAttributes");
        //    HasKey(sa => sa.Id);
        //    Property(sa => sa.Name).IsRequired();
        //}

        public void Configure(EntityTypeBuilder<SpecificationAttribute> builder)
        {
            builder.ToTable("SpecificationAttributes");
            builder.HasKey(b => b.Id);
            builder.Property(sa => sa.Name).IsRequired();
           
        }
    }
}