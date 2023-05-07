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



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Oferta>()
            .HasOne(o => o.Empresa)
            .WithMany(e => e.Ofertas)
            .HasForeignKey(o => o.IdEmpresa);

            modelBuilder.Entity<Postulacion>()
                .HasOne(p => p.Oferta)
                .WithMany(o => o.Postulaciones)
                .HasForeignKey(p => p.IdOferta);

            modelBuilder.Entity<Postulacion>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Postulaciones)
                .HasForeignKey(p => p.IdUsuario);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Empresa)
                .WithMany(e => e.Calificaciones)
                .HasForeignKey(c => c.IdEmpresa);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Calificaciones)
                .HasForeignKey(c => c.IdUsuario);
        }

    }
}
