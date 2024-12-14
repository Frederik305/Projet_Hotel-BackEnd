using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Metier;

namespace Projet_Hotel_CodeBase.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ReservationController : ControllerBase
    {
        // Service métier pour la gestion des réservations
        private ReservationMetier serviceReservation = new ReservationMetier();
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(ILogger<ReservationController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [HttpPost("modifierReservation")]
        // Action pour modifier une réservation
        public IActionResult ModifierReservation(ReservationDTO reservationDTO)
        {
            try
            {
                // Appel du service métier pour modifier la réservation
                ReservationDTO reservationModifier = serviceReservation.ModifierReservation(reservationDTO);
                // Si la réservation modifiée est nulle, renvoie une erreur, sinon renvoie la réservation modifiée
                return reservationModifier == null ? NotFound() : Ok(reservationModifier);
            }
            catch (Exception ex)
            {
                // En cas d'erreur, renvoie un message d'erreur
                return Conflict(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("ajouterReservation")]
        // Action pour ajouter une nouvelle réservation
        public IActionResult AddReservation(ReservationDTO reservationDTO)
        {
            try
            {
                // Appel du service métier pour ajouter une nouvelle réservation
                ReservationDTO nouvelleReservation = serviceReservation.AddReservation(reservationDTO);
                return nouvelleReservation == null ? NotFound() : Ok(nouvelleReservation);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("afficherReservation")]
        // Action pour obtenir toutes les réservations
        public IActionResult GetReservations()
        {
            try
            {
                // Récupère toutes les réservations via le service métier
                ReservationDTO[] reservations = serviceReservation.GetReservations();
                return reservations.Length == 0 ? NotFound() : Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("rechercherReservation")]
        // Action pour rechercher des réservations en fonction de critères
        public IActionResult RechercherReservations([FromQuery] ReservationDTO reservationDTO)
        {
            try
            {
                // Recherche des réservations en fonction des critères spécifiés dans reservationDTO
                ReservationDTO[] reservations = serviceReservation.SearchReservation(reservationDTO);
                return reservations.Length == 0 ? NotFound() : Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }

        [Authorize]
        [HttpPost("annulerReservation")]
        // Action pour annuler une réservation existante
        public IActionResult CancelReservation([FromBody] ReservationDTO reservationDTO)
        {
            // Si l'ID de réservation est vide, renvoie une erreur
            if (reservationDTO.PkResId.Equals(""))
            {
                return BadRequest("Veuillez entrer une réservation");
            }

            try
            {
                // Appel du service métier pour annuler la réservation
                serviceReservation.CancelReservation(reservationDTO);
                return Ok("La reservation à été annulé");
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

    }
}

