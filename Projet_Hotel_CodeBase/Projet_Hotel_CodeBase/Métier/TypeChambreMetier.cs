namespace Projet_Hotel_CodeBase.Metier;
using Projet_Hotel_CodeBase.DTO;

public class TypeChambreMetier
{
    // Récupère tous les types de chambres depuis la base de données
    public TypeChambreDTO[] GetTypeChambres()
    {
        using (var context = new MyDbContext())
        {
            // Utilisation de LINQ pour récupérer tous les types de chambres et les transformer en DTO
            return context.TypeChambres.Select(t => new TypeChambreDTO
            {
                PkTypId = t.PkTypId,                    // Identifiant unique du type de chambre
                TypNomType = t.TypNomType,              // Nom du type de chambre
                TypDescription = t.TypDescription,      // Description du type de chambre
                TypPrixPlafond = t.TypPrixPlafond,      // Prix plafond du type de chambre
                TypPrixPlancher = t.TypPrixPlancher     // Prix plancher du type de chambre
            }).ToArray();
        }
    }

    // Ajoute un nouveau type de chambre dans la base de données
    public TypeChambreDTO AddTypeChambre(TypeChambreDTO typeChambreDTO)
    {
        using (var context = new MyDbContext())
        {
            // Création d'un nouvel objet TypeChambre à partir du DTO fourni
            var typeChambre = new TypeChambre
            {
                PkTypId = Guid.NewGuid(),                           // Génération d'un nouvel identifiant unique
                TypNomType = typeChambreDTO.TypNomType,             // Nom du type de chambre
                TypDescription = typeChambreDTO.TypDescription,     // Description du type
                TypPrixPlafond = typeChambreDTO.TypPrixPlafond,     // Prix plafond
                TypPrixPlancher = typeChambreDTO.TypPrixPlancher,   // Prix plancher
            };

            // Ajout du nouvel objet TypeChambre à la base de données
            context.TypeChambres.Add(typeChambre);

            // Sauvegarde des changements dans la base de données
            context.SaveChanges();

            // Retourne un DTO contenant les informations du type de chambre ajouté
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

    // Récupère un type de chambre spécifique par son identifiant
    public TypeChambreDTO GetTypeChambreById(Guid PkTypId)
    {
        using (var db = new MyDbContext())
        {
            // Recherche du type de chambre correspondant à l'ID donné
            var typeChambre = db.TypeChambres
                .Where(t => t.PkTypId == PkTypId)  // Filtre par ID
                .Select(t => new TypeChambreDTO
                {
                    PkTypId = t.PkTypId,                    // Identifiant unique
                    TypNomType = t.TypNomType,              // Nom du type de chambre
                    TypPrixPlancher = t.TypPrixPlancher,    // Prix plancher
                    TypPrixPlafond = t.TypPrixPlafond,      // Prix plafond
                    TypDescription = t.TypDescription       // Description
                }).FirstOrDefault();  // Retourne le premier résultat (ou null si non trouvé)

            // Retourne le DTO trouvé ou null si aucun type de chambre correspondant n'a été trouvé
            return typeChambre;
        }
    }
}
