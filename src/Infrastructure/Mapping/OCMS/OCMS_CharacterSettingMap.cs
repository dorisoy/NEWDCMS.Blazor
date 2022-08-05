using DCMS.Domain.OCMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCMS.Infrastructure.Mapping.OCMS
{
	public partial class OCMS_CharacterSettingMap : IEntityTypeConfiguration<OCMS_CharacterSetting>
	{
		public void Configure(EntityTypeBuilder<OCMS_CharacterSetting> builder)
		{
			builder.ToTable("OCMS_CharacterSetting");
			builder.HasKey(b => b.Id);
		}
	}

}
