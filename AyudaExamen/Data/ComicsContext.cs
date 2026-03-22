using AyudaExamen.Models;
using Microsoft.EntityFrameworkCore;

namespace AyudaExamen.Data
{
    public class ComicsContext : DbContext
    {
        public ComicsContext(DbContextOptions<ComicsContext> options) : base(options) { }

        public DbSet<Comic> Comics { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}