using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Projet_Hotel_CodeBase.Metier;

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

        [HttpGet(Name = "GetChambre")]
        public Chambre[] Get()
        {
            
            return new MetierChambre().GetChambres();

            
        }
        [HttpGet("chambre/",Name = "GetChambreNum")]
        public Chambre Get(short numChambre)
        {
            return new MetierChambre().GetChambre(numChambre);
        }
    }
}