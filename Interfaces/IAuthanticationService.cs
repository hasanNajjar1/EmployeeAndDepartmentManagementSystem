using EmployeeAndDepartmentManagementSystem.DTOs.Authantication.Request;

namespace EmployeeAndDepartmentManagementSystem.Interfaces
{
    public interface IAuthanticationService
    {
        Task<string> Login(LoginDTO input);
    }
}
