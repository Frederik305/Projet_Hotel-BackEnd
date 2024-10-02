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
                    TypeChambre = c.TypeChambre,
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
                        TypeChambre = c.TypeChambre,
                    }).ToArray();
                return chambreDTO;
            }
        }
        public void AddChambre(ChambreDTO chambreDTO)
        {
            using (var db = new MyDbContext())
            {
                //var typeChambre = new TypeChambre();
                //db.TypeChambres.Where(e => e.TypNomType.Equals(chambreDTO.TypeChambre));
                var typeChambre = db.TypeChambres
             .FirstOrDefault(tc => tc.TypNomType == chambreDTO.NomTypeChambre);

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

                
            }
        }
    }
}
