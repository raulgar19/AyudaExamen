using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AyudaExamen.Models
{
    [Table("imagenes")]
    public class Imagen
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("comic_id")]
        public int ComicId { get; set; }

        [Column("imagen_url")]
        public string Url { get; set; }
    }
}