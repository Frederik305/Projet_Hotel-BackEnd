

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public void EditReservation(Reservation reservation, DateTime dateDebut, DateTime dateFin)
        {




        }
    }
}

