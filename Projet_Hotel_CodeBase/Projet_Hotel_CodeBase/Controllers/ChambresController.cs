using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

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
            using (var context = new MyDbContext())
            {
                // Supposons que vous voulez récupérer l'entité avec Id = 1
                
                var entite = context.Chambres.ToArray();
                // Utilisation de la méthode Find pour récupérer l'entité
                Console.WriteLine(entite);
                return entite;
            
             }
        }
    }
}
