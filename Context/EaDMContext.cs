

using EmployeeAndDepartmentManagementSystem.Configurations;
using EmployeeAndDepartmentManagementSystem.Entities;
using EmployeeAndDepartmentManagementSystem.EntityConfigurations;
using EmployeeAndDepartmentManagementSystem.EntityCongigurations;
using EmployeeAndDepartmentManagementSystem.Helper;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeAndDepartmentManagementSystem.Context
{
    public class EaDMContext : DbContext
    {
        //Tables 
        public DbSet<Admin> Admins { get; set; }
        public DbSet<LookupType> LookupTypes { get; set; }
        public DbSet<LookupItem> LookupItems { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public EaDMContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);




            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new LookupTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LookupItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());

            modelBuilder.Entity<LookupType>().HasData(
            new LookupType { Id = 1, Name = "PositionTypeId" });
           
            modelBuilder.Entity<LookupItem>().HasData(
            new LookupItem { LookupTypeId = 1 ,Id = 1, Name = "Sales and Marketing" },
            new LookupItem { LookupTypeId = 1 ,Id = 2, Name = "Finance and Accounting" },
            new LookupItem { LookupTypeId = 1 ,Id = 3, Name = "Administrative" });

            modelBuilder.Entity<Department>().HasData(new Department { Id = 1, Name = "IT Department" , CreationDate = DateTime.Now ,IsActive=true });

            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   Id = 1,
                   FirstName = "Admin",

                   LastName = "Account"
               ,
                   Email = EncryptionHelper.GenerateSHA384String("hasan1928@live.com"),

                   Password = EncryptionHelper.GenerateSHA384String("123qwe"),
                   CreationDate = DateTime.Now,
                   Salary= 0,
                   IsActive = true,
                   DepartmentId = 1
                    

               });
          


        }
    }
}
