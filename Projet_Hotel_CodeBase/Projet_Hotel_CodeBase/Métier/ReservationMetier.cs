using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Métier;

namespace Projet_Hotel_CodeBase.Metier
{
    public class ReservationMetier
    {
        // Instance des validations métier utilisées pour la validation des réservations et chambres
        ValidationsMetier validationMetier = new ValidationsMetier();

        // Annule une réservation
        public void CancelReservation(ReservationDTO reservationDTO)
        {
            using (var db = new MyDbContext())
            {
                // Vérifie si la réservation existe
                if (!validationMetier.DoesReservationExist(reservationDTO, db))
                {
                    throw new Exception("La réservation spécifiée n'existe pas.");
                }

                var reservation = db.Reservations.FirstOrDefault(r => r.PkResId == reservationDTO.PkResId);

                // Si la réservation est trouvée, elle est supprimée de la base de données
                if (reservation != null)
                {
                    db.Reservations.Remove(reservation);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("La réservation entrée n'existe pas.");
                }
            }
        }

        // Modifie une réservation existante
        public ReservationDTO ModifierReservation(ReservationDTO reservationDTO)
        {
            using (var db = new MyDbContext())
            {
                // Vérifie l'existence de la réservation dans la base de données
                if (!validationMetier.DoesReservationExist(reservationDTO, db))
                {
                    throw new Exception(message: "La réservation spécifiée n'existe pas.");
                }

                // Vérifie si la chambre existe dans la réservation
                if (!validationMetier.DoesRoomExist(reservationDTO.FkChaId, db))
                {
                    throw new Exception(message: "La chambre spécifiée n'existe pas.");
                }

                // Vérifie si la chambre est disponible dans les dates spécifiées
                if (!validationMetier.IsRoomAvailable(reservationDTO, db))
                {
                    throw new Exception(message: "Les dates de la réservation ne concordent pas avec la disponibilité de la chambre");
                }

                // Vérifie que la date de début soit avant la date de fin de la réservation
                if (!validationMetier.IsStartDateBeforeEndDate(reservationDTO))
                {
                    throw new Exception(message: "La date de début de la réservation doit être avant la date de fin de la réservation");
                }

                // Mise à jour de la réservation dans la base de données
                var reservation = db.Reservations.FirstOrDefault(r => r.PkResId == reservationDTO.PkResId);

                reservation.ResDateDebut = (DateTime)reservationDTO.ResDateDebut;
                reservation.ResDateFin = (DateTime)reservationDTO.ResDateFin;
                reservation.ResPrixJour = reservationDTO.ResPrixJour;
                reservation.FkChaId = reservationDTO.FkChaId;

                db.SaveChanges();

                // Retourne un DTO de la réservation mise à jour
                return new ReservationDTO
                {
                    PkResId = reservation.PkResId,
                    ResAutre = reservation.ResAutre,
                    ResDateDebut = reservation.ResDateDebut,
                    ResDateFin = reservation.ResDateFin,
                    ResPrixJour = reservation.ResPrixJour,
                    FkChaId = reservation.FkChaId,
                    FkCliId = reservation.FkCliId
                };
            }
        }

        // Ajoute une nouvelle réservation
        public ReservationDTO AddReservation(ReservationDTO reservationDTO)
        {
            using (var db = new MyDbContext())
            {
                // Vérifie si la chambre existe et est disponible dans les dates spécifiées
                if (!validationMetier.DoesRoomExist(reservationDTO.FkChaId, db))
                {
                    throw new Exception(message: "La chambre spécifiée n'existe pas.");
                }

                if (!validationMetier.IsRoomAvailable(reservationDTO, db))
                {
                    throw new Exception(message: "Les dates de la réservation ne concordent pas avec la disponibilité de la chambre");
                }

                // Vérifie que la date de début soit avant la date de fin de la réservation
                if (!validationMetier.IsStartDateBeforeEndDate(reservationDTO))
                {
                    throw new Exception(message: "La date de début de la réservation doit être avant la date de fin de la réservation");
                }

                // Récupère la chambre et le client pour la réservation
                var chambre = db.Chambres.FirstOrDefault(c => c.PkChaId == reservationDTO.FkChaId);
                var client = db.Clients.FirstOrDefault(c => c.PkCliId == reservationDTO.FkCliId);

                var nouvelleReservation = new Reservation
                {
                    PkResId = Guid.NewGuid(),
                    ResAutre = reservationDTO.ResAutre,
                    ResDateDebut = (DateTime)reservationDTO.ResDateDebut,
                    ResDateFin = (DateTime)reservationDTO.ResDateFin,
                    ResPrixJour = reservationDTO.ResPrixJour,
                    Chambre = chambre,
                    Client = client
                };

                // Ajoute la nouvelle réservation et sauvegarde les modifications
                db.Reservations.Add(nouvelleReservation);
                db.SaveChanges();

                // Retourne un DTO de la nouvelle réservation
                return new ReservationDTO
                {
                    PkResId = nouvelleReservation.PkResId,
                    ResAutre = nouvelleReservation.ResAutre,
                    ResDateDebut = nouvelleReservation.ResDateDebut,
                    ResDateFin = nouvelleReservation.ResDateFin,
                    ResPrixJour = nouvelleReservation.ResPrixJour,
                    FkChaId = nouvelleReservation.FkChaId,
                    FkCliId = nouvelleReservation.FkCliId
                };
            }
        }

        // Récupère toutes les réservations
        public ReservationDTO[] GetReservations()
        {
            using (var context = new MyDbContext())
            {
                return context.Reservations.Select(r => new ReservationDTO
                {
                    PkResId = r.PkResId,
                    ResAutre = r.ResAutre,
                    ResDateDebut = r.ResDateDebut,
                    ResDateFin = r.ResDateFin,
                    ResPrixJour = r.ResPrixJour,
                    FkChaId = r.Chambre.PkChaId,
                    FkCliId = r.Client.PkCliId
                }).ToArray();
            }
        }

        // Recherche des réservations selon certains critères
        public ReservationDTO[] SearchReservation(ReservationDTO reservationDTO)
        {
            using (var db = new MyDbContext())
            {
                // Démarre une requête pour récupérer les réservations
                var query = db.Reservations.AsQueryable();

                // Applique les filtres sur les dates de début et de fin, le prix, etc.
                if (reservationDTO.ResDateDebut != null)
                {
                    query = query.Where(r => r.ResDateDebut == reservationDTO.ResDateDebut);
                }
                if (reservationDTO.ResDateFin != null)
                {
                    query = query.Where(r => r.ResDateFin == reservationDTO.ResDateFin);
                }
                if (reservationDTO.ResPrixJour != 0)
                {
                    query = query.Where(r => r.ResPrixJour == reservationDTO.ResPrixJour);
                }

                // Sélectionne les réservations filtrées et les retourne en tableau
                return query.Select(r => new ReservationDTO
                {
                    PkResId = r.PkResId,
                    ResAutre = r.ResAutre,
                    ResDateDebut = r.ResDateDebut,
                    ResDateFin = r.ResDateFin,
                    ResPrixJour = r.ResPrixJour,
                    FkChaId = r.FkChaId,
                    FkCliId = r.FkCliId
                }).ToArray();
            }
        }
    }
}
