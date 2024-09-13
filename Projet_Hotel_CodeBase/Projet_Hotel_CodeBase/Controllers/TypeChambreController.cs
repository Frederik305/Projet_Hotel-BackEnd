using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            using (var context = new MyDbContext())
            {
                // Supposons que vous voulez récupérer l'entité avec Id = 1

                var entite = context.TypeChambres.ToArray();
                // Utilisation de la méthode Find pour récupérer l'entité
                Console.WriteLine(entite);
                return entite;

            }
        }
        [HttpPost(Name ="PostNewTypeChambre")]
        public IActionResult Post(string nomTypeChambre,double prixPlancher, double prixPlafond,string desc) {
            var typeChambreEntity = new TypeChambre();
            typeChambreEntity.PkTypId= Guid.NewGuid();
            typeChambreEntity.TypNomType= nomTypeChambre;
            typeChambreEntity.TypPrixPlancher= prixPlancher;
            typeChambreEntity.TypPrixPlafond= prixPlafond;
            typeChambreEntity.TypDescription= desc;
            try
            {
                using (var context = new MyDbContext())
                {
                    context.TypeChambres.Add(typeChambreEntity);
                    context.SaveChanges(); // Sauvegarde les changements dans la base de données
                }
                return Ok(); // Réponse 200 OK si l'opération réussit
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                // Log.Error(ex, "Error while adding new type de chambre");
                return StatusCode(500, "Internal server error"); // Réponse 500 en cas d'erreur
            }


        }


    }
}
