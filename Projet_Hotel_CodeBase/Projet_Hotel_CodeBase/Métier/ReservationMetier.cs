
using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Métier;

namespace Projet_Hotel_CodeBase.Metier
{
    public class ReservationMetier
    {
        ValidationsMetier validationMetier = new ValidationsMetier();
        //Logan
        public void CancelReservation(ReservationDTO reservationDTO)
        {

            using (var db = new MyDbContext())
            {
                if (!validationMetier.DoesReservationExist(reservationDTO, db))
                {
                    throw new Exception("La reservation spécifié n'existe pas.");
                }

                var reservation = db.Reservations.FirstOrDefault(r => r.PkResId == reservationDTO.PkResId);

                if (reservation != null)
                {
                    db.Reservations.Remove(reservation);

                    db.SaveChanges();


                }
                else
                {
                    throw new Exception("La réservation entré n'existe pas.");

                }

            }
        }
        //Logan
        public ReservationDTO ModifierReservation(ReservationDTO reservationDTO)
        {



            using (var db = new MyDbContext())
            {

                if (!validationMetier.DoesReservationExist(reservationDTO, db))
                {
                    throw new Exception("La reservation spécifié n'existe pas.");
                }

                if (!validationMetier.DoesRoomExist(reservationDTO.FkChaId, db))
                {
                    throw new Exception("La chambre spécifié n'existe pas.");
                }


                if (!validationMetier.IsRoomAvailable(reservationDTO, db))
                {
                    throw new Exception("Les dates de la réservation ne concordent pas avec la diponibilité de la chambre");
                }
                var reservation = db.Reservations.FirstOrDefault(r => r.PkResId == reservationDTO.PkResId);

                reservation.ResDateDebut = (DateTime)reservationDTO.ResDateDebut;
                reservation.ResDateFin = (DateTime)reservationDTO.ResDateFin;
                reservation.ResPrixJour = reservationDTO.ResPrixJour;
                reservation.FkChaId = reservationDTO.FkChaId;

                db.SaveChanges();
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

        //Logan
        public ReservationDTO AddReservation(ReservationDTO reservationDTO)
        {
            using (var db = new MyDbContext())
            {
                if (!validationMetier.DoesRoomExist(reservationDTO.FkChaId, db))
                {
                    throw new Exception("La chambre spécifié n'existe pas.");
                }

                if (!validationMetier.IsRoomAvailable(reservationDTO, db))
                {
                    throw new Exception("Les dates de la réservation ne concordent pas avec la diponibilité de la chambre");
                }
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

                db.Reservations.Add(nouvelleReservation);
                db.SaveChanges();

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
        //Logan
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

        public ReservationDTO[] SearchReservation(ReservationDTO reservationDTO)
        {
            using (var db = new MyDbContext())
            {
                // Start with the Reservations table and apply filters based on the provided DTO
                var query = db.Reservations.AsQueryable();

                // Filter by Reservation start date, if provided
                if (reservationDTO.ResDateDebut != null)
                {
                    query = query.Where(r => r.ResDateDebut == reservationDTO.ResDateDebut);
                }

                // Filter by Reservation end date, if provided
                if (reservationDTO.ResDateFin != null)
                {
                    query = query.Where(r => r.ResDateFin == reservationDTO.ResDateFin);
                }

                // Filter by Room ID, if provided
                if (reservationDTO.FkChaId != Guid.Empty)
                {
                    query = query.Where(r => r.FkChaId == reservationDTO.FkChaId);
                }

                // Filter by Client ID, if provided
                if (reservationDTO.FkCliId != Guid.Empty)
                {
                    query = query.Where(r => r.FkCliId == reservationDTO.FkCliId);
                }

                // Execute the query and map the results to ReservationDTO
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
