using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Projet_Hotel_CodeBase.Metier;
using Projet_Hotel_CodeBase.DTO;
using Microsoft.EntityFrameworkCore;

namespace Projet_Hotel_CodeBase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChambresController : ControllerBase
    {
        

        private readonly ILogger<ChambresController> _logger;

        public ChambresController(ILogger<ChambresController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetChambres")]
        public Chambre[] Get()
        {
            
            return new MetierChambre().GetChambres();

            
        }
        [HttpGet("chambre/",Name = "GetChambreByNum")]
        public Chambre Get(short numChambre)
        {
            return new MetierChambre().GetChambre(numChambre);
        }

        
        [HttpPost("/CreeChambre", Name = "CreeChambre")]
        public Chambre CreateChambre([FromBody] ChambreDTO chambre)
        {
            
            return new MetierChambre().addChambre(chambre);
        }

    }
}
