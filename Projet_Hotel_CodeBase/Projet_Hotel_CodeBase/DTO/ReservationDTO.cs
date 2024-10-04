using System.Text.Json.Serialization;

namespace Projet_Hotel_CodeBase.DTO
{
    public class ReservationDTO
    {
        public Guid PkResId { get; set; }
        public DateTime ResDateDebut { get; set; }
        public DateTime ResDateFin { get; set; }
        public decimal ResPrixJour { get; set; }
        public string ResAutre { get; set; }

        [JsonIgnore]
        public Guid FkCliId { get; set; }
        [JsonIgnore]
        public Guid FkChaId { get; set; }
        public short ResChambre { get; set; }
        public  string ResCliCourriel { get; set; }

        [JsonIgnore]
        Client client { get; set; }
        [JsonIgnore]
        public string ClientName { get; set; }

        [JsonIgnore]
        Chambre chambre { get; set; }
        public string numChambre { get; set; }

    }

}
