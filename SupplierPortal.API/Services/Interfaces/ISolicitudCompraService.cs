using SupplierPortal.API.Models;
using SupplierPortal.API.Dtos; // Add this line if DetalleSolicitudDto is in the DTOs namespace

namespace SupplierPortal.API.Services.Interfaces
{
    public interface ISolicitudCompraService
    {
        Task<IEnumerable<DetalleSolicitudDto>> GetAllAsync();
        Task<DetalleSolicitudDto?> GetByIdAsync(int id);
        Task<DetalleSolicitudDto> AddAsync(DetalleSolicitudDto solicitudCompra);
        Task<DetalleSolicitudDto> UpdateAsync(DetalleSolicitudDto solicitudCompra);
        Task<bool> DeleteAsync(int id);
        Task<DetalleSolicitudDto?> GetDetalleSolicitudAsync(int solicitudId);
        Task<SolicitudCompra> UpdateEstadoAsync(int id, string nuevoEstado);
    }
}
