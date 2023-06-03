using MiChamba.Models;
using Microsoft.EntityFrameworkCore;

namespace MiChamba.Data
{
    public class MiChambaDbContext : DbContext
    {
        // CONSTRUCTOR
        public MiChambaDbContext(DbContextOptions<MiChambaDbContext> options) : base(options) 
        {

        }


        // PROPIEDADES - TABLAS
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Oferta> Ofertas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Postulacion> Postulaciones { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Recurso> Recursos { get; set; }
        public DbSet<Curriculum> Curriculums { get; set; }


        //LLaves foraneas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
