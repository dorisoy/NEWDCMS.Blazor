using DCMS.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DCMS.Infrastructure.Mapping.Main
{
    public class StockMap : IEntityTypeConfiguration<Stock>
    {

        //public StockMap()
        //{
        //    ToTable("Stocks");
        //    HasKey(o => o.Id);

        //    HasRequired(s => s.Product)
        //        .WithMany(s => s.Stocks)
        //        .HasForeignKey(s => s.ProductId);


        //    HasRequired(s => s.WareHouse)
        //        .WithMany(s => s.Stocks)
        //        .HasForeignKey(s => s.WareHouseId);
        //}

        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks");
            builder.HasKey(b => b.Id);

            builder.HasOne(s => s.Product)
                .WithMany(s => s.Stocks)
                .HasForeignKey(s => s.ProductId).IsRequired();


            //builder.HasOne(s => s.WareHouse)
            //    .WithMany(s => s.Stocks)
            //    .HasForeignKey(s => s.WareHouseId).IsRequired();

           
        }
    }

    public class StockInOutRecordMap : IEntityTypeConfiguration<StockInOutRecord>
    {


        //public StockInOutRecordMap()
        //{
        //    Ignore(c => c.DirectionEnum);

        //    ToTable("StockInOutRecords");
        //    HasKey(o => o.Id);

        //}

        public void Configure(EntityTypeBuilder<StockInOutRecord> builder)
        {
            builder.ToTable("StockInOutRecords");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.DirectionEnum);
           
        }
    }


    public class StockFlowMap : IEntityTypeConfiguration<StockFlow>
    {

        //public StockFlowMap()
        //{
        //    ToTable("StockFlows");
        //    HasKey(o => o.Id);

        //    HasRequired(s => s.Stock)
        //        .WithMany(s => s.StockFlows)
        //        .HasForeignKey(s => s.StockId);
        //}


        public void Configure(EntityTypeBuilder<StockFlow> builder)
        {
            builder.ToTable("StockFlows");
            builder.HasKey(b => b.Id);
            builder.HasOne(s => s.Stock)
              .WithMany(s => s.StockFlows)
              .HasForeignKey(s => s.StockId).IsRequired();
           
        }
    }


    /// <summary>
    /// 出入库流水映射
    /// </summary>
    public partial class StockInOutRecordStockFlowMap : IEntityTypeConfiguration<StockInOutRecordStockFlow>
    {
        //public StockInOutRecordStockFlowMap()
        //{
        //    ToTable("StockInOutRecords_StockFlows_Mapping");

        //    HasRequired(o => o.StockFlow)
        //         .WithMany()
        //         .HasForeignKey(o => o.StockFlowId);

        //    HasRequired(o => o.StockInOutRecord)
        //        .WithMany(m => m.StockInOutRecordStockFlows)
        //        .HasForeignKey(o => o.StockInOutRecordId);

        //}

        public void Configure(EntityTypeBuilder<StockInOutRecordStockFlow> builder)
        {

            builder.ToTable("StockInOutRecords_StockFlows_Mapping");
            builder.HasKey(mapping => new { mapping.StockInOutRecordId, mapping.StockFlowId });

            builder.Property(mapping => mapping.StockInOutRecordId);
            builder.Property(mapping => mapping.StockFlowId);

            builder.HasOne(mapping => mapping.StockFlow)
                .WithMany()
                .HasForeignKey(mapping => mapping.StockFlowId)
                .IsRequired();

            builder.HasOne(mapping => mapping.StockInOutRecord)
               .WithMany(customer => customer.StockInOutRecordStockFlows)
                .HasForeignKey(mapping => mapping.StockInOutRecordId)
                .IsRequired();



           
        }
    }



    /// <summary>
    /// 用于商品出入库明细记录
    /// </summary>
    public class StockInOutDetailsMap : IEntityTypeConfiguration<StockInOutDetails>
    {
        public void Configure(EntityTypeBuilder<StockInOutDetails> builder)
        {
            builder.ToTable("StockInOutDetails");
            builder.HasKey(b => b.Id);
           
        }
    }

}
