using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 期末结账
    /// </summary>
    public partial class ClosingAccountsMap : IEntityTypeConfiguration<ClosingAccounts>
    {
        public void Configure(EntityTypeBuilder<ClosingAccounts> builder)
        {
            builder.ToTable("ClosingAccounts");
            builder.HasKey(b => b.Id);
           
        }
    }

    /// <summary>
    /// 成本变化明细汇总
    /// </summary>
    public partial class CostPriceSummeryMap : IEntityTypeConfiguration<CostPriceSummery>
    {
        public void Configure(EntityTypeBuilder<CostPriceSummery> builder)
        {
            builder.ToTable("CostPriceSummeries");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.Records);
           
        }
    }


    /// <summary>
    /// 成本变化明细记录
    /// </summary>
    public partial class CostPriceChangeRecordsMap : IEntityTypeConfiguration<CostPriceChangeRecords>
    {
        public void Configure(EntityTypeBuilder<CostPriceChangeRecords> builder)
        {
            builder.ToTable("CostPriceChangeRecords");
            builder.HasKey(b => b.Id);
           
        }
    }

}
