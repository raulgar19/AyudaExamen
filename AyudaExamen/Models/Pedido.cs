using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AyudaExamen.Models
{
    [Table("pedidos")]
    public class Pedido
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("pedido_id")]
        public int PedidoId { get; set; }

        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [Column("comic_id")]
        public int ComicId { get; set; }
    }
}
