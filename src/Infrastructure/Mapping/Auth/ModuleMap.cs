using DCMS.Domain.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DCMS.Infrastructure.Mapping.Auth
{
	public partial class ModuleMap : IEntityTypeConfiguration<Module>
    {
        //public ModuleMap()
        //{
        //    ToTable("Module");
        //    HasKey(pr => pr.Id);

        //    Ignore(o => o.LayoutPosition);

        //    HasOptional(c => c.ParentModule)
        //        .WithMany(c => c.ChildModules)
        //        .HasForeignKey(d => d.ParentId);

        //    //模块和权限 一对多关系(写法2)
        //    //this.HasMany(b => b.Permissions)
        //    // .WithRequired(p => p.module);

        //    //权限和模块
        //    //HasMany(pr => pr.UserRoles)
        //    //    .WithMany(cr => cr.Modules)
        //    //    .Map(m => m.ToTable("Module_Role_Mapping"));


        //    HasMany(pr => pr.UserRoles)
        //      .WithMany(cr => cr.Modules)
        //      .Map(m => m.MapLeftKey("Module_Id").MapRightKey("UserRole_Id").ToTable("Module_Role_Mapping"));
        //}

        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("Module");
            builder.HasKey(b => b.Id);

            builder.Ignore(o => o.ParentModule);
            builder.Ignore(o => o.ChildModules);
            builder.Ignore(o => o.LayoutPosition);

            //builder.HasOne(c => c.ParentModule)
            //    .WithMany(c => c.ChildModules)
            //    .HasForeignKey(d => d.ParentId);

           
        }

    }


    public partial class ModuleRoleMap : IEntityTypeConfiguration<ModuleRole>
    {
        public void Configure(EntityTypeBuilder<ModuleRole> builder)
        {
            builder.ToTable("ModuleRole");
            builder.HasKey(mapping => new { mapping.Module_Id, mapping.UserRole_Id });

            builder.Property(mapping => mapping.Module_Id);
            builder.Property(mapping => mapping.UserRole_Id);

            builder.HasOne(mapping => mapping.UserRole)
                .WithMany(role => role.ModuleRoles)
                .HasForeignKey(mapping => mapping.UserRole_Id)
                .IsRequired();

            builder.HasOne(mapping => mapping.Module)
                .WithMany(record => record.ModuleRoles)
                .HasForeignKey(mapping => mapping.Module_Id)
                .IsRequired();

            builder.Ignore(m => m.Id);

           
        }

    }
}
