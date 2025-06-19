using HopeCellApis.Models;
using Microsoft.AspNetCore.Mvc;

using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace HopeCellApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly HopeCellDbContext _context;

        public AuthController(HopeCellDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var passwordHash = HashPassword(request.Password);

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.PasswordHash == passwordHash);

            if (user == null)
                return Unauthorized("Invalid email or password");

            return Ok("Login successful!");
        }

  
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash); // matches varchar(64) in SQL
        }

    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
