using SupplierPortal.API.Dtos;

namespace SupplierPortal.API.Services.Interfaces
{
    public interface IProveedorService
    {
        Task<IEnumerable<ProveedorDto>> GetAllAsync();
        Task<ProveedorDto?> GetByIdAsync(int id);
        Task<ProveedorDto> AddAsync(ProveedorDto proveedor);
        Task<ProveedorDto> UpdateAsync(ProveedorDto proveedor);
        Task<bool> DeleteAsync(int id);
    }
}
