namespace EmployeeAndDepartmentManagementSystem.DTOs.Employees.Response
{
	public class EmployeeDTO
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string? Phone { get; set; }
		public float? Salary { get; set; }
		public int DepartmentId { get; set; }
		public int? PositionTypeId { get; set; }
	}
}
