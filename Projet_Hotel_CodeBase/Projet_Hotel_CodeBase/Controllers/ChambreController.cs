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
        private ChambreMetier chambreMetier = new ChambreMetier();

        private readonly ILogger<ChambreController> _logger;

        public ChambreController(ILogger<ChambreController> logger)
        {
            _logger = logger;

        }

        [Authorize]
        [HttpGet("/GetChambres", Name = "GetChambres")]
        public IActionResult GetChambres()
        {
            try
            {
                ChambreDTO[] chambre = chambreMetier.RequestChambres();
                return chambre.Length == 0 ? NotFound() : Ok(chambre);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("/GetChambreByNum", Name = "GetChambreNum")]
        public IActionResult GetChambreByNum([FromQuery] ChambreDTO chambreDTO)
        {
            try
            {
                ChambreDTO[] chambre = chambreMetier.RequestChambreByNum(chambreDTO);
                return chambre.Length == 0 ? NotFound() : Ok(chambre);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("/GetChambreById")]
        public IActionResult GetChambreById([FromQuery] ChambreDTO chambreDTO)
        {
            try
            {
                ChambreDTO chambre = chambreMetier.GetChambreById(chambreDTO);
                return chambre == null ? NotFound() : Ok(chambre);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost("/CreeChambre", Name = "CreeChambre")]
        public IActionResult AddChambre([FromBody] ChambreDTO chambreDTO)
        {
            try
            {
                ChambreDTO newChambre = chambreMetier.AddChambre(chambreDTO);
                return newChambre == null ? NotFound() : Ok(newChambre);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost("modifierChambre")]
        public IActionResult ModifierChambre(ChambreDTO chambreDTO)
        {

            try
            {
                ChambreDTO chambreModifier = chambreMetier.ModifierChambre(chambreDTO);


                return chambreModifier == null ? NotFound() : Ok(chambreModifier);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }


        }

        [Authorize]
        [HttpGet("/GetAvailableChambre/")]
        public IActionResult GetAvailableRoom([FromQuery] ReservationDTO reservationDTO)
        {
            try
            {
                ChambreDTO[] roomsAvailable = chambreMetier.RequestRoomsAvailable(reservationDTO);


                return roomsAvailable == null ? NotFound() : Ok(roomsAvailable);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }


}
