using MelonBookshelfApi.RequestDtos;
using MelonBookshelfApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelfApi.Controllers
{
    [ApiController]
    [Route("api")]
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<IdentityUser> userManager, ILogger<AuthenticationController> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("users/register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto request)
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
        public async Task<IActionResult> Login([FromBody] UserLoginDto request)
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

                string token = JwtTokenGenerator.GenerateToken(_configuration, user);

                _logger.LogInformation("JWT token generated successfully.");
                return Ok(new { Token = token });
            }
            else
            {
                _logger.LogError("User login failed.");

                return BadRequest("User login failed.");
            }
        }
    }
}
