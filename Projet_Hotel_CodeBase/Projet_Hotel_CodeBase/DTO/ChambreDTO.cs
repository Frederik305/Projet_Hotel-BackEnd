﻿using System.Text.Json.Serialization;

namespace Projet_Hotel_CodeBase.DTO
{
    public class ChambreDTO
    {
        
        public short ChaNumero { get; set; }
        public bool ChaEtat { get; set; }
        public string ChaAutreInfo { get; set; }

        [JsonIgnore]
        TypeChambre typeChambre { get; set; }
        public string NomTypeChambre { get; set; }
        
    }

}
