using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Métier;

namespace Projet_Hotel_CodeBase.Metier
{
    public class ClientMetier
    {
        //Logan
        public ClientDTO AddClient(ClientDTO clientDTO)


        {
            if (new ValidationsMetier().EmailExists(clientDTO))
            {
                throw new Exception("Courriel déja utiliser");
            }


            using (var context = new MyDbContext())
            {



                var nouveauClient = new Client
                {
                    PkCliId = Guid.NewGuid(),
                    CliNom = clientDTO.CliNom,
                    CliPrenom = clientDTO.CliPrenom,
                    CliAddresseResidence = clientDTO.CliAddresseResidence,
                    CliCourriel = clientDTO.CliCourriel,
                    CliMotDePasse = clientDTO.CliMotDePasse,
                    CliTelephoneMobile = clientDTO.CliTelephoneMobile


                };

                context.Clients.Add(nouveauClient);

                
                context.SaveChanges();
                return new ClientDTO
                {
                    PkCliId = nouveauClient.PkCliId,
                    CliNom = nouveauClient.CliNom,
                    CliPrenom = nouveauClient.CliPrenom,
                    CliAddresseResidence = nouveauClient.CliAddresseResidence,
                    CliCourriel = nouveauClient.CliCourriel,
                    CliMotDePasse = nouveauClient.CliMotDePasse,
                    CliTelephoneMobile = nouveauClient.CliTelephoneMobile
                };
                  
            }
        }
        //Logan
        public ClientDTO[] GetClients()
        {
            using (var context = new MyDbContext())
            {

                return context.Clients.Select(c => new ClientDTO
                {
                    CliNom = c.CliNom,
                    CliPrenom = c.CliPrenom,
                    CliAddresseResidence = c.CliAddresseResidence,
                    CliCourriel = c.CliCourriel,
                    CliMotDePasse = c.CliMotDePasse,
                    CliTelephoneMobile = c.CliTelephoneMobile,
                    PkCliId = c.PkCliId
                }).ToArray();

            }
        }
        public ClientDTO[] GetClient(ClientDTO clientDTO)
        {
            using (var context = new MyDbContext())
            {

                var clients = context.Clients
                             .Where(c => c.CliNom == clientDTO.CliNom || c.CliPrenom == clientDTO.CliPrenom)
                             .Select(c => new ClientDTO
                             {
                                 CliNom = c.CliNom,
                                 CliPrenom = c.CliPrenom,
                                 CliAddresseResidence = c.CliAddresseResidence,
                                 CliCourriel = c.CliCourriel,
                                 CliMotDePasse = c.CliMotDePasse,
                                 CliTelephoneMobile = c.CliTelephoneMobile,
                                 PkCliId = c.PkCliId
                             })
                             .ToArray();

                return clients;

            }
        }
        /// <summary>
        /// Récupère un client par son identifiant.
        /// </summary>
        /// <param name="clientId">L'identifiant unique du client à récupérer.</param>
        /// <returns>
        /// Un objet <see cref="ClientDTO"/> représentant le client correspondant, ou <c>null</c> si aucun client n'est trouvé.
        /// </returns>
        public ClientDTO GetClientById(ClientDTO clientDTO)
        {
            using (var context = new MyDbContext())
            {
                var client = context.Clients
                    .Where(c => c.PkCliId == clientDTO.PkCliId)
                    .Select(c => new ClientDTO
                    {
                        CliNom = c.CliNom,
                        CliPrenom = c.CliPrenom,
                        CliAddresseResidence = c.CliAddresseResidence,
                        CliCourriel = c.CliCourriel,
                        CliMotDePasse = c.CliMotDePasse,
                        CliTelephoneMobile = c.CliTelephoneMobile,
                        PkCliId = c.PkCliId
                    })
                    .FirstOrDefault(); // Retourne le premier client trouvé ou null

                return client; // Retourne le client ou null si non trouvé
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
