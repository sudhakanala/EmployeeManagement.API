
using EmployeeManagement.API.Entities;

namespace EmployeeManagement.API.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
