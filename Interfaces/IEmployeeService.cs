using EmployeeAndDepartmentManagementSystem.DTOs.Employees.Response;
using EmployeeAndDepartmentManagementSystem.DTOs.Employees.Response.EmployeeAndDepartmentManagementSystem.DTO;

namespace EmployeeAndDepartmentManagementSystem.Interfaces
{
	public interface IEmployeeService
	{
		Task<EmployeeDTO> CreateEmployee(EmployeeCreateDTO employeeCreateDto);
		Task<EmployeeDTO> UpdateSpecificEmployee(UpdateEmployeeDTO employeeDto);
		Task<bool> DeleteEmployee(int employeeId);
		Task<List<EmployeeDTO>> GetAllEmployeesByDepartmentId(int departmentId);
		Task<List<FilteredEmployeeInfoDTO>> GetAllEmployeeInfoInSpecificDepartment(int departmentId, bool includeNames = false, bool includePhones = false, bool includeEmails = false);

	}
}
