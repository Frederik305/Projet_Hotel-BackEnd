using Microsoft.EntityFrameworkCore.Query.Internal;
using Projet_Hotel_CodeBase.DTO;
using System.Linq;

namespace Projet_Hotel_CodeBase.Métier
{
    public class ChambreMetier
    {
        public Chambre[] requestChambres()
        {
            using (var db = new MyDbContext())
            {
                return db.Chambres.ToArray();
            }
        }

        public Chambre requestChambreByNum(short numChambre)
        {
            using (var db = new MyDbContext())
            {
                var chambreEntity = db.Chambres.SingleOrDefault(c => c.ChaNumero == numChambre);
                return chambreEntity;
            }
        }
        public void addChambre(ChambreDTO chambreDTO)
        {
            using (var db = new MyDbContext())
            {
                //var typeChambre = new TypeChambre();
                //db.TypeChambres.Where(e => e.TypNomType.Equals(chambreDTO.TypeChambre));
                var typeChambre = db.TypeChambres
             .FirstOrDefault(tc => tc.TypNomType == chambreDTO.TypeChambre);

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

                /*db.Chambres.Add(chambre);
                db.SaveChanges();*/
            }
        }
    }
}
