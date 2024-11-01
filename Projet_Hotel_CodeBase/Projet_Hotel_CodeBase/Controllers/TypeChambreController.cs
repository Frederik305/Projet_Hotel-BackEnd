using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Metier;

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

        [Authorize]
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

        [Authorize]
        [HttpPost(Name = "PostNewTypeChambre")]
        public IActionResult AddTypeChambre(TypeChambreDTO typeChambreDTO)
        {
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
