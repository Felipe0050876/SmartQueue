using Microsoft.EntityFrameworkCore;
using SmartQueue.Models;

namespace SmartQueue.Data
{
    public class SmartQueueContext : DbContext
    {
        public SmartQueueContext(DbContextOptions<SmartQueueContext> opciones)
            : base(opciones)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<SolicitudAtencion> Solicitudes { get; set; }
        public DbSet<Turno> Turnos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacion 1 a 1: cada solicitud tiene un unico turno
            modelBuilder.Entity<SolicitudAtencion>()
                .HasOne(s => s.Turno)
                .WithOne(t => t.SolicitudAtencion)
                .HasForeignKey<Turno>(t => t.SolicitudAtencionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Usuario inicial (HU-00). La contrasena esta guardada como hash SHA-256.
            // Credenciales reales: correo admin@smartqueue.com / contrasena Admin123
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = 1,
                Nombre = "Administrador",
                Correo = "admin@smartqueue.com",
                Contrasena = "3b612c75a7b5048a435fb6ec81e52ff92d6d795a8b5a9c17070f6a63c97a53b2"
            });
        }
    }
}
