using EmployeeAndDepartmentManagementSystem.DTOs.Departments.Request;
using EmployeeAndDepartmentManagementSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EmployeeAndDepartmentManagementSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IDepartmentService _departmentService;
		private readonly IEmployeeService _employeeService;

		public AdminController(IDepartmentService departmentService, IEmployeeService employeeService)
		{
			_departmentService = departmentService;
			_employeeService = employeeService;
		}
		/// <summary>
		/// Retrieves a department by its ID.
		/// </summary>
		/// <param name="departmentId">The ID of the department to retrieve.</param>
		/// <returns>ActionResult containing the department data if found, otherwise NotFound.</returns>
		[HttpGet]
		[Route("GetDepartmentById/{departmentId}")]
		public async Task<IActionResult> GetDepartmentById(int departmentId)
		{
			try
			{
				Log.Information("Retrieving department with ID: {DepartmentId}", departmentId);
				var department = await _departmentService.GetDepartmentById(departmentId);

				if (department == null)
				{
					Log.Warning("Department with ID {DepartmentId} not found", departmentId);
					return NotFound("Department not found");
				}

				Log.Information("Department with ID {DepartmentId} retrieved successfully", departmentId);
				return Ok(department);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while retrieving the department with ID: {DepartmentId}", departmentId);
				return StatusCode(500, "An error occurred while retrieving the department.");
			}
		}
		/// <summary>
		/// Retrieves all departments.
		/// </summary>
		/// <returns>ActionResult containing a list of all departments.</returns>
		[HttpGet]
		[Route("GetAllDepartments")]
		public async Task<IActionResult> GetAllDepartments(string token)
		{
			try
			{
				Log.Information("Attempting to retrieve all departments from the database.");
				var departments = await _departmentService.GetAllDepartments();

				if (departments == null || !departments.Any())
				{
					Log.Warning("No departments found.");
					return NotFound("No departments found.");
				}

				Log.Information("Successfully retrieved all departments.");
				return Ok(departments);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while retrieving all departments.");
				return StatusCode(500, "Internal server error");
			}
		}
		/// <summary>
		/// Retrieves all employees by department ID.
		/// </summary>
		/// <param name="departmentId">The ID of the department to get employees for.</param>
		/// <returns>List of employees in the specified department.</returns>
		[HttpGet]
		[Route("GetAllEmployeesByDepartmentId/{departmentId}")]
		public async Task<IActionResult> GetAllEmployeesByDepartmentId(int departmentId)
		{
			try
			{
				Log.Information("Retrieving all employees in department ID: {DepartmentId}", departmentId);
				var employees = await _employeeService.GetAllEmployeesByDepartmentId(departmentId);

				if (employees == null || !employees.Any())
				{
					Log.Warning("No employees found for department ID: {DepartmentId}", departmentId);
					return NotFound("No employees found for the specified department.");
				}

				Log.Information("Employees retrieved successfully for department ID: {DepartmentId}", departmentId);
				return Ok(employees);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while retrieving employees for department ID: {DepartmentId}", departmentId);
				return StatusCode(500, "An error occurred while retrieving employees.");
			}
		}
		/// <summary>
		/// Retrieves employee information within a specific department based on client needs.
		/// </summary>
		/// <param name="departmentId">The department ID to search within.</param>
		/// <param name="includeNames">Include employee names if true.</param>
		/// <param name="includePhones">Include employee phone numbers if true.</param>
		/// <param name="includeEmails">Include employee emails if true.</param>
		/// <returns>A list of employees with requested information.</returns>
		[HttpGet("GetAllEmployeeInfoInSpecificDepartment")]
		public async Task<IActionResult> GetAllEmployeeInfoInSpecificDepartment(int departmentId, bool includeNames = false, bool includePhones = false, bool includeEmails = false)
		{
			try
			{
				Log.Information("Retrieving employee information for department ID: {DepartmentId}", departmentId);
				var employees = await _employeeService.GetAllEmployeeInfoInSpecificDepartment(departmentId, includeNames, includePhones, includeEmails);

				if (employees == null || !employees.Any())
				{
					Log.Warning("No employees found in department ID {DepartmentId}", departmentId);
					return NotFound($"No employees found in department with ID {departmentId}.");
				}

				Log.Information("Successfully retrieved employee information for department ID {DepartmentId}", departmentId);
				return Ok(employees);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while retrieving employee information for department ID {DepartmentId}", departmentId);
				return StatusCode(500, "An error occurred while processing your request.");
			}
		}
		/// <summary>
		/// Creates a new department.
		/// </summary>
		/// <param name="input">Data required to create a department.</param>
		/// <returns>ActionResult indicating success or failure.</returns>
		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDTO input)
		{
			if (!ModelState.IsValid)
			{
				Log.Warning("Invalid model state for department creation: {@ModelState}", ModelState);
				return BadRequest(ModelState);
			}

			try
			{
				Log.Information("Starting department creation for: {@Department}", input);
				await _departmentService.CreateDepartement(input);

				Log.Information("Department created successfully: {DepartmentName}", input.Name);
				return StatusCode(StatusCodes.Status201Created, "Department created successfully.");
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while creating the department.");
				return StatusCode(500, ex.InnerException?.Message);
			}
		}
		/// <summary>
		/// Creates a new employee within a specified department.
		/// </summary>
		/// <param name="employeeCreateDto">Data for the new employee.</param>
		/// <returns>ActionResult containing the created employee data.</returns>
		[HttpPost]
		[Route("CreateEmployee")]
		public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDTO employeeCreateDto)
		{
			try
			{
				Log.Information("Attempting to create employee: {FirstName} {LastName}",
								employeeCreateDto.FirstName, employeeCreateDto.LastName);
				var createdEmployee = await _employeeService.CreateEmployee(employeeCreateDto);
				Log.Information("Employee {FirstName} {LastName} created successfully",
								createdEmployee.FirstName, createdEmployee.LastName);

				return CreatedAtAction(nameof(CreateEmployee), new { id = createdEmployee.Id }, createdEmployee);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while creating the employee: {FirstName} {LastName}",
						  employeeCreateDto.FirstName, employeeCreateDto.LastName);
				return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
			}
		}
		/// <summary>
		/// Updates an existing department.
		/// </summary>
		/// <param name="input">Data required to update the department.</param>
		/// <returns>ActionResult indicating success or failure.</returns>
		[HttpPut]
		[Route("[action]")]
		public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentDTO input)
		{
			if (!ModelState.IsValid)
			{
				Log.Warning("Invalid model state for department update: {@ModelState}", ModelState);
				return BadRequest(ModelState);
			}
			try
			{
				Log.Information("Starting department update for ID: {DepartmentId}", input.Id);
				var result = await _departmentService.UpdateDepartment(input);
				if (!result)
				{
					Log.Warning("Department with ID {DepartmentId} not found.", input.Id);
					return NotFound($"Department with ID {input.Id} not found.");
				}
				Log.Information("Department updated successfully: {DepartmentName}", input.Name);
				return Ok("Department updated successfully.");
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while updating the department.");
				return StatusCode(500, ex.InnerException?.Message);
			}
		}
		/// <summary>
		/// Updates an existing employee's details.
		/// </summary>
		/// <param name="employeeDto">Data required to update the employee.</param>
		/// <returns>ActionResult containing the updated employee data.</returns>
		[HttpPut]
		[Route("UpdateSpecificEmployee")]
		public async Task<IActionResult> UpdateSpecificEmployee([FromBody] UpdateEmployeeDTO employeeDto)
		{
			try
			{
				Log.Information("Updating employee with ID: {EmployeeId}", employeeDto.Id);
				var updatedEmployee = await _employeeService.UpdateSpecificEmployee(employeeDto);

				Log.Information("Employee with ID {EmployeeId} updated successfully", employeeDto.Id);
				return Ok(updatedEmployee);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while updating the employee with ID: {EmployeeId}", employeeDto.Id);
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// Deletes an existing department by ID.
		/// </summary>
		/// <param name="departmentId">ID of the department to delete.</param>
		/// <returns>ActionResult indicating success or failure.</returns>
		[HttpDelete]
		[Route("[action]/{departmentId}")]
		public async Task<IActionResult> DeleteDepartment(int departmentId)
		{
			try
			{
				Log.Information("Starting deletion process for department with ID: {DepartmentId}", departmentId);
				var result = await _departmentService.DeleteDepartment(departmentId);
				if (!result)
				{
					Log.Warning("Department with ID {DepartmentId} not found.", departmentId);
					return NotFound($"Department with ID {departmentId} not found.");
				}

				Log.Information("Department with ID {DepartmentId} deleted successfully.", departmentId);
				return Ok("Department deleted successfully.");
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while deleting the department.");
				return StatusCode(500, ex.InnerException?.Message);
			}
		}
		/// <summary>
		/// Deletes an employee by ID.
		/// </summary>
		/// <param name="employeeId">The ID of the employee to delete.</param>
		/// <returns>ActionResult indicating the outcome of the deletion.</returns>
		[HttpDelete]
		[Route("DeleteEmployee/{employeeId}")]
		public async Task<IActionResult> DeleteEmployee(int employeeId)
		{
			try
			{
				Log.Information("Attempting to delete employee with ID: {EmployeeId}", employeeId);
				var isDeleted = await _employeeService.DeleteEmployee(employeeId);

				if (isDeleted)
				{
					Log.Information("Employee with ID {EmployeeId} deleted successfully.", employeeId);
					return Ok("Employee deleted successfully.");
				}
				else
				{
					Log.Warning("Employee with ID {EmployeeId} could not be deleted.", employeeId);
					return NotFound("Employee not found.");
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while deleting the employee with ID: {EmployeeId}", employeeId);
				return StatusCode(500, "An error occurred while deleting the employee.");
			}
		}

	}
}
