using DCMS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.Main
{
    public partial class PrintTemplateMap : IEntityTypeConfiguration<PrintTemplate>
    {
        public void Configure(EntityTypeBuilder<PrintTemplate> builder)
        {
            builder.ToTable("PrintTemplates");
            builder.HasKey(b => b.Id);
            builder.Ignore(password => password.EPaperTypes);
           
        }
    }
}
