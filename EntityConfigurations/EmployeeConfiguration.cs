using EmployeeAndDepartmentManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeAndDepartmentManagementSystem.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            //Set Primary Key 
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            //Default Values
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreationDate).HasDefaultValueSql("getdate()");

            // FirstName
            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            // LastName
            builder.Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            // Email
            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnType("nvarchar(250)");
            builder.HasIndex(a => a.Email)
                .IsUnique();

            // Password
            builder.Property(a => a.Password)
                .IsRequired()
                .HasMaxLength(250);

            // Phone
            builder.Property(e => e.Phone)
                .HasMaxLength(15); 
             
            // Salary
            builder.Property(e => e.Salary)
                .IsRequired();
        }
    }
}