namespace EmployeeAndDepartmentManagementSystem.Entities
{
    public class Employee : MainEntity
    {
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public  string Email { get; set; }
        public  string Password { get; set; }
        public string? Phone { get; set; }
        public  float? Salary { get; set; }
        public int DepartmentId { get; set; }
        public int? PositionTypeId { get; set; }
    }
}
