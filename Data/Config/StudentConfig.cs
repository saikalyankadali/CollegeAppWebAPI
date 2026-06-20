using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CollegeApp.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable(nameof(Student));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(n => n.Name).IsRequired().HasMaxLength(50);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(50);
            builder.Property(n => n.Phone).IsRequired().HasMaxLength(10);
            builder.HasData(new List<Student>()
            {
                new Student
                {
                    Id = 1,
                    Name = "Kalyan",
                    Email = "kalyan@gmail.com",
                    Phone = "9988776633",
                    DOB = new DateTime(1998,7,2)
                },
                new Student {
                    Id = 2,
                    Name = "Niha",
                    Email = "niha@gmail.com",
                    Phone = "8988776633",
                    DOB = new DateTime(1998,4,2)
                }
            });

            builder.HasOne(n => n.Department).WithMany(n => n.Students)
                .HasForeignKey(n => n.DepartmentId)
                .HasConstraintName("FK_Students_Department");
        }
    }
}
