using Microsoft.EntityFrameworkCore;
using NaturalLife.Model;
using NaturalLife.Models;

namespace NaturalLife.Context
{
    public class AppDbContext: DbContext
    {
        private const string conectionstring = "conexion";
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Proceso> Procesos { get; set; }
        public DbSet<Login> Login { get; set; }
    }
}
