using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AyudaExamen.Models
{
    [Table("comics")]
    public class Comic
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nombre")]
        public string Name { get; set; }

        [Column("autor")]
        public string Author { get; set; }

        [Column("anio")]
        public int Year { get; set; }

        [Column("descripcion")]
        public string Description { get; set; }
    }
}