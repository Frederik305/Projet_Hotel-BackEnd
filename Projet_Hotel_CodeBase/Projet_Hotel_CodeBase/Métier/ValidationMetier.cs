using Projet_Hotel_CodeBase.DTO;

namespace Projet_Hotel_CodeBase.Métier
{
    public class ValidationsMetier
    {
        public bool EmailExists(ClientDTO clientDTO)
        {
            using (var db = new MyDbContext())
            {
                var client = db.Clients
                .FirstOrDefault(c => c.CliCourriel == clientDTO.CliCourriel);

                if (client == null)
                {
                    return false;
                }
                return true;
            }
        }
        public bool PasswordExists(ClientDTO clientDTO)
        {
            using (var db = new MyDbContext())
            {
                var client = db.Clients
                .FirstOrDefault(c => c.CliMotDePasse == clientDTO.CliMotDePasse);

                if (client == null)
                {
                    return false;
                }
                return true;
            }
        }
        /// <summary>
        /// Vérifie si une chambre est disponible pour une nouvelle réservation.
        /// </summary>
        /// <param name="reservationDTO">L'objet <see cref="ReservationDTO"/> contenant les détails de la réservation, y compris l'identifiant de la chambre et les dates de début et de fin.</param>
        /// <returns>
        /// <c>true</c> si la chambre est disponible pour la période spécifiée dans <paramref name="reservationDTO"/>; 
        /// autrement, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// La méthode interroge la base de données pour récupérer toutes les réservations existantes pour la chambre
        /// spécifiée par l'identifiant. Elle vérifie ensuite si les dates de la nouvelle réservation se chevauchent
        /// avec celles des réservations existantes. Si un chevauchement est détecté, la chambre est considérée comme
        /// non disponible.
        /// </remarks>
        public bool IsRoomAvailable(ReservationDTO reservationDTO)
        {
            
            using (var db = new MyDbContext())
            {
                var reservationsChambre= db.Reservations.Where(r =>r.Chambre.PkChaId ==reservationDTO.FkChaId && r.PkResId != reservationDTO.PkResId).ToList();
                foreach (var reservation in reservationsChambre)
                {
                    // Si la nouvelle réservation chevauche une réservation existante, la chambre n'est pas disponible
                    if (reservation.ResDateDebut < reservationDTO.ResDateFin && reservation.ResDateFin > reservationDTO.ResDateDebut)
                    {
                        return false; // Chambre non disponible
                    }

                }
           
            }return true;
        }

        public bool IsRoomAvailableModify(ReservationDTO reservationDTO)
        {

            using (var db = new MyDbContext())
            {
                var reservationsChambre = db.Reservations.Where(r => r.Chambre.PkChaId == reservationDTO.FkChaId).ToList();
                foreach (var reservation in reservationsChambre)
                {
                    // Si la nouvelle réservation chevauche une réservation existante, la chambre n'est pas disponible
                    if (reservation.ResDateDebut < reservationDTO.ResDateFin && reservation.ResDateFin > reservationDTO.ResDateDebut)
                    {
                        return false; // Chambre non disponible
                    }

                }

            }
            return true;
        }
    }
}
