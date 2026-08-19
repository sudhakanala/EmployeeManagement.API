using EmployeeManagement.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Data
{
    public class EmployeeManagementDbContext : DbContext
    {
        public EmployeeManagementDbContext(
            DbContextOptions<EmployeeManagementDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

    }
}
