using EmployeeAndDepartmentManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeAndDepartmentManagementSystem.EntityCongigurations
{
    public class LookupItemEntityTypeConfiguration : IEntityTypeConfiguration<LookupItem>
    {
        public void Configure(EntityTypeBuilder<LookupItem> builder)
        {
            builder.ToTable("LookupItems");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreationDate).HasDefaultValueSql("getdate()");

            //Relationships
           // builder.HasMany<Employee>().WithOne().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
