
using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Métier;

namespace Projet_Hotel_CodeBase.Metier
{
    public class ReservationMetier
    {
        //Logan
        public void CancelReservation(ReservationDTO reservationDTO)
        {
            using (var context = new MyDbContext())
            {
                
                var reservation = context.Reservations.FirstOrDefault(r => r.PkResId == reservationDTO.PkResId);

                if (reservation != null)
                {
                    context.Reservations.Remove(reservation);

                    context.SaveChanges();


                }
                else { 
                    throw new Exception("La réservation entré n'existe pas."); 
                
                }

            }
        }
        //Logan
        public ReservationDTO ModifierReservation(ReservationDTO reservationDTO)
        {
            if (!new ValidationsMetier().IsRoomAvailable(reservationDTO))
            {
                throw new Exception("Les dates de la réservation ne concordent pas avec la diponibilité de la chambre");
            }

            
            using (var context = new MyDbContext())
            {

                var reservation = context.Reservations.FirstOrDefault(r => r.PkResId == reservationDTO.PkResId);

                reservation.ResDateDebut = reservationDTO.ResDateDebut;
                reservation.ResDateFin = reservationDTO.ResDateFin;
                reservation.ResPrixJour = reservationDTO.ResPrixJour;
                reservation.FkChaId = reservationDTO.FkChaId;

                context.SaveChanges();
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

            if(!new ValidationsMetier().IsRoomAvailable(reservationDTO))
            {
                throw new Exception("Les dates de la réservation ne concordent pas avec la diponibilité de la chambre");
            }
            using (var context = new MyDbContext())
            {
                var chambre = context.Chambres
             .FirstOrDefault(c => c.PkChaId == reservationDTO.FkChaId);
                var client = context.Clients
             .FirstOrDefault(c => c.PkCliId == reservationDTO.FkCliId);


                var nouvelleReservation = new Reservation
                {
                    PkResId = Guid.NewGuid(),
                    ResAutre = reservationDTO.ResAutre,
                    ResDateDebut = reservationDTO.ResDateDebut,
                    ResDateFin = reservationDTO.ResDateFin,
                    ResPrixJour = reservationDTO.ResPrixJour,
                    Chambre = chambre,
                    Client = client


                };

                context.Reservations.Add(nouvelleReservation);
                context.SaveChanges();
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
            using(var context = new MyDbContext())
            {
                return context.Reservations.Select(r => new ReservationDTO
                {
                    PkResId = Guid.NewGuid(),
                    ResAutre = r.ResAutre,
                    ResDateDebut = r.ResDateDebut,
                    ResDateFin = r.ResDateFin,
                    ResPrixJour = r.ResPrixJour,
                    FkChaId= r.Chambre.PkChaId,
                    FkCliId= r.Client.PkCliId
                }).ToArray();
            }
        }
    }
    
}
