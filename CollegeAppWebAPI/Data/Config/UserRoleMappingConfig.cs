using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class UserRoleMappingConfig : IEntityTypeConfiguration<UserRoleMapping>
    {
        public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {
            builder.ToTable(nameof(UserRoleMapping));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasIndex(x => new {x.UserId, x.RoleId}, "UK_UserRoleMapping").IsUnique();
            builder.Property(n => n.UserId).IsRequired();
            builder.Property(n => n.RoleId).IsRequired();

            builder.HasOne(n => n.Role).WithMany(n => n.UserRoleMappings)
                .HasForeignKey(n => n.RoleId)
                .HasConstraintName("FK_UserRoleMappings_Roles");
            builder.HasOne(n => n.User).WithMany(n => n.UserRoleMappings)
                .HasForeignKey(n => n.UserId)
                .HasConstraintName("FK_UserRoleMappings_Users");
        }
    }
}