using SupplierPortal.API.Data;
using SupplierPortal.API.Dtos;
using SupplierPortal.API.Models;
using SupplierPortal.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SupplierPortal.API.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly ApplicationDbContext _context;

        public ProveedorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProveedorDto>> GetAllAsync()
        {
            var proveedores = await _context.Proveedores.ToListAsync();
            return proveedores.Select(p => new ProveedorDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Direccion = p.Direccion,
                Contacto = p.Contacto
            });
        }

        public async Task<ProveedorDto?> GetByIdAsync(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null) return null;

            return new ProveedorDto
            {
                Id = proveedor.Id,
                Nombre = proveedor.Nombre,
                Direccion = proveedor.Direccion,
                Contacto = proveedor.Contacto
            };
        }

        public async Task<ProveedorDto> AddAsync(ProveedorDto proveedorDto)
        {
            var proveedor = new Proveedor
            {
                Nombre = proveedorDto.Nombre,
                Direccion = proveedorDto.Direccion,
                Contacto = proveedorDto.Contacto
            };

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            proveedorDto.Id = proveedor.Id;
            return proveedorDto;
        }

        public async Task<ProveedorDto> UpdateAsync(ProveedorDto proveedorDto)
        {
            var proveedor = await _context.Proveedores.FindAsync(proveedorDto.Id);
            if (proveedor == null) throw new Exception("Proveedor no encontrado");

            proveedor.Nombre = proveedorDto.Nombre;
            proveedor.Direccion = proveedorDto.Direccion;
            proveedor.Contacto = proveedorDto.Contacto;

            await _context.SaveChangesAsync();
            return proveedorDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null) return false;

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
