using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Métier;
using System.Linq;

namespace Projet_Hotel_CodeBase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChambreController : ControllerBase   
    { 
        private ChambreMetier serviceChambre = new ChambreMetier();
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
        public void Post(ChambreDTO chambreDTO)
        {
            new ChambreMetier().AddChambre(chambreDTO);
        }

        [HttpPost("modifierChambre")]
        public IActionResult ModifierChambre(ChambreDTO chambreDTO)
        {

            try
            {
                ChambreDTO chambreModifier = serviceChambre.ModifierChambre(chambreDTO);


                return chambreModifier == null ? NotFound() : Ok(chambreModifier);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }


        }
    }
    
}
