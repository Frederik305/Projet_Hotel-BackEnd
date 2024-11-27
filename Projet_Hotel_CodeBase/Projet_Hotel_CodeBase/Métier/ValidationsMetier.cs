using Projet_Hotel_CodeBase.DTO;

namespace Projet_Hotel_CodeBase.Métier
{
    public class ValidationsMetier
    {
        public bool IsValidClient(LoginDTO loginDTO, MyDbContext db)
        {
            var hasSameEmailAndPasswordAsClient = db.Clients
                .AsEnumerable() 
                .Any(c => c.CliCourriel == loginDTO.LogCourriel && string.Equals(c.CliMotDePasse, loginDTO.LogMotDePasse, StringComparison.Ordinal));

            return hasSameEmailAndPasswordAsClient;
        }

        public bool TelephoneExists(ClientDTO clientDTO, MyDbContext db)
        {
            var client = db.Clients
            .FirstOrDefault(c => c.CliTelephoneMobile == clientDTO.CliTelephoneMobile);

            if (client == null)
            {
                return false;
            }
            return true;
        }
        public bool IsCurrentClientEmail(ClientDTO clientDTO, MyDbContext db)
        {
            var hasSameEmailAsAnotherClient = db.Clients
                .Any(c => c.CliCourriel == clientDTO.CliCourriel && c.PkCliId != clientDTO.PkCliId);

            return !hasSameEmailAsAnotherClient;
        }

        public bool IsCurrentClientPhone(ClientDTO clientDTO, MyDbContext db)
        {
            var hasSameEmailAsAnotherClient = db.Clients
                .Any(c => c.CliTelephoneMobile == clientDTO.CliTelephoneMobile && c.PkCliId != clientDTO.PkCliId);

            return !hasSameEmailAsAnotherClient;
        }

        public bool EmailExists(ClientDTO clientDTO, MyDbContext db)
        {
            var client = db.Clients
            .FirstOrDefault(c => c.CliCourriel == clientDTO.CliCourriel);

            if (client == null)
            {
                return false;
            }
            return true;
        }
        public bool PasswordExists(ClientDTO clientDTO, MyDbContext db)
        {
            var client = db.Clients
            .FirstOrDefault(c => c.CliMotDePasse == clientDTO.CliMotDePasse);

            if (client == null)
            {
                return false;
            }
            return true;
        }
        public bool IsRoomAvailable(ReservationDTO reservationDTO, MyDbContext db)
        {
            var reservationsChambre = db.Reservations.Where(r => r.Chambre.PkChaId == reservationDTO.FkChaId && r.PkResId != reservationDTO.PkResId).ToList();
            foreach (var reservation in reservationsChambre)
            {
                // Si la nouvelle réservation chevauche une réservation existante, la chambre n'est pas disponible
                if (reservation.ResDateDebut < reservationDTO.ResDateFin && reservation.ResDateFin > reservationDTO.ResDateDebut)
                {
                    return false; // Chambre non disponible
                }

            }
            return true;
        }
        public bool DoesRoomExist(Guid chambreId, MyDbContext db)
        {

            return db.Chambres.Any(c => c.PkChaId == chambreId);

        }
        public bool DoesReservationExist(ReservationDTO reservationDTO, MyDbContext db)
        {

            return db.Reservations.Any(r => r.PkResId == reservationDTO.PkResId);

        }
    }
}
