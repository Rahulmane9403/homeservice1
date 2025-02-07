using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeServiceApp.Models;

namespace HomeServiceApp.Controllers
{
    [Route("api/auth")]  // Change the route here for authentication-related actions
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly HomeServiceDBContext _context;

        public UserController(HomeServiceDBContext context)
        {
            _context = context;
        }

        // GET: api/auth/users
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            return await _context.Users
                .Select(user => new User
                {
                    Id = user.Id,
                    Name = user.Name ?? "Unknown",
                    Email = user.Email,
                    Phone = user.Phone ?? "Not Provided",
                    Password = user.Password,
                    PasswordSalt = user.PasswordSalt ?? "",  // Handle null salt
                    Address = user.Address ?? "No Address",
                    Preferences = user.Preferences ?? "None"
                })
                .ToListAsync();
        }

        // GET: api/auth/users/5
        [HttpGet("users/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/auth/register (Register New User)
        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser(User user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'HomeServiceDBContext.Users' is null.");
            }

            // Check if user already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return Conflict(new { message = "User already exists" });
            }

            // Add new user to DB (no password hashing)
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Return created user
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // POST: api/auth/login (User Login)
        [HttpPost("login")]
        public async Task<ActionResult<object>> Login([FromBody] UserLoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest(new { message = "Login model cannot be null" });
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginModel.Email);

            // Check if user exists
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            // Check if the password matches (no hashing, using plain-text comparison)
            if (user.Password != loginModel.Password)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            // Return successful login response with user info
            return Ok(new { message = "Login successful", userId = user.Id, email = user.Email });
        }

        // DELETE: api/auth/users/5
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Remove user from DB
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    // Model for login request
    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
