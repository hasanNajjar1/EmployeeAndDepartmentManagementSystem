using EmployeeAndDepartmentManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeAndDepartmentManagementSystem.EntityCongigurations
{
    public class LookupTypeEntityConfiguration : IEntityTypeConfiguration<LookupType>
    {
        public void Configure(EntityTypeBuilder<LookupType> builder)
        {
            builder.ToTable("LookupTypes");
            //Set Primary Key 
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreationDate).HasDefaultValueSql("getdate()");

            //RelationShips
            builder.HasMany<LookupItem>().WithOne().HasForeignKey(x => x.LookupTypeId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
