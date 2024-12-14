using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Métier;

namespace Projet_Hotel_CodeBase.Metier
{
    public class ClientMetier
    {
        // Instance de la classe pour les validations de données
        ValidationsMetier validationsMetier = new ValidationsMetier();

        // Méthode pour ajouter un nouveau client
        public ClientDTO AddClient(ClientDTO clientDTO)
        {
            using (var db = new MyDbContext())
            {
                // Vérifie si l'email existe déjà dans la base de données
                if (validationsMetier.EmailExists(clientDTO, db))
                {
                    throw new Exception(message: "Courriel déjà utilisé");
                }
                // Vérifie si le téléphone existe déjà dans la base de données
                if (validationsMetier.TelephoneExists(clientDTO, db))
                {
                    throw new Exception(message: "Téléphone déjà utilisé");
                }

                // Crée un nouvel objet client et mappe les données du DTO
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

                // Ajoute le nouveau client à la base de données et sauvegarde
                db.Clients.Add(nouveauClient);
                db.SaveChanges();

                // Retourne un DTO du client ajouté
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

        // Méthode pour modifier les informations d'un client existant
        public ClientDTO ModifierClient(ClientDTO clientDTO)
        {
            var cli = new ClientDTO { PkCliId = clientDTO.PkCliId };
            using (var db = new MyDbContext())
            {
                // Vérifie que l'email et le téléphone du client actuel ne sont pas déjà utilisés
                if (!validationsMetier.IsCurrentClientEmail(clientDTO, db))
                {
                    throw new Exception(message: "Courriel déjà utilisé");
                }
                if (!validationsMetier.IsCurrentClientPhone(clientDTO, db))
                {
                    throw new Exception(message: "Téléphone déjà utilisé");
                }

                // Récupère le client existant à partir de l'ID et met à jour les informations
                var client = db.Clients.FirstOrDefault(c => c.PkCliId == cli.PkCliId);

                client.CliPrenom = clientDTO.CliPrenom;
                client.CliNom = clientDTO.CliNom;
                client.CliAddresseResidence = clientDTO.CliAddresseResidence;
                client.CliTelephoneMobile = clientDTO.CliTelephoneMobile;
                client.CliCourriel = clientDTO.CliCourriel;
                client.CliMotDePasse = clientDTO.CliMotDePasse;

                // Sauvegarde les modifications
                db.SaveChanges();

                // Retourne un DTO avec les informations mises à jour
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

        // Méthode pour récupérer un client par son email
        public ClientDTO GetClientByEmail(ClientDTO clientDTO)
        {
            using (var db = new MyDbContext())
            {
                var client = db.Clients
                    .Where(c => c.CliCourriel == clientDTO.CliCourriel)
                    .Select(c => new ClientDTO
                    {
                        CliNom = c.CliNom,
                        CliPrenom = c.CliPrenom,
                        CliAddresseResidence = c.CliAddresseResidence,
                        CliCourriel = c.CliCourriel,
                        CliMotDePasse = c.CliMotDePasse,
                        CliTelephoneMobile = c.CliTelephoneMobile,
                        PkCliId = c.PkCliId
                    }).FirstOrDefault();
                if (client == null)
                {
                    throw new Exception(message: "L'adresse courriel entrée n'est reliée à aucun client.");
                }
                return client;
            }
        }

        // Méthode pour récupérer tous les clients
        public ClientDTO[] GetClients()
        {
            using (var db = new MyDbContext())
            {
                // Sélectionne tous les clients et les projette en un tableau de DTO
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

        // Méthode pour rechercher des clients par leur nom ou prénom
        public ClientDTO[] GetClientByName(ClientDTO clientDTO)
        {
            using (var db = new MyDbContext())
            {
                // Sélectionne les clients dont le nom ou le prénom correspondent
                var clients = db.Clients
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

                return clients; // Retourne le tableau des clients trouvés
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
    }
}
