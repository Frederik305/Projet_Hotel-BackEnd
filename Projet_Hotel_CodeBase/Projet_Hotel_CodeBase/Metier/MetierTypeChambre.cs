namespace Projet_Hotel_CodeBase.Metier
{
    public class MetierTypeChambre
    {
        public TypeChambre[] GetTypeChambres()
        {
            using (var context = new MyDbContext())
            {
                return context.TypeChambres.ToArray();

            }
        }
        public void AddTypeChambre(TypeChambre typeChambre)
        {
           
                using (var context = new MyDbContext())
                {
                    context.TypeChambres.Add(typeChambre);
                    context.SaveChanges(); // Sauvegarde les changements dans la base de données
                }
            
            }
        public void GetTypeChambre(string nomTypeChambre)
        {

        }
            
        
    }
}
