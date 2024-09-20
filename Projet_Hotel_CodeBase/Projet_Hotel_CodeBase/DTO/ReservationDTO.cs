namespace Projet_Hotel_CodeBase.DTO
{
    public class ReservationDTO
    {
        public Guid PkResId { get; set; }
        public DateTime ResDateDebut { get; set; }
        public DateTime ResDateFin { get; set; }
        public Double ResPrixJour { get; set; }
        public string ResAutre { get; set; }
        public string FkCliId { get; set; }
        public string FkClhaId { get; set; }
    }
}
