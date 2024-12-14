using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Hotel_CodeBase.DTO
{
    public class ClientDTO
    {
        // Identifiant unique pour le client, utilisé comme clé primaire dans la base de données.
        [Column(TypeName = "uniqueidentifier")]
        public Guid PkCliId { get; set; } // PK_CLI_id (Clé primaire)

        // Prénom du client. La longueur maximale de ce champ est de 50 caractères.
        [Column(TypeName = "varchar(50)")]
        public string? CliPrenom { get; set; } // CLI_prenom

        // Nom du client. La longueur maximale de ce champ est de 50 caractères.
        [Column(TypeName = "varchar(50)")]
        public string? CliNom { get; set; } // CLI_nom

        // Adresse de résidence du client. La longueur maximale de ce champ est de 100 caractères.
        [Column(TypeName = "varchar(100)")]
        public string? CliAddresseResidence { get; set; } // CLI_adresseResidence

        // Numéro de téléphone mobile du client, de type 'char' pour garantir qu'il ait exactement 15 caractères.
        [Column(TypeName = "char(15)")]
        public string? CliTelephoneMobile { get; set; } // CLI_telephoneMobile

        // Adresse email du client. La longueur maximale de ce champ est de 75 caractères.
        [Column(TypeName = "varchar(75)")]
        public string? CliCourriel { get; set; } // CLI_courriel

        // Mot de passe du client, de longueur maximale de 64 caractères pour stocker un hash sécurisé.
        [Column(TypeName = "varchar(64)")]
        public string? CliMotDePasse { get; set; } // CLI_motDePasse
    }
}
