using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Metier;
using System.Net;

namespace Projet_Hotel_CodeBase.Controllers
{
    [ApiController]
    [Route("[controller]")]
   


        

        
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }

       

       
        [HttpPost("AddClient")]
        public void AddClient([FromBody]ClientDTO clientDTO)
        {
            
            new ClientMetier().AddClient(clientDTO);
            
           
        }
        [HttpGet("GetClient")]
        public ClientDTO[] GetClient([FromQuery] string CliNom, [FromQuery] string CliPrenom)
        {

            return new ClientMetier().GetClient(new ClientDTO { CliNom=CliNom, CliPrenom=CliPrenom});


        }
        [HttpGet("GetClients")]
        public ClientDTO[] GetClients()
        {

            return new ClientMetier().GetClients();


        }



    }
}
