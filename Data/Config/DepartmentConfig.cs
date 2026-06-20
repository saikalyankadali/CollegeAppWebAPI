using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    internal class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable(nameof(Department));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(n => n.DepartmentName).IsRequired().HasMaxLength(100);
            builder.Property(n => n.Description).IsRequired(false).HasMaxLength(500);
            builder.HasData(new List<Department>()
            {
                new Department
                {
                    Id = 1,
                    DepartmentName = "CSE",
                    Description = "CSE Department"
                },
                new Department {
                    Id = 2,
                    DepartmentName = "ECE",
                    Description = "ECE Department"
                },
                new Department {
                    Id = 3,
                    DepartmentName = "EEE",
                    Description = "EEE Department"
                }
            });
        }
    }
}
