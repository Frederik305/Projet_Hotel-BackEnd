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
        private TypeChambreMetier typeChambreMetier = new TypeChambreMetier();
        private readonly ILogger<TypeChambreController> _logger;

        public TypeChambreController(ILogger<TypeChambreController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTypeChambre")]
        public IActionResult Get()
        {
            try
            {
                TypeChambreDTO[] typeChambre = typeChambreMetier.GetTypeChambres();
                return typeChambre.Length == 0 ? NotFound() : Ok(typeChambre);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost(Name ="PostNewTypeChambre")]
        public IActionResult AddTypeChambre(TypeChambreDTO typeChambreDTO) {
            try
            {
                TypeChambreDTO newTypeChambre = typeChambreMetier.AddTypeChambre(typeChambreDTO);
                return newTypeChambre == null ? NotFound() : Ok(newTypeChambre);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
