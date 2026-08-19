using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Services;

namespace EmployeeManagement.API.Controllers
{
    public class AuthController : IPasswordService
    {
        public string HashPassword(User user, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsValidPassword(User user, string password, string passwordHash)
        {
            throw new NotImplementedException();
        }
    }
}
