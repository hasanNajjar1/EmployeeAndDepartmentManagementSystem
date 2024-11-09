using EmployeeAndDepartmentManagementSystem.Context;
using EmployeeAndDepartmentManagementSystem.DTOs.Departments.Request;
using EmployeeAndDepartmentManagementSystem.Entities;
using EmployeeAndDepartmentManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAndDepartmentManagementSystem.Implemintitions
{
	public class DepartmentService : IDepartmentService
	{
		private readonly EaDMContext _context;
		public DepartmentService(EaDMContext context)
		{
			_context = context;
		}
		// Create Department
		public async Task CreateDepartement(CreateDepartmentDTO input)
		{
			var newDepartment = new Department
			{
				Name = input.Name,
				Description = input.Description
			};
			_context.Departments.Add(newDepartment);
			var isSaved = await _context.SaveChangesAsync() > 0;
		}
		// Update Department
		public async Task<bool> UpdateDepartment(UpdateDepartmentDTO input)
		{
			var department = await _context.Departments
				.Where(d => d.Id == input.Id)
				.Select(d => d)
				.FirstOrDefaultAsync();

			if (department == null)
				return false;
			department.Name = input.Name;
			department.Description = input.Description;
			var isSaved = await _context.SaveChangesAsync() > 0;
			return isSaved;
		}
		// Delete Department
		public async Task<bool> DeleteDepartment(int departmentId)
		{
			var department = await _context.Departments
				.Where(d => d.Id == departmentId)
				.Select(d => d)
				.FirstOrDefaultAsync();
			if (department == null)
				return false;
			_context.Departments.Remove(department);
			var isDeleted = await _context.SaveChangesAsync() > 0;
			return isDeleted;
		}
		public async Task<List<DepartmentDTO>> GetAllDepartments()
		{
			var departments = from department in _context.Departments
							  select new DepartmentDTO
							  {
								  Id = department.Id,
								  Name = department.Name,
								  Description = department.Description,
								  CreationDate = department.CreationDate.ToShortDateString(),
							  };

			return await departments.ToListAsync();
		}

		public async Task<DepartmentDTO> GetDepartmentById(int departmentId)
		{
			var department = await (from d in _context.Departments
									where d.Id == departmentId
									select new DepartmentDTO
									{
										Id = d.Id,
										Name = d.Name,
										Description = d.Description,
										CreationDate = d.CreationDate.ToShortDateString()
									}).FirstOrDefaultAsync();

			if (department == null)
				throw new Exception("Department not found");

			return department;
		}
	}
}
