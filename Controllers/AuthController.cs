using EmployeeManagement.API.Data;
using EmployeeManagement.API.DTOs;
using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController      : ControllerBase
    {
        private readonly IPasswordService _passwordService;

        private readonly EmployeeManagementDbContext _context;

        private readonly ITokenService _tokenService;
        public AuthController(IPasswordService passwordService, EmployeeManagementDbContext context, ITokenService tokenService)
        {
            _passwordService = passwordService;
            _context = context;
            _tokenService = tokenService;
        }
                
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            // Hash the plain-text password first
            var passwordHash = _passwordService.HashPassword(null, request.Password);

            // Create User entity with all required properties set in the initializer
            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Role = "Employee",
                Designation = "Employee",
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                PasswordHash = passwordHash
            };

            // Add user to database
            _context.Users.Add(user);

            // Save changes
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "User registered successfully",
                UserId = user.UserId
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            // 1. Find the user using email
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            // 2. Check whether user exists
            if (user == null)
            {
                return Unauthorized(new
                {
                    Message = "Invalid email or password"
                });
            }

            // 3. Verify password
            var isValidPassword = _passwordService.IsValidPassword(
                user,
                request.Password,
                user.PasswordHash);

            if (!isValidPassword)
            {
                return Unauthorized(new
                {
                    Message = "Invalid email or password"
                });
            }

            // 4. Generate JWT
            var token = _tokenService.GenerateToken(user);

            // 5. Return token
            return Ok(new
            {
                Message = "Login successful",
                Token = token
            });
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok(new
            {
                Message = "You are authenticated"
            });
        }
    }
}
