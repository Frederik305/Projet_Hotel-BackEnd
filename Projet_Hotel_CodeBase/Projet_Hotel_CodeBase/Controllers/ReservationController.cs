

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projet_Hotel_CodeBase.Metier;
using Projet_Hotel_CodeBase.DTO;

namespace Projet_Hotel_CodeBase.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ReservationController : ControllerBase
    { 
        private ReservationMetier serviceReservation=new ReservationMetier();
        private readonly ILogger<ReservationController> _logger;
        public ReservationController(ILogger<ReservationController> logger)
        {
            _logger = logger;
        }

        [HttpPost("modifierReservation")]
        public IActionResult ModifierReservation(ReservationDTO reservationDTO)
        {
           
            try
            {
                ReservationDTO reservationModifier = serviceReservation.ModifierReservation(reservationDTO);


                return reservationModifier == null ? NotFound() : Ok(reservationModifier);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }


        }

        [HttpPost("ajouterReservation")]
        public IActionResult AddReservation(ReservationDTO reservationDTO)
        {
            
            try
            {
                ReservationDTO nouvelleReservation= serviceReservation.AddReservation(reservationDTO);

                
                return nouvelleReservation == null ? NotFound() : Ok(nouvelleReservation);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }


        }
        [HttpGet("afficherReservation")]
        public IActionResult GetReservations()
        {
            try
            {
                ReservationDTO[] reservations = serviceReservation.GetReservations();
                return reservations == null ? NotFound() : Ok(reservations);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            //return serviceReservation.GetReservations();
        }


        [HttpPost("annulerReservation")]
        public IActionResult CancelReservation([FromBody] ReservationDTO reservationDTO)
        {
            if (reservationDTO.PkResId == null || reservationDTO.PkResId.Equals(""))
            {
                return BadRequest("Veuillez entrer une réservation");
            }

            try
            {
                serviceReservation.CancelReservation(reservationDTO);
                return Ok("La reservation à été annulé");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}

