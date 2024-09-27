using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projet_Hotel_CodeBase.Metier;
using Projet_Hotel_CodeBase.DTO;

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
        public TypeChambreDTO[] Get()
        {
            return new TypeChambreMetier().GetTypeChambres();
        }

        [HttpPost(Name ="PostNewTypeChambre")]
        public void Post(TypeChambreDTO typeChambre) {
            
            new TypeChambreMetier().AddTypeChambre(typeChambre);



        }


    }
}
