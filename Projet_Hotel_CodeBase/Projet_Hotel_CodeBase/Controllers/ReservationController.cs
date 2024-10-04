

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
        public void ModifierReservation(Guid PkResAmodifier, DateTime dateDebut, DateTime dateFin)
        {
            new ReservationMetier().ModifierReservation(PkResAmodifier, dateDebut,dateFin);



        }

        [HttpPost("ajouterReservation")]
        public void AddReservation(ReservationDTO reservationDTO)
        {
            new ReservationMetier().AddReservation(reservationDTO);



        }
        [HttpGet("afficherReservation")]
        public ReservationDTO[] GetReservations()
        {
            return new ReservationMetier().GetReservations();
        }
        [HttpPost("CancelReservation")]
        public void CancelReservation(Guid PkResACancel)
        {
            new ReservationMetier().CancelReservation(PkResACancel);
        }

    }
}

