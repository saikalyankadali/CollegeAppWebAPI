using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class UserTypeConfig : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.ToTable(nameof(UserType));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(n => n.UserTypeName).IsRequired().HasMaxLength(250);
            builder.Property(n => n.Description).IsRequired().HasMaxLength(250);

            builder.HasData(new List<UserType>()
            {
                new UserType
                {
                    Id = 1,
                    UserTypeName = "Student",
                    Description = "For students"
                },
                new UserType {
                    Id = 2,
                    UserTypeName = "Faculty",
                    Description = "For Faculty"
                },
                new UserType {
                    Id = 3,
                    UserTypeName = "Supporting Staff",
                    Description = "For Supporting Staff"
                },
                new UserType {
                    Id = 4,
                    UserTypeName = "Parents",
                    Description = "For Parents"
                }
            });
        }
    }
}