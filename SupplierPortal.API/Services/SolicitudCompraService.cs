using Microsoft.EntityFrameworkCore;
using SupplierPortal.API.Data;
using SupplierPortal.API.Models;
using SupplierPortal.API.Services.Interfaces;
using SupplierPortal.API.Dtos;

namespace SupplierPortal.API.Services
{
    public class SolicitudCompraService : ISolicitudCompraService
    {
        private readonly ApplicationDbContext _context;

        public SolicitudCompraService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DetalleSolicitudDto>> GetAllAsync()
        {
            var solicitudes = await _context.SolicitudesCompra
                .Include(s => s.Proveedor)
                .Include(s => s.Usuario)
                .ToListAsync();

            return solicitudes.Select(s => new DetalleSolicitudDto
            {
                SolicitudId = s.Id,
                Proveedor = s.Proveedor?.Nombre,
                Usuario = s.Usuario?.NombreUsuario,
                Fecha = s.Fecha,
                Estado = s.Estado,
                Detalles = new List<DetalleProductoDto>() 
            });
        }

        public async Task<DetalleSolicitudDto?> GetByIdAsync(int id)
        {
            var solicitud = await _context.SolicitudesCompra
                .Include(s => s.Detalles)
                .ThenInclude(d => d.Producto)
                .Include(s => s.Proveedor)
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (solicitud == null)
                return null;

            return new DetalleSolicitudDto
            {
                SolicitudId = solicitud.Id,
                Proveedor = solicitud.Proveedor?.Nombre,
                Usuario = solicitud.Usuario?.NombreUsuario,
                Fecha = solicitud.Fecha,
                Estado = solicitud.Estado,
                Detalles = solicitud.Detalles.Select(d => new DetalleProductoDto
                {
                    Producto = d.Producto.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                })
            };
        }

        public async Task<DetalleSolicitudDto> AddAsync(DetalleSolicitudDto solicitudDto)
        {
            try
            {
                // Crear la entidad SolicitudCompra
                var solicitud = new SolicitudCompra
                {
                    ProveedorId = int.Parse(solicitudDto.Proveedor ?? "0"),
                    UsuarioId = int.Parse(solicitudDto.Usuario ?? "0"),
                    Fecha = solicitudDto.Fecha,
                    Estado = solicitudDto.Estado
                };

                // Log de la solicitud de compra
                Console.WriteLine($"Insertando SolicitudCompra: ProveedorId={solicitud.ProveedorId}, UsuarioId={solicitud.UsuarioId}, Fecha={solicitud.Fecha}, Estado={solicitud.Estado}");

                // Agregar la solicitud a la base de datos
                _context.SolicitudesCompra.Add(solicitud);
                await _context.SaveChangesAsync();

                // Log del ID generado
                Console.WriteLine($"SolicitudCompra insertada con ID={solicitud.Id}");

                // Insertar detalles en ProductoSolicitudCompra
                var detalles = solicitudDto.Detalles.Select(d => new ProductoSolicitudCompra
                {
                    SolicitudCompraId = solicitud.Id,
                    ProductoId = int.Parse(d.Producto),
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                }).ToList();

                // Log de los detalles
                foreach (var detalle in detalles)
                {
                    Console.WriteLine($"Insertando ProductoSolicitudCompra: SolicitudCompraId={detalle.SolicitudCompraId}, ProductoId={detalle.ProductoId}, Cantidad={detalle.Cantidad}, PrecioUnitario={detalle.PrecioUnitario}");
                }

                _context.ProductosSolicitudesCompra.AddRange(detalles);
                await _context.SaveChangesAsync();

                // Actualizar el DTO con el ID generado de la solicitud
                solicitudDto.SolicitudId = solicitud.Id;
                return solicitudDto;
            }
            catch (Exception ex)
            {
                // Registrar el error en el log
                Console.Error.WriteLine($"Error al procesar la solicitud de compra: {ex.Message}");
                Console.Error.WriteLine($"Detalle del error: {ex.StackTrace}");

                // Propagar la excepción para que el controlador la maneje
                throw new InvalidOperationException("Error al procesar la solicitud de compra. Verifica los datos proporcionados.", ex);
            }
        }



        public async Task<DetalleSolicitudDto> UpdateAsync(DetalleSolicitudDto solicitudDto)
        {
            var solicitud = await _context.SolicitudesCompra.FindAsync(solicitudDto.SolicitudId);

            if (solicitud == null)
                throw new Exception("Solicitud no encontrada");

            solicitud.ProveedorId = int.Parse(solicitudDto.Proveedor ?? "0");
            solicitud.UsuarioId = int.Parse(solicitudDto.Usuario ?? "0");
            solicitud.Fecha = solicitudDto.Fecha;
            solicitud.Estado = solicitudDto.Estado;

            _context.SolicitudesCompra.Update(solicitud);
            await _context.SaveChangesAsync();

            return solicitudDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var solicitud = await _context.SolicitudesCompra
                .Include(s => s.Detalles)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (solicitud == null)
                return false;

            _context.ProductosSolicitudesCompra.RemoveRange(solicitud.Detalles);        

            _context.SolicitudesCompra.Remove(solicitud);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DetalleSolicitudDto?> GetDetalleSolicitudAsync(int id)
        {
            var solicitud = await _context.SolicitudesCompra
                .Include(s => s.Detalles)
                .ThenInclude(d => d.Producto)
                .Include(s => s.Proveedor)
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (solicitud == null)
                return null;

            return new DetalleSolicitudDto
            {
                SolicitudId = solicitud.Id,
                Proveedor = solicitud.Proveedor?.Nombre,
                Usuario = solicitud.Usuario?.NombreUsuario,
                Fecha = solicitud.Fecha,
                Estado = solicitud.Estado,
                Detalles = solicitud.Detalles.Select(d => new DetalleProductoDto
                {
                    Producto = d.Producto.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                })
            };
        }
    
        public async Task<SolicitudCompra> UpdateEstadoAsync(int id, string nuevoEstado)
        {
            // Busca la solicitud de compra en la base de datos
            var solicitud = await _context.SolicitudesCompra.FindAsync(id);

            if (solicitud == null)
            {
                throw new InvalidOperationException($"SolicitudCompra con ID {id} no encontrada.");
            }

            // Actualiza el estado
            solicitud.Estado = nuevoEstado;

            try
            {
                _context.SolicitudesCompra.Update(solicitud);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Registrar el error para depuración
                Console.WriteLine($"Error al actualizar el estado: {ex.Message}");
                throw new Exception("Error al actualizar el estado en la base de datos.", ex);
            }

            return solicitud;
        }
    
    }
}
