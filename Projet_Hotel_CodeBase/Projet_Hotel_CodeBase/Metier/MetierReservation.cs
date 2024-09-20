using Azure.Identity;
using Projet_Hotel_CodeBase.DTO;

namespace Projet_Hotel_CodeBase.Metier
{
    public class MetierReservation
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
        public void AddReservation(ReservationDTO reservationDTO)
        {
            using (var context = new MyDbContext())
            {
                var chambre = context.Chambres
             .FirstOrDefault(c => c.ChaNumero == reservationDTO.ResChambre);
                var client = context.Clients
             .FirstOrDefault(c => c.CliCourriel == reservationDTO.ResCliCourriel);


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
            }
        }
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
                     ResChambre= r.Chambre.ChaNumero,
                     ResCliCourriel= r.Client.CliCourriel
                }).ToArray();
            }
        }
    }
    
}
