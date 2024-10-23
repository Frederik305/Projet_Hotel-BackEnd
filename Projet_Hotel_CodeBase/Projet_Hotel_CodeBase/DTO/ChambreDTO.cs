using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Hotel_CodeBase.DTO
{
    public class ChambreDTO
    {
        [Column(TypeName = "uniqueidentifier")]
        public Guid PkChaId { get; set; } // PK_CHA_id (Primary key)

        [Column(TypeName = "smallint")]
        public short ChaNumero { get; set; } // CHA_numero

        [Column(TypeName = "bit")]
        public bool ChaEtat { get; set; } // CHA_etat

        [Column(TypeName = "varchar(300)")]
        public string? ChaAutreInfo { get; set; } // CHA_autreInfo (length: 300)

        [Column(TypeName = "uniqueidentifier")]
        public Guid FkTypId { get; set; }
    }
}
