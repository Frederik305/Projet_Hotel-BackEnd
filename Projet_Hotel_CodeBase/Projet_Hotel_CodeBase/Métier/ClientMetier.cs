using Projet_Hotel_CodeBase.DTO;
using System.Xml.Serialization;

namespace Projet_Hotel_CodeBase.Métier
{
    public class ClientMetier
    {
        public void addClient(ClientDTO clientDTO)
        {
            using (var db = new MyDbContext())
            {
                //var typeChambre = new TypeChambre();
                //db.TypeChambres.Where(e => e.TypNomType.Equals(chambreDTO.TypeChambre));
                /*var client = db.Clients
             .FirstOrDefault(tc => tc.TypNomType == clientDTO.TypeChambre);*/

                // Regarde si un client existe deja avec la meme addresse de courriel
                var client = db.Clients
             .FirstOrDefault(c => c.CliCourriel == clientDTO.CliCourriel);
                if( client != null )
                {
                    return;
                }

                var newClient = new Client
                {
                    PkCliId = Guid.NewGuid(),
                    CliPrenom = clientDTO.CliPrenom,
                    CliNom = clientDTO.CliNom,
                    CliAddresseResidence = clientDTO.CliAddresseResidence,
                    CliTelephoneMobile = clientDTO.CliTelephoneMobile,
                    CliCourriel = clientDTO.CliCourriel,
                    CliMotDePasse = clientDTO.CliMotDePasse,
                };

                db.Clients.Add(newClient);
                db.SaveChanges();
            }
        }
        public void loggin(ClientDTO clientDTO)
        {
            using (var db = new MyDbContext())
            {
                var client = db.Clients
             .FirstOrDefault(c => c.CliCourriel == clientDTO.CliCourriel && c.CliMotDePasse == clientDTO.CliMotDePasse);

                if (client != null)
                {
                    Console.WriteLine($"utilisateur: " + clientDTO.CliCourriel);
                }
            }
        }
    }
}
