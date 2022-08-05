using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    /// <summary>
    /// 规格属性项映射
    /// </summary>
    public partial class SpecificationAttributeOptionMap : IEntityTypeConfiguration<SpecificationAttributeOption>
    {
        //public SpecificationAttributeOptionMap()
        //{
        //    ToTable("SpecificationAttributeOptions");
        //    HasKey(sao => sao.Id);
        //    Property(sao => sao.Name).IsRequired();

        //    //忽略换算数
        //    Ignore(sao => sao.ConvertedQuantity);
        //    Ignore(sao => sao.UnitConversion);

        //    HasRequired(sao => sao.SpecificationAttribute)
        //        .WithMany(sa => sa.SpecificationAttributeOptions)
        //        .HasForeignKey(sao => sao.SpecificationAttributeId);
        //}

        public void Configure(EntityTypeBuilder<SpecificationAttributeOption> builder)
        {
            builder.ToTable("SpecificationAttributeOptions");
            builder.HasKey(b => b.Id);
            builder.Property(sao => sao.Name).IsRequired();
            builder.Ignore(sao => sao.ConvertedQuantity);
            builder.Ignore(sao => sao.UnitConversion);

            builder.HasOne(sao => sao.SpecificationAttribute)
                .WithMany(sa => sa.SpecificationAttributeOptions)
                .HasForeignKey(sao => sao.SpecificationAttributeId)
                .IsRequired();

           
        }
    }
}