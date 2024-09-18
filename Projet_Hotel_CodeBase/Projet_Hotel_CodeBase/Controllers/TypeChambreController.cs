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
        public IActionResult Post(string nomTypeChambre,double prixPlancher, double prixPlafond,string desc) {
            var typeChambreEntity = new TypeChambre();
            typeChambreEntity.PkTypId= Guid.NewGuid();
            typeChambreEntity.TypNomType= nomTypeChambre;
            typeChambreEntity.TypPrixPlancher= prixPlancher;
            typeChambreEntity.TypPrixPlafond= prixPlafond;
            typeChambreEntity.TypDescription= desc;
            return new MetierTypeChambre().SetTypeChambre(typeChambreEntity);


        }


    }
}
