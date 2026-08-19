using EmployeeManagement.API.Entities;

namespace EmployeeManagement.API.Services
{
    public interface IPasswordService
    {    
        string HashPassword(User user, string password);

        bool IsValidPassword(User user, string password, string passwordHash);
    }
}
