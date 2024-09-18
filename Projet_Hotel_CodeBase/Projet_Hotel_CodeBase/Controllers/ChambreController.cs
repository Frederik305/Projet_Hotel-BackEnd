using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Projet_Hotel_CodeBase.Métier;
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
        public Chambre[] Get() 
        {
            return new ChambreMetier().requestChambres();
        }

        [HttpGet("/GetChambre/{numChambre}", Name = "GetChambreNum")]
        public Chambre Get(short numChambre)
        {
            return new ChambreMetier().requestChambreByNum(numChambre);
        }

        [HttpPost("/PostChambre", Name = "PostChambre")]
        public void Post(short CHA_numero, bool CHA_etat, string CHA_autreInfo, Guid FK_TYP_id)
        {
            new ChambreMetier().addChambre(CHA_numero, CHA_etat, CHA_autreInfo, FK_TYP_id);
        }
    }
}
