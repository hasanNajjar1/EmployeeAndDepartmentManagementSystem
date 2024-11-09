using System.Collections.Generic;
using System.Reflection;
using System.Security.Principal;

namespace EmployeeAndDepartmentManagementSystem.Entities
{
    public class Admin : MainEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
