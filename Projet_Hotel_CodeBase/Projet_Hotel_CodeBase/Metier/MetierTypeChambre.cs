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
        public void SetTypeChambre(TypeChambre typeChambreEntity)
        {
           
                using (var context = new MyDbContext())
                {
                    context.TypeChambres.Add(typeChambreEntity);
                    context.SaveChanges(); // Sauvegarde les changements dans la base de données
                }
                
            }
            
        
    }
}
