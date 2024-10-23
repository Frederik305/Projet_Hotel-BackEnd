using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Metier;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Projet_Hotel_CodeBase.Controllers
{
    [ApiController]
    [Route("[controller]")]

   
    public class ClientController : ControllerBase
    {
        private ClientMetier clientMetier = new ClientMetier();


        private readonly ILogger<ClientController> _logger;
        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }

        [HttpPost("AddClient")]
        public IActionResult AddClient([FromBody]ClientDTO clientDTO)
        {
            try {
                ClientDTO nouveauClient = clientMetier.AddClient(clientDTO);
                return nouveauClient == null ? NotFound() : Ok(nouveauClient);
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost("Modifierlient")]
        public IActionResult ModifierClient([FromBody] ClientDTO clientDTO) 
        {
            try
            {
                ClientDTO nouveauClient = clientMetier.ModifierClient(clientDTO);
                return nouveauClient == null ? NotFound() : Ok(nouveauClient);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        
        [HttpGet("GetClient")]
        public ClientDTO[] GetClient([FromQuery] string CliNom, [FromQuery] string CliPrenom)
        {

            return serviceClient.GetClient(new ClientDTO { CliNom=CliNom, CliPrenom=CliPrenom});

        }
        [HttpGet("GetClients")]
        public ClientDTO[] GetClients()
        {

            return serviceClient.GetClients();


        }



    }
}
