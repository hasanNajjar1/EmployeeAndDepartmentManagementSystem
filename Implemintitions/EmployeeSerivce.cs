using EmployeeAndDepartmentManagementSystem.Context;
using EmployeeAndDepartmentManagementSystem.DTOs.Employees.Response;
using EmployeeAndDepartmentManagementSystem.DTOs.Employees.Response.EmployeeAndDepartmentManagementSystem.DTO;
using EmployeeAndDepartmentManagementSystem.Entities;
using EmployeeAndDepartmentManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

public class EmployeeService : IEmployeeService
{
	private readonly EaDMContext _context;

	public EmployeeService(EaDMContext context)
	{
		_context = context;
	}
	// Add new employee inside a specific department 
	public async Task<EmployeeDTO> CreateEmployee(EmployeeCreateDTO employeeCreateDto)
	{
		var department = await _context.Departments.FindAsync(employeeCreateDto.DepartmentId);
		if (department == null)
		{
			throw new Exception("Department does not exist");
		}
		var employee = new Employee
		{
			FirstName = employeeCreateDto.FirstName,
			LastName = employeeCreateDto.LastName,
			Email = employeeCreateDto.Email,
			Password = employeeCreateDto.Password,
			Phone = employeeCreateDto.Phone,
			Salary = employeeCreateDto.Salary,
			DepartmentId = employeeCreateDto.DepartmentId,
			PositionTypeId = employeeCreateDto.PositionTypeId
		};
		await _context.Employees.AddAsync(employee);
		await _context.SaveChangesAsync();
		return new EmployeeDTO
		{
			Id = employee.Id,
			FirstName = employee.FirstName,
			LastName = employee.LastName,
			Email = employee.Email,
			Phone = employee.Phone,
			Salary = employee.Salary,
			DepartmentId = employee.DepartmentId,
			PositionTypeId = employee.PositionTypeId
		};
	}

	public async Task<bool> DeleteEmployee(int employeeId)
	{
		var employee = await _context.Employees
				.Where(e => e.Id == employeeId)
				.Select(d => d)
				.FirstOrDefaultAsync();
		if (employee == null)
			throw new Exception("");
		_context.Employees.Remove(employee);
		var isDeleted = await _context.SaveChangesAsync() > 0;
		return isDeleted;

	}

	public async Task<EmployeeDTO> UpdateSpecificEmployee(UpdateEmployeeDTO employeeDto)
	{
		var employee = await _context.Employees.FindAsync(employeeDto.Id);

		if (employee == null)
			throw new Exception("Employee not found");

		employee.FirstName = employeeDto.FirstName;
		employee.LastName = employeeDto.LastName;
		employee.Email = employeeDto.Email;
		employee.Phone = employeeDto.Phone;
		employee.Salary = employeeDto.Salary;
		employee.DepartmentId = employeeDto.DepartmentId;
		employee.PositionTypeId = employeeDto.PositionTypeId;

		await _context.SaveChangesAsync();

		return new EmployeeDTO
		{
			Id = employee.Id,
			FirstName = employee.FirstName,
			LastName = employee.LastName,
			Email = employee.Email,
			Phone = employee.Phone,
			Salary = employee.Salary,
			DepartmentId = employee.DepartmentId,
			PositionTypeId = employee.PositionTypeId
		};
	}
	public async Task<List<EmployeeDTO>> GetAllEmployeesByDepartmentId(int departmentId)
	{
		var employees = from employee in _context.Employees
						where employee.DepartmentId == departmentId
						select new EmployeeDTO
						{
							Id = employee.Id,
							FirstName = employee.FirstName,
							LastName = employee.LastName,
							Email = employee.Email,
							Phone = employee.Phone,
							Salary = employee.Salary,
							DepartmentId = employee.DepartmentId,
							PositionTypeId = employee.PositionTypeId
						};

		return await employees.ToListAsync();
	}

	public async Task<List<FilteredEmployeeInfoDTO>> GetAllEmployeeInfoInSpecificDepartment(int departmentId, bool includeNames = false, bool includePhones = false, bool includeEmails = false)
	{
		var query = _context.Employees
			.Where(e => e.DepartmentId == departmentId)
			.Select(e => new FilteredEmployeeInfoDTO
			{
				Id = e.Id,
				Name = includeNames ? $"{e.FirstName} {e.LastName}" : null,
				Phone = includePhones ? e.Phone : null,
				Email = includeEmails ? e.Email : null
			});

		return await query.ToListAsync();
	}
}
