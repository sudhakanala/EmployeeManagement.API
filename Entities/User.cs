namespace EmployeeManagement.API.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public required string FullName { get; set; }
        public required string PasswordHash { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public required string Role { get; set; }
        public required string Designation { get; set; }

    }
}
