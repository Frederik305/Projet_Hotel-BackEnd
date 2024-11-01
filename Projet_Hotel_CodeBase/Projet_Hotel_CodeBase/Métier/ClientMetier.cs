using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Métier;

namespace Projet_Hotel_CodeBase.Metier
{
    public class ClientMetier
    {
        ValidationsMetier validationsMetier = new ValidationsMetier();
        public ClientDTO AddClient(ClientDTO clientDTO)
        {
            using (var db = new MyDbContext())

            {
                if (validationsMetier.EmailExists(clientDTO, db))
                {
                    throw new Exception("Courriel déjà utiliser");
                }
                if (validationsMetier.TelephoneExists(clientDTO, db))
                {
                    throw new Exception("Telephone déjà utiliser");
                }
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

                db.Clients.Add(nouveauClient);
                db.SaveChanges();

                return new ClientDTO()
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

        public ClientDTO ModifierClient(ClientDTO clientDTO)
        {
            var cli = new ClientDTO { PkCliId = clientDTO.PkCliId };
            using (var db = new MyDbContext())
            {
                if (!validationsMetier.IsCurrentClientEmail(clientDTO, db))
                {
                    throw new Exception("Courriel déjà utiliser");
                }
                if (!validationsMetier.IsCurrentClientPhone(clientDTO, db))
                {
                    throw new Exception("Telephone déjà utiliser");
                }

                var client = db.Clients.FirstOrDefault(c => c.PkCliId == cli.PkCliId);

                client.CliPrenom = clientDTO.CliPrenom;
                client.CliNom = clientDTO.CliNom;
                client.CliAddresseResidence = clientDTO.CliAddresseResidence;
                client.CliTelephoneMobile = clientDTO.CliTelephoneMobile;
                client.CliCourriel = clientDTO.CliCourriel;
                client.CliMotDePasse = clientDTO.CliMotDePasse;

                db.SaveChanges();

                return new ClientDTO()
                {
                    PkCliId = clientDTO.PkCliId,
                    CliNom = clientDTO.CliNom,
                    CliPrenom = clientDTO.CliPrenom,
                    CliAddresseResidence = clientDTO.CliAddresseResidence,
                    CliCourriel = clientDTO.CliCourriel,
                    CliMotDePasse = clientDTO.CliMotDePasse,
                    CliTelephoneMobile = clientDTO.CliTelephoneMobile
                };

            }
        }
        //Logan
        public ClientDTO[] GetClients()
        {
            using (var db = new MyDbContext())
            {

                var clients = db.Clients.Select(c => new ClientDTO
                {
                    CliNom = c.CliNom,
                    CliPrenom = c.CliPrenom,
                    CliAddresseResidence = c.CliAddresseResidence,
                    CliCourriel = c.CliCourriel,
                    CliMotDePasse = c.CliMotDePasse,
                    CliTelephoneMobile = c.CliTelephoneMobile,
                    PkCliId = c.PkCliId
                }).ToArray();

                return clients;
            }
        }
        public ClientDTO[] GetClientByName(ClientDTO clientDTO)
        {
            using (var db = new MyDbContext())
            {

                var client = db.Clients
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

                return client;
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
                if (validationsMetier.EmailExists(clientDTO, db) && validationsMetier.PasswordExists(clientDTO, db))
                {
                    Console.WriteLine($"utilisateur: " + clientDTO.CliCourriel);
                }
            }
        }
    }
}
