//// Controllers/AuthController.cs

//using HomeServiceApp.Models;
//using Microsoft.AspNetCore.Cryptography.KeyDerivation;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using System.Security.Cryptography;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity.Data;

//namespace HomeServiceApp.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly HomeServiceDBContext _context;
//        private readonly IConfiguration _configuration;

//        public AuthController(HomeServiceDBContext context, IConfiguration configuration)
//        {
//            _context = context;
//            _configuration = configuration;
//        }

//        // Register user and hash password with salt
//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
//        {
//            var (hashedPassword, salt) = HashPassword(registerRequest.Password);

//            var newUser = new User
//            {
//                Name = registerRequest.Name,
//                Email = registerRequest.Email,
//                Phone = registerRequest.Phone,
//                Password = hashedPassword, // Store hashed password
//                PasswordSalt = salt, // Store the salt
//                Address = registerRequest.Address,
//                Preferences = registerRequest.Preferences
//            };

//            _context.Users.Add(newUser);
//            await _context.SaveChangesAsync();

//            return Ok(new { message = "User registered successfully!" });
//        }

//        // Hash password and generate salt
//        private (string hashedPassword, string salt) HashPassword(string password)
//        {
//            byte[] salt;
//            using (var rng = new RNGCryptoServiceProvider())
//            {
//                salt = new byte[128 / 8];  // 128 bits
//                rng.GetBytes(salt);
//            }

//            // Hash the password with the salt
//            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
//                password: password,
//                salt: salt,
//                prf: KeyDerivationPrf.HMACSHA256,
//                iterationCount: 10000,
//                numBytesRequested: 256 / 8));

//            // Return both the hashed password and the salt
//            return (hashedPassword, Convert.ToBase64String(salt));
//        }
//    }
//}
