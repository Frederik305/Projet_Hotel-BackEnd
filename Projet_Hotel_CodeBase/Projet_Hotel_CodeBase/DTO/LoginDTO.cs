using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Hotel_CodeBase.DTO
{
    public class LoginDTO
    {
        // Adresse e-mail de l'utilisateur. La longueur maximale de ce champ est de 75 caractères.
        [Column(TypeName = "varchar(75)")]
        public string? LogCourriel { get; set; } // Log_courriel (Adresse e-mail de l'utilisateur)

        // Mot de passe de l'utilisateur. La longueur maximale de ce champ est de 64 caractères pour stocker un hash sécurisé.
        [Column(TypeName = "varchar(64)")]
        public string? LogMotDePasse { get; set; } // Log_motDePasse (Mot de passe, généralement stocké sous forme de hash)
    }
}
