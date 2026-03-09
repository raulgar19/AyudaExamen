using AyudaExamen.Models;
using Microsoft.EntityFrameworkCore;

namespace AyudaExamen.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

        public DbSet<Hospital> Hospitales { get; set; }
        public DbSet<Sala> Salas { get; set; }
    }
}