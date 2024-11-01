using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Hotel_CodeBase.DTO
{
    public class LoginDTO
    {
        [Column(TypeName = "varchar(75)")]
        public string? LogCourriel { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string? LogMotDePasse { get; set; }
    }
}
