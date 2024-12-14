using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Hotel_CodeBase.DTO
{
    public class TypeChambreDTO
    {
        // Identifiant unique pour le type de chambre, utilisé comme clé primaire.
        [Column(TypeName = "uniqueidentifier")]
        public Guid PkTypId { get; set; } // PK_TYP_id (Clé primaire)

        // Nom du type de chambre (ex. "Chambre Standard", "Suite", etc.). Ce champ a une longueur maximale de 30 caractères.
        [Column(TypeName = "varchar(30)")]
        public string? TypNomType { get; set; } // TYP_nomType

        // Le prix minimum du type de chambre. Le type 'float' est utilisé pour les valeurs décimales représentant des prix.
        [Column(TypeName = "float")]
        public double TypPrixPlancher { get; set; } // TYP_prixPlancher

        // Le prix maximum du type de chambre. Ce champ est également de type 'float' pour représenter une plage de prix.
        [Column(TypeName = "float")]
        public double TypPrixPlafond { get; set; } // TYP_prixPlafond

        // Description du type de chambre. Ce champ peut contenir jusqu'à 300 caractères et fournit des informations supplémentaires sur ce type de chambre (ex. caractéristiques, équipements, etc.).
        [Column(TypeName = "varchar(300)")]
        public string? TypDescription { get; set; } // TYP_description (description du type de chambre)
    }
}
