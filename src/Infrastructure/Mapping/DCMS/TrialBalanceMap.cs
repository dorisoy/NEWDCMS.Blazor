using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{

    /// <summary>
    /// 科目余额(试算表)
    /// </summary>
    public partial class TrialBalanceMap : IEntityTypeConfiguration<TrialBalance>
    {

        //public TrialBalanceMap()
        //{
        //    ToTable("TrialBalances");
        //    HasKey(o => o.Id);
        //}

        public void Configure(EntityTypeBuilder<TrialBalance> builder)
        {
            builder.ToTable("TrialBalances");
            builder.HasKey(b => b.Id);

            builder.Ignore(b => b.BillId);
            builder.Ignore(b => b.CollectionAmount);

           
        }
    }


    /// <summary>
    /// 利润表
    /// </summary>
    public partial class ProfitSheetMap : IEntityTypeConfiguration<ProfitSheet>
    {

        public void Configure(EntityTypeBuilder<ProfitSheet> builder)
        {
            builder.ToTable("ProfitSheets");
            builder.HasKey(b => b.Id);

            builder.Ignore(b => b.LineNum);
            builder.Ignore(b => b.BillId);
            builder.Ignore(b => b.CollectionAmount);

           
        }
    }


    /// <summary>
    /// 资产负债表
    /// </summary>
    public partial class BalanceSheetMap : IEntityTypeConfiguration<BalanceSheet>
    {

        public void Configure(EntityTypeBuilder<BalanceSheet> builder)
        {
            builder.ToTable("BalanceSheets");
            builder.HasKey(b => b.Id);

            builder.Ignore(b => b.LineNum);
            builder.Ignore(b => b.BillId);
            builder.Ignore(b => b.CollectionAmount);

           
        }
    }


}
