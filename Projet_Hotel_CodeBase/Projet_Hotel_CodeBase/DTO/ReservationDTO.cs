using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projet_Hotel_CodeBase.DTO
{
    public class ReservationDTO
    {
        [Column(TypeName = "uniqueidentifier")]
        public Guid PkResId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ResDateDebut { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ResDateFin { get; set; }

        [Column(TypeName = "money")]
        public decimal ResPrixJour { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string ResAutre { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid FkCliId { get; set; }
      
        [Column(TypeName = "uniqueidentifier")]
        public Guid FkChaId { get; set; }


    }

}
