using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Projet_Hotel_CodeBase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TypeChambreController : ControllerBase
    {
        private readonly ILogger<TypeChambreController> _logger;

        public TypeChambreController(ILogger<TypeChambreController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTypeChambre")]
        public TypeChambre[] Get()
        {
            using (var db = new MyDbContext())
            {
                var entity = db.TypeChambres.ToArray();
                return entity;
            }
        }
        /*public void Post(string TYP_nomType, float TYP_prixPlancher, float TYP_prixPlafond, string TYP_description)
        {
            using (var db = new MyDbContext())
            {
                var typeChambreEntity = new TypeChambre();
                typeChambreEntity.PkTypId = Guid.NewGuid();
                typeChambreEntity.TypNomType = TYP_nomType;
                typeChambreEntity.TypPrixPlancher = TYP_prixPlancher;
                typeChambreEntity.TypPrixPlafond = TYP_prixPlafond;
                typeChambreEntity.TypDescription = TYP_description;

                db.TypeChambres.Add(typeChambreEntity);
                db.SaveChanges();
            }
        }*/
    }
}
