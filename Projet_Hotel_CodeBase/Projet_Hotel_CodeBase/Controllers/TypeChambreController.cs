using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projet_Hotel_CodeBase.Metier;

namespace Projet_Hotel_CodeBase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TypeChambreController : ControllerBase
    {
        // GET: TypeChambreController
        private readonly ILogger<TypeChambreController> _logger;

        public TypeChambreController(ILogger<TypeChambreController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTypeChambre")]
        public TypeChambre[] Get()
        {
            return new MetierTypeChambre().GetTypeChambres();
        }

        [HttpPost(Name ="PostNewTypeChambre")]
        public void Post(TypeChambre typeChambre) {
            
            new MetierTypeChambre().AddTypeChambre(typeChambre);



        }


    }
}
