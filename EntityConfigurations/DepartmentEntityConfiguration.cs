// Configurations/DepartmentConfiguration.cs
using EmployeeAndDepartmentManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Xml.Linq;

namespace EmployeeAndDepartmentManagementSystem.Configurations
{
    public class DepartmentEntityConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            // set table name

            builder.ToTable("Departments");

            // Set primary key
            builder.HasKey(x => x.Id);

            // Set Identity
            builder.Property(x => x.Id).UseIdentityColumn();

            //Not Null

            builder.Property(x => x.Name).IsRequired(false);

            builder.Property(x => x.Description).IsRequired(false);

            // Length

            builder.Property(x=>x.Name).HasMaxLength(50);

            //Nvarchar Manpluation

            builder.Property(x => x.Name).IsUnicode(false); 
            //Default Values
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreationDate).HasDefaultValueSql("getdate()");

            // Name
            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Description
            builder.Property(d => d.Description)
                .HasMaxLength(500);

            // Relationship
            builder.HasMany<Employee>().WithOne().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.NoAction); ;

        }
    }
}
