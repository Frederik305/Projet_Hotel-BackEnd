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
        // Instance de la couche métier pour la gestion des connexions
        private readonly LoginMetier loginMetier = new LoginMetier();

        // Constructeur pour injecter les services nécessaires : logger et configuration
        public LoginController(ILogger<LoginController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("login")]
        // Méthode d'authentification de l'utilisateur
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                // Vérifie les informations d'identification de l'utilisateur dans la base de données
                LoginDTO nouveauLoginDTO = loginMetier.login(loginDTO);

                // Génère un jeton d'accès pour l'utilisateur si les informations sont valides
                var token = GenerateAccessToken(loginDTO.LogCourriel);

                // Renvoie le jeton d'accès sous forme de réponse JSON
                return Ok(new { AccessToken = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (Exception ex)
            {
                // En cas d'erreur (comme des informations incorrectes), renvoie un message d'erreur
                return BadRequest(new { message = ex.Message });
            }

        }

        // Cette méthode génère un jeton JWT pour l'utilisateur
        private JwtSecurityToken GenerateAccessToken(string courriel)
        {
            // Création des informations du jeton (les "claims" ou revendications)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, courriel), // Ajout de l'email de l'utilisateur en tant que revendication

            };

            // Création du JWT avec les paramètres : émetteur, audience, revendiations, expiration, et signature
            var token = new JwtSecurityToken(
                             issuer: _configuration["JwtSettings:Issuer"],              // L'émetteur du jeton
                             audience: _configuration["JwtSettings:Audience"],          // L'audience du jeton
                             claims: claims,                                            // Les revendications associées à l'utilisateur
                             expires: DateTime.UtcNow.AddMinutes(120),                  // Durée de validité du jeton (2 heures)
                             signingCredentials: new SigningCredentials(
                                 new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])),     // Clé secrète pour la signature
                                 SecurityAlgorithms.HmacSha256)                                                                 // Algorithme de signature utilisé
            );
            // Retourne le jeton généré
            return token;
        }
    }
}
