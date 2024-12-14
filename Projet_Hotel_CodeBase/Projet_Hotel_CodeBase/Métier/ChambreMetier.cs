using Projet_Hotel_CodeBase.DTO;

namespace Projet_Hotel_CodeBase.Métier
{
    public class ChambreMetier
    {
        // Récupère toutes les chambres de la base de données et les retourne sous forme de tableau de DTO
        public ChambreDTO[] RequestChambres()
        {
            using (var db = new MyDbContext())
            {
                // Sélectionner et projeter les chambres depuis la base de données vers un tableau de DTO
                return db.Chambres.Select(c => new ChambreDTO
                {
                    PkChaId = c.PkChaId,                // ID unique de la chambre
                    ChaNumero = c.ChaNumero,            // Numéro de la chambre
                    ChaEtat = c.ChaEtat,                // État de la chambre (Disponible, Occupée, etc.)
                    ChaAutreInfo = c.ChaAutreInfo,      // Informations supplémentaires sur la chambre
                    FkTypId = c.FkTypId                 // Type de chambre associé à cette chambre
                }).ToArray();
            }
        }

        // Récupère les chambres en fonction du numéro de chambre et les retourne sous forme de tableau de DTO
        public ChambreDTO[] RequestChambreByNum(ChambreDTO chambreDTO)
        {
            using (var db = new MyDbContext())
            {
                // Sélectionner et projeter les chambres qui ont le numéro spécifié
                var chambre = db.Chambres
                    .Where(c => c.ChaNumero == chambreDTO.ChaNumero) // Filtre par numéro de chambre
                    .Select(c => new ChambreDTO
                    {
                        PkChaId = c.PkChaId,
                        ChaNumero = c.ChaNumero,
                        ChaEtat = c.ChaEtat,
                        ChaAutreInfo = c.ChaAutreInfo,
                        FkTypId = c.FkTypId
                    }).ToArray();
                return chambre;
            }
        }

        // Récupère une chambre par son ID (PK)
        public ChambreDTO GetChambreById(ChambreDTO chambreDTO)
        {
            using (var db = new MyDbContext())
            {
                // Sélectionner et retourner une chambre correspondant à l'ID passé
                var chambre = db.Chambres
                    .Where(c => c.PkChaId == chambreDTO.PkChaId) // Filtre par l'ID de la chambre
                    .Select(c => new ChambreDTO
                    {
                        PkChaId = c.PkChaId,
                        ChaNumero = c.ChaNumero,
                        ChaEtat = c.ChaEtat,
                        ChaAutreInfo = c.ChaAutreInfo,
                        FkTypId = c.FkTypId
                    }).FirstOrDefault();
                return chambre;
            }
        }

        // Ajoute une nouvelle chambre dans la base de données
        public ChambreDTO AddChambre(ChambreDTO chambreDTO)
        {
            using (var db = new MyDbContext())
            {
                // Récupère le type de chambre correspondant à l'ID spécifié
                var typeChambre = db.TypeChambres
             .FirstOrDefault(tc => tc.PkTypId == chambreDTO.FkTypId);

                // Crée une nouvelle chambre et assigne les valeurs de chambreDTO
                var nouvelleChambre = new Chambre
                {
                    PkChaId = Guid.NewGuid(),  // Crée un nouvel ID unique
                    ChaAutreInfo = chambreDTO.ChaAutreInfo,
                    FkTypId = typeChambre.PkTypId, // Associe le type de chambre
                    ChaEtat = chambreDTO.ChaEtat,
                    ChaNumero = chambreDTO.ChaNumero,
                    TypeChambre = typeChambre // Associe l'objet TypeChambre complet
                };

                // Ajoute la nouvelle chambre à la base de données
                db.Chambres.Add(nouvelleChambre);
                db.SaveChanges();

                // Retourne un DTO de la chambre ajoutée
                return new ChambreDTO()
                {
                    PkChaId = nouvelleChambre.PkChaId,
                    ChaAutreInfo = nouvelleChambre.ChaAutreInfo,
                    FkTypId = nouvelleChambre.FkTypId,
                    ChaEtat = nouvelleChambre.ChaEtat,
                    ChaNumero = nouvelleChambre.ChaNumero
                };
            }
        }

        // Modifie les informations d'une chambre existante dans la base de données
        public ChambreDTO ModifierChambre(ChambreDTO chambreDTO)
        {
            using (var db = new MyDbContext())
            {
                // Vérifie si la chambre existe dans la base de données
                if (!new ValidationsMetier().DoesRoomExist(chambreDTO.PkChaId, db))
                {
                    throw new Exception(message:"La chambre spécifié n'existe pas.");
                }

                // Récupère la chambre et modifie ses propriétés selon les valeurs de chambreDTO
                var chambre = db.Chambres.FirstOrDefault(c => c.PkChaId == chambreDTO.PkChaId);
                chambre.ChaNumero = chambreDTO.ChaNumero;
                chambre.ChaAutreInfo = chambreDTO.ChaAutreInfo;
                chambre.ChaEtat = chambreDTO.ChaEtat;
                chambre.FkTypId = chambreDTO.FkTypId;

                // Sauvegarde les modifications
                db.SaveChanges();

                // Retourne un DTO avec les informations mises à jour
                return new ChambreDTO
                {
                    PkChaId = chambre.PkChaId,
                    ChaNumero = chambre.ChaNumero,
                    ChaAutreInfo = chambre.ChaAutreInfo,
                    ChaEtat = chambre.ChaEtat,
                    FkTypId = chambre.FkTypId
                };
            }
        }

        // Récupère toutes les chambres disponibles pour une période de réservation donnée
        public ChambreDTO[] RequestRoomsAvailable(ReservationDTO reservationDTO)
        {
            using (var db = new MyDbContext())
            {
                // Sélectionner les chambres qui ne sont pas réservées dans la période spécifiée
                return db.Chambres.Where(c => !c.Reservations.Any(r =>
                    r.ResDateFin >= reservationDTO.ResDateDebut && r.ResDateDebut <= reservationDTO.ResDateFin))
                .Select(c => new ChambreDTO
                {
                    PkChaId = c.PkChaId,
                    ChaNumero = c.ChaNumero,
                    ChaEtat = c.ChaEtat,
                    ChaAutreInfo = c.ChaAutreInfo,
                    FkTypId = c.FkTypId
                }).ToArray(); ;
            }
        }
    }
}
