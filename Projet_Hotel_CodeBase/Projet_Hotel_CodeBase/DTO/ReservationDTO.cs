using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Hotel_CodeBase.DTO
{
    public class ReservationDTO
    {
        // Identifiant unique pour la réservation, utilisé comme clé primaire.
        [Column(TypeName = "uniqueidentifier")]
        public Guid PkResId { get; set; } // PK_RES_id (Clé primaire)

        // Date de début de la réservation. Ce champ peut être nul si la date n'est pas encore définie.
        [Column(TypeName = "datetime")]
        public DateTime? ResDateDebut { get; set; } // RES_dateDebut

        // Date de fin de la réservation. Ce champ peut être nul si la date n'est pas encore définie.
        [Column(TypeName = "datetime")]
        public DateTime? ResDateFin { get; set; } // RES_dateFin

        // Prix par jour de la réservation. Ce champ est de type 'money' pour stocker une valeur monétaire.
        [Column(TypeName = "money")]
        public decimal ResPrixJour { get; set; } // RES_prixJour

        // Autres informations liées à la réservation. Peut contenir jusqu'à 300 caractères.
        [Column(TypeName = "varchar(300)")]
        public string? ResAutre { get; set; } // RES_autre (autres détails de la réservation)

        // Identifiant du client associé à la réservation. Il s'agit d'une clé étrangère vers la table des clients.
        [Column(TypeName = "uniqueidentifier")]
        public Guid FkCliId { get; set; } // FkCliId (Clé étrangère vers le client)

        // Identifiant de la chambre associée à la réservation. Il s'agit d'une clé étrangère vers la table des chambres.
        [Column(TypeName = "uniqueidentifier")]
        public Guid FkChaId { get; set; } // FkChaId (Clé étrangère vers la chambre)
    }
}
