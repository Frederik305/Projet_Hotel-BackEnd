namespace Projet_Hotel_CodeBase.Metier;
using Projet_Hotel_CodeBase.DTO;

public class TypeChambreMetier
{
    public TypeChambreDTO[] GetTypeChambres()
    {
        using (var context = new MyDbContext())
        {
            return context.TypeChambres.Select(t=> new TypeChambreDTO
            {
                PkTypId = t.PkTypId,
                TypNomType=t.TypNomType,
                TypDescription=t.TypDescription,
                TypPrixPlafond=t.TypPrixPlafond,
                TypPrixPlancher=t.TypPrixPlancher
            }).ToArray();
                

        }
    }
    public TypeChambreDTO AddTypeChambre(TypeChambreDTO typeChambreDTO)
    {

        using (var context = new MyDbContext())
        {
            var typeChambre = new TypeChambre
            {
                PkTypId = Guid.NewGuid(),
                TypNomType = typeChambreDTO.TypNomType,
                TypDescription = typeChambreDTO.TypDescription,
                TypPrixPlafond = typeChambreDTO.TypPrixPlafond,
                TypPrixPlancher = typeChambreDTO.TypPrixPlancher,
            };
            context.TypeChambres.Add(typeChambre);
            context.SaveChanges(); // Sauvegarde les changements dans la base de données

            return new TypeChambreDTO
            {
                PkTypId = typeChambre.PkTypId,
                TypNomType = typeChambre.TypNomType,
                TypDescription = typeChambre.TypDescription,
                TypPrixPlafond = typeChambre.TypPrixPlafond,
                TypPrixPlancher = typeChambre.TypPrixPlancher,
            };
        }
    }
}
