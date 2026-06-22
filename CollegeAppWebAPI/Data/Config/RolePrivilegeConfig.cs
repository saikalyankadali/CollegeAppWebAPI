using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class RolePrivilegeConfig : IEntityTypeConfiguration<RolePrivilege>
    {
        public void Configure(EntityTypeBuilder<RolePrivilege> builder)
        {
            builder.ToTable(nameof(RolePrivilege));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(n => n.RolePrivilegeName).IsRequired().HasMaxLength(250);
            builder.Property(n => n.Description);
            builder.Property(n => n.IsActive).IsRequired();
            builder.Property(n => n.IsDeleted).IsRequired();
            builder.Property(n => n.CreatedDate).IsRequired();
            builder.Property(n => n.ModifiedDate).IsRequired();

            builder.HasOne(n => n.Role).WithMany(n => n.RolePrivileges)
                .HasForeignKey(n => n.RoleId)
                .HasConstraintName("FK_RolePrivileges_Roles");
        }
    }
}