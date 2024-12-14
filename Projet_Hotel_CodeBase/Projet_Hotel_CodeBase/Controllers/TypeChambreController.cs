using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Metier;
using Projet_Hotel_CodeBase.Métier;

namespace Projet_Hotel_CodeBase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TypeChambreController : ControllerBase
    {
        // Service métier pour la gestion des types de chambres
        private readonly TypeChambreMetier typeChambreMetier = new TypeChambreMetier();
        private readonly ILogger<TypeChambreController> _logger;

        public TypeChambreController(ILogger<TypeChambreController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [HttpGet(Name = "GetTypeChambre")]
        // Action pour obtenir tous les types de chambres
        public IActionResult GetTypeChambres()
        {
            try
            {
                // Appel du service métier pour récupérer tous les types de chambres
                TypeChambreDTO[] typeChambre = typeChambreMetier.GetTypeChambres();
                // Si aucun type de chambre n'est trouvé, renvoie une erreur, sinon renvoie la liste des types
                return typeChambre.Length == 0 ? NotFound() : Ok(typeChambre);
            }
            catch (Exception ex)
            {
                // En cas d'erreur, renvoie un message d'erreur
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet(Name = "/GetTypeChambreById")]
        // Action pour obtenir un type de chambre par son identifiant
        public IActionResult GetTypeChambreById([FromQuery] Guid PkTypId)
        {
            try
            {
                // Récupère un type de chambre spécifique par son identifiant
                TypeChambreDTO typeChambre = typeChambreMetier.GetTypeChambreById(PkTypId);
                return typeChambre == null ? NotFound() : Ok(typeChambre);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost(Name = "AddTypeChambre")]
        // Action pour ajouter un nouveau type de chambre
        public IActionResult AddTypeChambre(TypeChambreDTO typeChambreDTO)
        {
            try
            {
                // Ajoute un nouveau type de chambre via le service métier
                TypeChambreDTO newTypeChambre = typeChambreMetier.AddTypeChambre(typeChambreDTO);
                return newTypeChambre == null ? NotFound() : Ok(newTypeChambre);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
