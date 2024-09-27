using Microsoft.EntityFrameworkCore;

using Projet_Hotel_CodeBase.DTO;
namespace Projet_Hotel_CodeBase.Metier
{
    public class ChambreMetier
    {
        public Chambre[] GetChambres()
        {
            using (var context = new MyDbContext())
            {
                

                
                
                return context.Chambres.ToArray();

            }
        }
        public Chambre GetChambre(int numChambre)
        {
            using (var context = new MyDbContext())
            {
                // Supposons que vous voulez récupérer l'entité avec Id = 1

                var entite = context.Chambres.Where(c => c.ChaNumero == numChambre).FirstOrDefault();
                // Utilisation de la méthode Find pour récupérer l'entité

                return entite;

            }
        }
        public void AddChambre(ChambreDTO chambreDTO)
        {
            using (var context = new MyDbContext())
            {
                var typeChambre = context.TypeChambres
             .FirstOrDefault(tc => tc.TypNomType == chambreDTO.NomTypeChambre);
                

                var nouvelleChambre = new Chambre
                {
                    PkChaId = Guid.NewGuid(),
                    ChaAutreInfo = chambreDTO.ChaAutreInfo,
                    FkTypId = typeChambre.PkTypId,
                    ChaEtat=chambreDTO.ChaEtat,
                    ChaNumero=chambreDTO.ChaNumero,
                    TypeChambre=typeChambre
                    
                };
               
                context.Chambres.Add(nouvelleChambre);
                context.SaveChanges();
            }
            

            /*

            // Créer une nouvelle Chambre avec l'ID du TypeChambre trouvé
            var nouvelleChambre = new Chambre
            {
                PkChambreId = Guid.NewGuid(),
                NomChambre = chambreDto.NomChambre,
                FkTypeChambreId = typeChambre.PkTypeChambreId
            };

            // Ajouter la nouvelle chambre à la base de données
            _context.Chambres.Add(nouvelleChambre);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Chambre créée avec succès", chambreId = nouvelleChambre.PkChambreId });*/
        }
    }
}

    