using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Métier;
namespace Projet_Hotel_CodeBase.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ChambreController : ControllerBase
    {
        // Instance de la classe métier qui gère les chambres
        private readonly ChambreMetier chambreMetier = new ChambreMetier();

        private readonly ILogger<ChambreController> _logger;

        public ChambreController(ILogger<ChambreController> logger)
        {
            _logger = logger;

        }

        // Action protégée par l'autorisation (les utilisateurs doivent être authentifiés)
        [Authorize]
        // Cette méthode récupère toutes les chambres
        [HttpGet("/GetChambres", Name = "GetChambres")]
        public IActionResult GetChambres()
        {
            try
            {
                // Récupère la liste des chambres depuis la couche métier
                ChambreDTO[] chambre = chambreMetier.RequestChambres();
                // Si aucune chambre n'est trouvée, renvoie un code 404 NotFound, sinon renvoie les chambres
                return chambre.Length == 0 ? NotFound() : Ok(chambre);
            }
            catch (Exception ex)
            {
                // En cas d'erreur, renvoie une erreur BadRequest avec le message
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        // Cette méthode récupère une chambre par son numéro
        [HttpGet("/GetChambreByNum", Name = "GetChambreNum")]
        public IActionResult GetChambreByNum([FromQuery] ChambreDTO chambreDTO)
        {
            try
            {
                // Récupère la chambre avec le numéro demandé depuis la couche métier
                ChambreDTO[] chambre = chambreMetier.RequestChambreByNum(chambreDTO);
                return chambre.Length == 0 ? NotFound() : Ok(chambre);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        // Cette méthode récupère une chambre par son identifiant
        [HttpGet("/GetChambreById")]
        public IActionResult GetChambreById([FromQuery] ChambreDTO chambreDTO)
        {
            try
            {
                // Récupère la chambre par son ID depuis la couche métier
                ChambreDTO chambre = chambreMetier.GetChambreById(chambreDTO);
                return chambre == null ? NotFound() : Ok(chambre);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        // Cette méthode permet de créer une nouvelle chambre
        [HttpPost("/CreeChambre", Name = "CreeChambre")]
        public IActionResult AddChambre([FromBody] ChambreDTO chambreDTO)
        {
            try
            {
                // Ajoute la chambre via la couche métier
                ChambreDTO chambre = chambreMetier.AddChambre(chambreDTO);
                return chambre == null ? NotFound() : Ok(chambre);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("modifierChambre")]
        // Cette méthode permet de modifier une chambre existante
        public IActionResult ModifierChambre(ChambreDTO chambreDTO)
        {

            try
            {
                // Modifie la chambre via la couche métier
                ChambreDTO chambre = chambreMetier.ModifierChambre(chambreDTO);
                return chambre == null ? NotFound() : Ok(chambre);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }


        }

        [Authorize]
        // Cette méthode récupère les chambres disponibles en fonction des données de réservation
        [HttpGet("/GetAvailableChambre/")]
        public IActionResult GetAvailableRoom([FromQuery] ReservationDTO reservationDTO)
        {
            try
            {
                // Récupère les chambres disponibles via la couche métier
                ChambreDTO[] chambre = chambreMetier.RequestRoomsAvailable(reservationDTO);
                return chambre.Length == 0 ? NotFound() : Ok(chambre);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}
