using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Metier;

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
        public IActionResult AddClient([FromBody] ClientDTO clientDTO)
        {
            try
            {
                ClientDTO nouveauClient = clientMetier.AddClient(clientDTO);
                return nouveauClient == null ? NotFound() : Ok(nouveauClient);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost("ModifierClient")]
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

        [Authorize]
        [HttpGet("GetClientByName")]
        public IActionResult GetClientByName([FromQuery] ClientDTO clientDTO)
        {
            try
            {
                ClientDTO[] client = clientMetier.GetClientByName(clientDTO);
                return client.Length == 0 ? NotFound() : Ok(client);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Authorize]
        [HttpGet("GetClientById")]
        public IActionResult GetClientById([FromQuery] ClientDTO clientDTO)
        {
            try
            {
                ClientDTO client = clientMetier.GetClientById(clientDTO);
                return client == null ? NotFound() : Ok(client);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("GetClients")]
        public IActionResult GetClients()
        {
            try
            {
                ClientDTO[] client = clientMetier.GetClients();
                return client.Length == 0 ? NotFound() : Ok(client);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
