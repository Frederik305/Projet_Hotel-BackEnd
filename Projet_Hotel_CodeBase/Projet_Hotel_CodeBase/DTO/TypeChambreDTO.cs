namespace Projet_Hotel_CodeBase.DTO
{
    public class TypeChambreDTO
    {
        public Guid PkTypId { get; set; }
        public string TypNomType { get; set; }
        public double TypPrixPlancher { get; set; }
        public double TypPrixPlafond { get; set; }
        public string TypDescription { get; set; }
    }

}
