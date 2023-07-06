using MelonBookshelfApi.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MelonBookshelfApi.Controllers
{
    [ApiController]
    [Route("api")]
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AuthenticationController> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("users/register")]
        public async Task<IActionResult> Register(UserRegistrationRequestDto request)
        {
            _logger.LogInformation("User Registration request received.");

            var user = new IdentityUser { UserName = request.FirstName + request.LastName, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User registered successfully.");
                return Ok();
            }

            _logger.LogError("User registration failed.");
            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("users/login")]
        public async Task<IActionResult> Login(UserLoginRequestDto request)
        {
            _logger.LogInformation("User Login request received.");

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (result)
            {
                _logger.LogInformation("User login successful. Generating JWT token.");

                string token = GenerateToken(user);

                _logger.LogInformation("JWT token generated successfully.");
                return Ok(new { Token = token });
            }
            else
            {
                _logger.LogError("User login failed.");

                return BadRequest("User login failed.");
            }
        }

        private string GenerateToken(IdentityUser user)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"];

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}
