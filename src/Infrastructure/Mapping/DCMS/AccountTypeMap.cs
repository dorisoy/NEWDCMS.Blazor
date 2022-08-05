using DCMS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    public partial class AccountingTypeMap : IEntityTypeConfiguration<AccountingType>
    {
        public void Configure(EntityTypeBuilder<AccountingType> builder)
        {
            builder.ToTable("AccountingTypes");
            builder.HasKey(b => b.Id);
           
        }
    }

    public partial class AccountingOptionMap : IEntityTypeConfiguration<AccountingOption>
    {
        //public AccountingOptionMap()
        //{
        //    ToTable("AccountingOptions");
        //    HasKey(ao => ao.Id);
        //    Property(ao => ao.Name).IsRequired();

        //    Ignore(ao => ao.Balance);

        //    HasRequired(sao => sao.AccountingType)
        //        .WithMany(sa => sa.AccountingOptions)
        //        .HasForeignKey(sao => sao.AccountingTypeId);
        //}

        public void Configure(EntityTypeBuilder<AccountingOption> builder)
        {
            builder.ToTable("AccountingOptions");
            builder.HasKey(b => b.Id);
            //builder.Ignore(ao => ao.Level);

            builder.Property(ao => ao.Name).IsRequired();

            builder.HasOne(sao => sao.AccountingType)
                .WithMany(sa => sa.AccountingOptions)
                .HasForeignKey(sao => sao.AccountingTypeId)
                .IsRequired();

           
        }
    }
}
