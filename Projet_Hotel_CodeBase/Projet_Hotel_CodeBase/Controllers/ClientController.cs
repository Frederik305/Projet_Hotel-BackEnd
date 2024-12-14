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
        // Instance de la classe métier qui gère les clients
        private readonly ClientMetier clientMetier = new ClientMetier();

        private readonly ILogger<ClientController> _logger;
        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }

        // Cette méthode permet d'ajouter un nouveau client
        [HttpPost("AddClient")]
        public IActionResult AddClient([FromBody] ClientDTO clientDTO)
        {
            try
            {
                // Ajoute un nouveau client via la couche métier
                ClientDTO client = clientMetier.AddClient(clientDTO);
                // Si le client n'est pas créé, renvoie NotFound, sinon renvoie le client créé
                return client == null ? NotFound() : Ok(client);
            }
            catch (Exception ex)
            {
                // En cas d'erreur, renvoie un BadRequest avec le message d'erreur
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("ModifierClient")]
        // Cette méthode permet de modifier les informations d'un client existant
        public IActionResult ModifierClient([FromBody] ClientDTO clientDTO)
        {
            try
            {
                // Modifie les informations du client via la couche métier
                ClientDTO client = clientMetier.ModifierClient(clientDTO);
                return client == null ? NotFound() : Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("GetClientByEmail")]
        // Cette méthode permet de récupérer un client par son email
        public IActionResult GetClientByEmail([FromQuery] ClientDTO clientDTO)
        {
            try
            {
                // Récupère le client par email via la couche métier
                ClientDTO client = clientMetier.GetClientByEmail(clientDTO);
                return Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("GetClientByName")]
        // Cette méthode permet de récupérer un ou plusieurs clients par leur nom
        public IActionResult GetClientByName([FromQuery] ClientDTO clientDTO)
        {
            try
            {
                // Récupère les clients par nom via la couche métier
                ClientDTO[] client = clientMetier.GetClientByName(clientDTO);
                return client.Length == 0 ? NotFound() : Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [Authorize]
        [HttpGet("GetClientById")]
        // Cette méthode permet de récupérer un client par son identifiant
        public IActionResult GetClientById([FromQuery] ClientDTO clientDTO)
        {
            try
            {
                // Récupère le client par ID via la couche métier
                ClientDTO client = clientMetier.GetClientById(clientDTO);
                return client == null ? NotFound() : Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("GetClients")]
        // Cette méthode permet de récupérer la liste de tous les clients
        public IActionResult GetClients()
        {
            try
            {
                // Récupère la liste de tous les clients via la couche métier
                ClientDTO[] client = clientMetier.GetClients();
                return client.Length == 0 ? NotFound() : Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
