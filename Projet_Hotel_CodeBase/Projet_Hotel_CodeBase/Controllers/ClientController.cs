using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

       

       
        [HttpPost("postClient")]
        public void EditClient(Client client,string prenom, string nom, string address, string telephone, string courriel, string motDePasse)
        {
            
                
            
           
        }

       

    }
}
