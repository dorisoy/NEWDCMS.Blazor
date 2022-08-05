using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.Main
{
    public partial class PercentagePlanMap : IEntityTypeConfiguration<PercentagePlan>
    {
        //public PercentagePlanMap()
        //{
        //    ToTable("PercentagePlan");
        //    HasKey(pr => pr.Id);
        //    Ignore(o => o.PlanType);
        //}

        public void Configure(EntityTypeBuilder<PercentagePlan> builder)
        {
            builder.ToTable("PercentagePlan");
            builder.HasKey(b => b.Id);
            builder.Ignore(o => o.PlanType);
           
        }
    }


    public partial class PercentageMap : IEntityTypeConfiguration<Percentage>
    {
        //public PercentageMap()
        //{
        //    ToTable("Percentage");
        //    HasKey(pr => pr.Id);
        //    Ignore(o => o.CalCulateMethod);
        //    Ignore(o => o.QuantityCalCulateMethod);
        //    Ignore(o => o.CostingCalCulateMethod);

        //    HasRequired(pr => pr.PercentagePlan)
        //             .WithMany(pro => pro.Percentages)
        //             .HasForeignKey(pro => pro.PercentagePlanId);

        //}

        public void Configure(EntityTypeBuilder<Percentage> builder)
        {
            builder.ToTable("Percentage");
            builder.HasKey(b => b.Id);

            builder.HasOne(pr => pr.PercentagePlan)
                     .WithMany(pro => pro.Percentages)
                     .HasForeignKey(pro => pro.PercentagePlanId);

            builder.Ignore(o => o.CalCulateMethod);
            builder.Ignore(o => o.QuantityCalCulateMethod);
            builder.Ignore(o => o.CostingCalCulateMethod);

           
        }
    }


    public partial class PercentageRangeOptionMap : IEntityTypeConfiguration<PercentageRangeOption>
    {
        //public PercentageRangeOptionMap()
        //{
        //    ToTable("PercentageRangeOption");
        //    HasKey(pr => pr.Id);

        //    HasRequired(pr => pr.Percentage)
        //        .WithMany(pro => pro.PercentageRangeOptions)
        //        .HasForeignKey(pro => pro.PercentageId);
        //}

        public void Configure(EntityTypeBuilder<PercentageRangeOption> builder)
        {
            builder.ToTable("PercentageRangeOption");
            builder.HasKey(b => b.Id);

            builder.HasOne(pr => pr.Percentage)
                .WithMany(pro => pro.PercentageRangeOptions)
                .HasForeignKey(pro => pro.PercentageId);

           
        }
    }
}
