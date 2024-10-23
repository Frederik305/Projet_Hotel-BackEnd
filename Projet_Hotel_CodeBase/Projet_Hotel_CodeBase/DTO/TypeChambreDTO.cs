using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Hotel_CodeBase.DTO
{
    public class TypeChambreDTO
    {
        [Column(TypeName = "uniqueidentifier")]
        public Guid PkTypId { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string TypNomType { get; set; }

        [Column(TypeName = "float")]
        public double TypPrixPlancher { get; set; }

        [Column(TypeName = "float")]
        public double TypPrixPlafond { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string TypDescription { get; set; }
    }

}
