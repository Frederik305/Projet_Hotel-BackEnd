using Microsoft.EntityFrameworkCore.Query.Internal;
using Projet_Hotel_CodeBase.DTO;
using System.Linq;

namespace Projet_Hotel_CodeBase.Métier
{
    public class ChambreMetier
    {
        public ChambreDTO[] RequestChambres()
        {
            using (var db = new MyDbContext())
            {
                return db.Chambres.Select(c => new ChambreDTO
                {
                    PkChaId = c.PkChaId,
                    ChaNumero = c.ChaNumero,
                    ChaEtat = c.ChaEtat,
                    ChaAutreInfo = c.ChaAutreInfo,
                    FkTypId = c.FkTypId
                }).ToArray();
            }
        }

        public ChambreDTO[] RequestChambreByNum(short numChambre)
        {
            using (var db = new MyDbContext())
            {
                var chambreDTO = db.Chambres
                    .Where(c => c.ChaNumero == numChambre)
                    .Select(c => new ChambreDTO
                    {
                        PkChaId = c.PkChaId,
                        ChaNumero = c.ChaNumero,
                        ChaEtat = c.ChaEtat,
                        ChaAutreInfo = c.ChaAutreInfo,
                        FkTypId = c.FkTypId
                    }).ToArray();
                return chambreDTO;
            }
        }
        public ChambreDTO AddChambre(ChambreDTO chambreDTO)
        {
            using (var db = new MyDbContext())
            {
                //var typeChambre = new TypeChambre();
                //db.TypeChambres.Where(e => e.TypNomType.Equals(chambreDTO.TypeChambre));
                var typeChambre = db.TypeChambres
             .FirstOrDefault(tc => tc.PkTypId == chambreDTO.FkTypId);

                var nouvelleChambre = new Chambre
                {
                    PkChaId = Guid.NewGuid(),
                    ChaAutreInfo = chambreDTO.ChaAutreInfo,
                    FkTypId = typeChambre.PkTypId,
                    ChaEtat = chambreDTO.ChaEtat,
                    ChaNumero = chambreDTO.ChaNumero,
                    TypeChambre = typeChambre
                };

                db.Chambres.Add(nouvelleChambre);
                db.SaveChanges();
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

        public ChambreDTO ModifierChambre(ChambreDTO chambreDTO)
        {

            using (var db = new MyDbContext())
            {
                if (!new ValidationsMetier().DoesRoomExist(chambreDTO.PkChaId, db))
                {
                    throw new Exception("La chambre spécifié n'existe pas.");
                }

                var chambre = db.Chambres.FirstOrDefault(c => c.PkChaId == chambreDTO.PkChaId);


                chambre.ChaNumero = chambreDTO.ChaNumero;
                chambre.ChaAutreInfo = chambreDTO.ChaAutreInfo;
                chambre.ChaEtat = chambreDTO.ChaEtat;
                chambre.FkTypId = chambreDTO.FkTypId;


                db.SaveChanges();
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
        public ChambreDTO[] RequestRoomsAvailable(ReservationDTO reservationDTO)
        {
            using (var db = new MyDbContext())
            {

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
