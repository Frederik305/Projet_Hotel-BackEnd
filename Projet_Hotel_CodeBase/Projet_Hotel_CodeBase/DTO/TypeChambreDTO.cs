namespace Projet_Hotel_CodeBase.DTO
{
    public class TypeChambreDTO
    {
        public Guid PkTypId { get; set; }
        public string TypNomType { get; set; }
        public Double TypPrixPlancher {  get; set; }
        public Double TypPrixPlafond { get; set; }
        public string TypDescription { get; set; }
    }
}
