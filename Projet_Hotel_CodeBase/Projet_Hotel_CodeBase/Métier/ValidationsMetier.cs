using Projet_Hotel_CodeBase.DTO;

namespace Projet_Hotel_CodeBase.Métier
{
    public class ValidationsMetier
    {
        // Vérifie si le client est valide en fonction de l'email et du mot de passe fournis dans le LoginDTO
        public bool IsValidClient(LoginDTO loginDTO, MyDbContext db)
        {
            var hasSameEmailAndPasswordAsClient = db.Clients
                .AsEnumerable()             // Charge les données en mémoire pour utilisation de LINQ
                .Any(c => c.CliCourriel == loginDTO.LogCourriel && string.Equals(c.CliMotDePasse, loginDTO.LogMotDePasse, StringComparison.Ordinal));

            return hasSameEmailAndPasswordAsClient;
        }

        // Vérifie que la date de début de la réservation est avant la date de fin
        public bool IsStartDateBeforeEndDate(ReservationDTO reservationDTO)
        {
            return reservationDTO.ResDateDebut < reservationDTO.ResDateFin;  // Retourne true si la date de début est antérieure à la date de fin
        }

        // Vérifie si un numéro de téléphone mobile existe déjà pour un autre client
        public bool TelephoneExists(ClientDTO clientDTO, MyDbContext db)
        {
            var client = db.Clients
                .FirstOrDefault(c => c.CliTelephoneMobile == clientDTO.CliTelephoneMobile); // Recherche un client avec le même numéro

            return client != null;
        }

        // Vérifie si l'email fourni appartient à un autre client que celui en cours
        public bool IsCurrentClientEmail(ClientDTO clientDTO, MyDbContext db)
        {
            var hasSameEmailAsAnotherClient = db.Clients
                .Any(c => c.CliCourriel == clientDTO.CliCourriel && c.PkCliId != clientDTO.PkCliId); // Vérifie si un autre client a le même email

            return !hasSameEmailAsAnotherClient;
        }

        // Vérifie si le téléphone mobile appartient déjà à un autre client que celui en cours
        public bool IsCurrentClientPhone(ClientDTO clientDTO, MyDbContext db)
        {
            var hasSamePhoneAsAnotherClient = db.Clients
                .Any(c => c.CliTelephoneMobile == clientDTO.CliTelephoneMobile && c.PkCliId != clientDTO.PkCliId); // Vérifie si un autre client a le même téléphone mobile

            return !hasSamePhoneAsAnotherClient;
        }

        // Vérifie si un email existe déjà dans la base de données pour un autre client
        public bool EmailExists(ClientDTO clientDTO, MyDbContext db)
        {
            var client = db.Clients
                .FirstOrDefault(c => c.CliCourriel == clientDTO.CliCourriel); // Recherche un client avec le même email

            return client != null;
        }

        // Vérifie si un mot de passe existe déjà dans la base de données pour un client
        public bool PasswordExists(ClientDTO clientDTO, MyDbContext db)
        {
            var client = db.Clients
                .FirstOrDefault(c => c.CliMotDePasse == clientDTO.CliMotDePasse); // Recherche un client avec le même mot de passe

            return client != null;
        }

        // Vérifie si une chambre est disponible pour la période demandée
        public bool IsRoomAvailable(ReservationDTO reservationDTO, MyDbContext db)
        {
            var reservationsChambre = db.Reservations.Where(r => r.Chambre.PkChaId == reservationDTO.FkChaId && r.PkResId != reservationDTO.PkResId).ToList();
            // Vérifie les réservations existantes pour la chambre spécifique et exclut celle en cours

            foreach (var reservation in reservationsChambre)
            {
                // Si la nouvelle réservation chevauche une réservation existante, la chambre n'est pas disponible
                if (reservation.ResDateDebut < reservationDTO.ResDateFin && reservation.ResDateFin > reservationDTO.ResDateDebut)
                {
                    return false; // Chambre non disponible
                }
            }
            return true; // Chambre disponible
        }

        // Vérifie si une chambre existe dans la base de données
        public bool DoesRoomExist(Guid chambreId, MyDbContext db)
        {
            return db.Chambres.Any(c => c.PkChaId == chambreId);
        }

        // Vérifie si une réservation existe avec l'ID spécifié
        public bool DoesReservationExist(ReservationDTO reservationDTO, MyDbContext db)
        {
            return db.Reservations.Any(r => r.PkResId == reservationDTO.PkResId);
        }
    }
}
