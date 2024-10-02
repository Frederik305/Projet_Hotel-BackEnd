using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.M�tier;
using System.Linq;

namespace Projet_Hotel_CodeBase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChambreController : ControllerBase
    {
        private readonly ILogger<ChambreController> _logger;

        public ChambreController(ILogger<ChambreController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/GetChambres", Name = "GetChambres")]
        public ChambreDTO[] Get() 
        {
            return new ChambreMetier().requestChambres();
        }

        [HttpGet("/GetChambre/{numChambre}", Name = "GetChambreNum")]
        public ChambreDTO[] Get(short numChambre)
        {
            return new ChambreMetier().requestChambreByNum(numChambre);
        }

        [HttpPost("/CreeChambre", Name = "CreeChambre")]
        public void Post(ChambreDTO chambreDTO)
        {
            new ChambreMetier().addChambre(chambreDTO);
        }
    }
}
