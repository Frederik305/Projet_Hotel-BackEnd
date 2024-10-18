namespace Projet_Hotel_CodeBase.DTO
{
    public class ChambreDTO
    {
        public Guid PkChaId { get; set; } // PK_CHA_id (Primary key)
        public short ChaNumero { get; set; } // CHA_numero
        public bool ChaEtat { get; set; } // CHA_etat
        public string? ChaAutreInfo { get; set; }
        public Guid FKTypId { get; set; }
    }
}
