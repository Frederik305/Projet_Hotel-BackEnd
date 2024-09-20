using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Métier;

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

        /*[HttpGet(Name = "GetTypeChambre")]
        public TypeChambre[] Get()
        {
            using (var db = new MyDbContext())
            {
                var entity = db.TypeChambres.ToArray();
                return entity;
            }
        }*/

        [HttpPost("/CreeUnCompteClient", Name = "CreeUnCompteClient")]
        public void Post( ClientDTO clientDTO)
        {
            new ClientMetier().addClient(clientDTO);
        }

        [HttpPost("/LogginCompteClient", Name = "LogginCompteClient")]
        public void PostLoggin(ClientDTO clientDTO)
        {
            new ClientMetier().loggin(clientDTO);
        }

        [HttpPost("/ConfirmationEtReminderClient", Name = "ConfirmationEtReminderClient")]
        public void PostConfirmationEtReminder(string reminder)
        {

        }
    }
}
