using Microsoft.AspNetCore.Mvc;

namespace Projet_Hotel_CodeBase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(ILogger<ReservationController> logger)
        {
            _logger = logger;
        }

        /*[HttpGet(Name = "GetReservation")]
        public TypeChambre[] Get()
        {
            using (var db = new MyDbContext())
            {
                var entity = db.TypeChambres.ToArray();
                return entity;
            }
        }
        */
        /*[HttpPost("/CreeUnTypeChambre", Name = "CreeUnTypeChambre")]
        public void Post(string TYP_nomType, float TYP_prixPlancher, float TYP_prixPlafond, string TYP_description)
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
