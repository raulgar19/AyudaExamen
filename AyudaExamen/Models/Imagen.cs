using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AyudaExamen.Models
{
    [Table("imagenes")]
    public class Imagen
    {
        [Key]
        [Column("comic_id")]
        public int Id { get; set; }

        [Column("imagen_url")]
        public string Ürl { get; set; }
    }
}