using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Hotel_CodeBase.DTO
{
    public class ChambreDTO
    {
        // Identifiant unique pour la chambre, utilisé comme clé primaire dans la base de données.
        [Column(TypeName = "uniqueidentifier")]
        public Guid PkChaId { get; set; } // PK_CHA_id (Clé primaire)

        // Numéro de la chambre. Il est de type 'smallint' dans la base de données.
        [Column(TypeName = "smallint")]
        public short ChaNumero { get; set; } // CHA_numero

        // Indicateur de l'état de la chambre. 'true' si la chambre est occupée, 'false' si elle est libre.
        [Column(TypeName = "bit")]
        public bool ChaEtat { get; set; } // CHA_etat

        // Informations supplémentaires concernant la chambre (par exemple, description ou particularités).
        // La longueur maximale de ce champ est de 300 caractères.
        [Column(TypeName = "varchar(300)")]
        public string? ChaAutreInfo { get; set; } // CHA_autreInfo (longueur: 300)

        // Référence à un identifiant unique du type de chambre, utilisé comme clé étrangère.
        [Column(TypeName = "uniqueidentifier")]
        public Guid FkTypId { get; set; } // FkTypId (clé étrangère vers le type de chambre)
    }
}
