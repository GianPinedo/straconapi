using Microsoft.EntityFrameworkCore;
using SupplierPortal.API.Models;

namespace SupplierPortal.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<SolicitudCompra> SolicitudesCompra { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ProductoSolicitudCompra> ProductosSolicitudesCompra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed para Usuarios
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, NombreUsuario = "colocador1", Contraseña = "colocador123", Rol = "Colocador" },
                new Usuario { Id = 2, NombreUsuario = "aprobador1", Contraseña = "aprobador123", Rol = "Aprobador" }
            );

            // Seed para Proveedores
            modelBuilder.Entity<Proveedor>().HasData(
                new Proveedor { Id = 1, Nombre = "Proveedor A", Direccion = "Calle 123", Contacto = "contacto@proveedora.com" },
                new Proveedor { Id = 2, Nombre = "Proveedor B", Direccion = "Avenida 456", Contacto = "contacto@proveedorb.com" },
                new Proveedor { Id = 3, Nombre = "Proveedor C", Direccion = "Pasaje 789", Contacto = "contacto@proveedorc.com" }
            );
            modelBuilder.Entity<Producto>()
            .Property(p => p.PrecioUnitario)
            .HasPrecision(18, 2); 
            // Seed para Productos
            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Nombre = "Producto 1", Enlace = "https://example.com/producto1", PrecioUnitario = 10.5m },
                new Producto { Id = 2, Nombre = "Producto 2", Enlace = "https://example.com/producto2", PrecioUnitario = 20m },
                new Producto { Id = 3, Nombre = "Producto 3", Enlace = "https://example.com/producto3", PrecioUnitario = 15m },
                new Producto { Id = 4, Nombre = "Producto 4", Enlace = "https://example.com/producto4", PrecioUnitario = 50m }
            );

            modelBuilder.Entity<ProductoSolicitudCompra>()
                .HasOne(p => p.SolicitudCompra)
                .WithMany(s => s.Detalles)
                .HasForeignKey(p => p.SolicitudCompraId);

            modelBuilder.Entity<ProductoSolicitudCompra>()
                .HasOne(p => p.Producto)
                .WithMany(p => p.Detalles)
                .HasForeignKey(p => p.ProductoId);

            // Configurar la precisión para PrecioUnitario
            modelBuilder.Entity<ProductoSolicitudCompra>()
                .Property(p => p.PrecioUnitario)
                .HasPrecision(18, 2);

            // Seed para Solicitudes de Compra
            modelBuilder.Entity<SolicitudCompra>().HasData(
                new SolicitudCompra { Id = 1, ProveedorId = 1, UsuarioId = 1, Fecha = new DateTime(2025, 1, 1), Estado = "Pendiente" }
            );
            modelBuilder.Entity<ProductoSolicitudCompra>()
                .Property(p => p.PrecioUnitario)
                .HasPrecision(18, 2); // Asegúrate de que esto coincide con el esquema de la base de datos

            modelBuilder.Entity<ProductoSolicitudCompra>()
                .HasOne(p => p.SolicitudCompra)
                .WithMany(s => s.Detalles)
                .HasForeignKey(p => p.SolicitudCompraId)
                .OnDelete(DeleteBehavior.Restrict); // Asegúrate de que no se intenta eliminar en cascada

            modelBuilder.Entity<ProductoSolicitudCompra>()
                .HasOne(p => p.Producto)
                .WithMany(p => p.Detalles)
                .HasForeignKey(p => p.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductoSolicitudCompra>().HasData(
                new ProductoSolicitudCompra { Id = 1, SolicitudCompraId = 1, ProductoId = 1, Cantidad = 2, PrecioUnitario = 10.5m },
                new ProductoSolicitudCompra { Id = 2, SolicitudCompraId = 1, ProductoId = 2, Cantidad = 1, PrecioUnitario = 20m }
            );

        }
    }
}
