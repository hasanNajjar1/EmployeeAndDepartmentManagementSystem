using EmployeeAndDepartmentManagementSystem.DTOs.Departments.Request;

namespace EmployeeAndDepartmentManagementSystem.Interfaces
{
	public interface IDepartmentService
	{
		Task CreateDepartement(CreateDepartmentDTO input);
		Task<bool> UpdateDepartment(UpdateDepartmentDTO input);
		Task<bool> DeleteDepartment(int departmentId);
		Task<List<DepartmentDTO>> GetAllDepartments();
		Task<DepartmentDTO> GetDepartmentById(int departmentId);

	}
}
