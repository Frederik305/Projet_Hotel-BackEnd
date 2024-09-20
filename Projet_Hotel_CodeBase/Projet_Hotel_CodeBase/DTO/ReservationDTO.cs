namespace Projet_Hotel_CodeBase.DTO
{
    public class ReservationDTO
    {
        public Guid PkResId { get; set; }
        public DateTime ResDateDebut { get; set; }
        public DateTime ResDateFin { get; set; }
        public decimal ResPrixJour { get; set; }
        public string ResAutre { get; set; }
        public short ResChambre { get; set; }
        public  string ResCliCourriel { get; set; }

    }

}
