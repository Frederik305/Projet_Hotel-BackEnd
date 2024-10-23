using Azure.Identity;
using Projet_Hotel_CodeBase.DTO;
using Projet_Hotel_CodeBase.Métier;

namespace Projet_Hotel_CodeBase.Metier
{
    public class ReservationMetier
    {
        public void CancelReservation(Guid PkResACancel)
        {
            using (var context = new MyDbContext())
            {
                
                var reservation = context.Reservations.FirstOrDefault(r => r.PkResId == PkResACancel);

                if (reservation != null)
                {
                    context.Reservations.Remove(reservation);
                    context.SaveChanges();
                }


            }
        }

        public void ModifierReservation(Guid PkResAmodifier, DateTime dateDebutModifier, DateTime dateFinModifier)
        {
            var res = new ReservationDTO { PkResId = PkResAmodifier };
            using (var context = new MyDbContext())
            {

                var reservation = context.Reservations.FirstOrDefault(r => r.PkResId == res.PkResId);

                reservation.ResDateDebut = dateDebutModifier;
                reservation.ResDateFin = dateFinModifier;
                context.SaveChanges();


            }

        }
        public ReservationDTO AddReservation(ReservationDTO reservationDTO)
        {

            if (!new ValidationsMetier().IsRoomAvailable(reservationDTO))
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
        public ReservationDTO[] GetReservations()
        {
            using (var context = new MyDbContext())
            {
                return context.Reservations.Select(r => new ReservationDTO
                {
                    PkResId = Guid.NewGuid(),
                    ResAutre = r.ResAutre,
                    ResDateDebut = r.ResDateDebut,
                    ResDateFin = r.ResDateFin,
                    ResPrixJour = r.ResPrixJour,
                    FkChaId = r.Chambre.PkChaId,
                    FkCliId = r.Client.PkCliId
                }).ToArray();
            }
        }
    }
    
}
