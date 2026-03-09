using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AyudaExamen.Models
{
    [Table("SALA")]
    public class Sala
    {
        [Key]
        [Column("SALA_COD")]
        public int IdSala { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("NUM_CAMA")]
        public int Camas { get; set; }

        [Column("HOSPITAL_COD")]
        public string HospitalId { get; set; }
    }
}