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
                // Supposons que vous voulez r�cup�rer l'entit� avec Id = 1
                
                var entite = context.Chambres.ToArray();
                // Utilisation de la m�thode Find pour r�cup�rer l'entit�
                Console.WriteLine(entite);
                return entite;
            
             }
        }
    }
}
