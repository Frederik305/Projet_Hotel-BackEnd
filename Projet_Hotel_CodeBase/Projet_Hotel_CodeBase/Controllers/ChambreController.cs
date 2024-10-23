using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Métier;
using Projet_Hotel_CodeBase.Metier;
using System.Linq;

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

        [HttpGet("/GetChambres", Name = "GetChambres")]
        public ChambreDTO[] Get() 
        {
            return new ChambreMetier().RequestChambres();
        }

        [HttpGet("/GetChambre/{numChambre}", Name = "GetChambreNum")]
        public ChambreDTO[] Get(short numChambre)
        {
            return new ChambreMetier().RequestChambreByNum(numChambre);
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
    }
}
