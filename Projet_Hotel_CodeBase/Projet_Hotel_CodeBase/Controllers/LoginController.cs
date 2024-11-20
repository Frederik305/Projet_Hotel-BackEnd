using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Métier;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Projet_Hotel_CodeBase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IConfiguration _configuration;
        private LoginMetier loginMetier = new LoginMetier();

        public LoginController(ILogger<LoginController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                // Check user credentials in DataBase
                LoginDTO nouveauLoginDTO = loginMetier.login(loginDTO);

                // generate token for user
                var token = GenerateAccessToken(loginDTO.LogCourriel);
                // return access token for user's use
                return Ok(new { AccessToken = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        private JwtSecurityToken GenerateAccessToken(string courriel)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, courriel),

            };

            // Create a JWT
            var token = new JwtSecurityToken(

                 issuer: _configuration["JwtSettings:Issuer"],
                 audience: _configuration["JwtSettings:Audience"],
                 claims: claims,
                 expires: DateTime.UtcNow.AddMinutes(60), // Token expiration time
                 signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])),
                     SecurityAlgorithms.HmacSha256));


            return token;
        }

    }
}
