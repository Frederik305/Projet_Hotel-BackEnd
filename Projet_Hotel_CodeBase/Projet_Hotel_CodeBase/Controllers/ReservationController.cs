

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
        private readonly ILogger<ReservationController> _logger;
        public ReservationController(ILogger<ReservationController> logger)
        {
            _logger = logger;
        }

        [HttpPost("modifierReservation")]
        public void ModifierReservation(Guid PkResAmodifier, [FromBody]ReservationDTO reservationModifier)
        {
            new MetierReservation().ModifierReservation(PkResAmodifier, reservationModifier);



        }

        [HttpPost("ajouterReservation")]
        public void AddReservation(ReservationDTO reservationDTO)
        {
            new MetierReservation().AddReservation(reservationDTO);



        }
        [HttpGet("afficherReservation")]
        public ReservationDTO[] GetReservations()
        {
            return new MetierReservation().GetReservations();
        }

    }
}

