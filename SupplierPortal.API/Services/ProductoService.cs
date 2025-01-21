using SupplierPortal.API.Data;
using SupplierPortal.API.Dtos;
using SupplierPortal.API.Models;
using SupplierPortal.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SupplierPortal.API.Services
{
    public class ProductoService : IProductoService
    {
        private readonly ApplicationDbContext _context;

        public ProductoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoDto>> GetAllAsync()
        {
            var productos = await _context.Productos.ToListAsync();
            return productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Enlace = p.Enlace,
                PrecioUnitario = p.PrecioUnitario
            });
        }

        public async Task<ProductoDto?> GetByIdAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return null;

            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Enlace = producto.Enlace,
                PrecioUnitario = producto.PrecioUnitario
            };
        }

        public async Task<ProductoDto> AddAsync(ProductoDto productoDto)
        {
            var producto = new Producto
            {
                Nombre = productoDto.Nombre,
                Enlace = productoDto.Enlace,
                PrecioUnitario = productoDto.PrecioUnitario
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            productoDto.Id = producto.Id;
            return productoDto;
        }

        public async Task<ProductoDto> UpdateAsync(ProductoDto productoDto)
        {
            var producto = await _context.Productos.FindAsync(productoDto.Id);
            if (producto == null) throw new Exception("Producto no encontrado");

            producto.Nombre = productoDto.Nombre;
            producto.Enlace = productoDto.Enlace;
            producto.PrecioUnitario = productoDto.PrecioUnitario;

            await _context.SaveChangesAsync();
            return productoDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return false;

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
