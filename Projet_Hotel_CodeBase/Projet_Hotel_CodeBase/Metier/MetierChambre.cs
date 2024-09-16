namespace Projet_Hotel_CodeBase.Metier
{
    public class MetierChambre
    {
        public Chambre[] GetChambres() { 
            using (var context = new MyDbContext())
            {
                // Supposons que vous voulez récupérer l'entité avec Id = 1
                
                var entite = context.Chambres.ToArray();
                // Utilisation de la méthode Find pour récupérer l'entité
                Console.WriteLine(entite);
                return entite;
            
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
        }
}
