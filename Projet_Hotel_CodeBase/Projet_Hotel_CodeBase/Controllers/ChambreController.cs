using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Métier;
using Projet_Hotel_CodeBase.Metier;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Projet_Hotel_CodeBase;
using Microsoft.AspNetCore.Identity;
namespace Projet_Hotel_CodeBase.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ChambreController : ControllerBase   
    { 
        private ChambreMetier chambreMetier = new ChambreMetier();

        private readonly ILogger<ChambreController> _logger;
        private readonly IConfiguration _configuration;

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

        [HttpPost("/CreeChambre", Name = "CreeChambre")]
        public IActionResult Post([FromBody] ChambreDTO chambreDTO)
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
