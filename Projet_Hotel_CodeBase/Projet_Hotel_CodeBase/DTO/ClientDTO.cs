using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Hotel_CodeBase.DTO
{
    public class ClientDTO
    {
        [Column(TypeName = "uniqueidentifier")]
        public Guid PkCliId { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? CliPrenom { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? CliNom { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? CliAddresseResidence { get; set; }

        [Column(TypeName = "char(15)")]
        public string? CliTelephoneMobile { get; set; }

        [Column(TypeName = "varchar(75)")]
        public string? CliCourriel { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string? CliMotDePasse { get; set; }
    }
}
