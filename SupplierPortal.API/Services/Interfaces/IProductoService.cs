using SupplierPortal.API.Dtos;
using SupplierPortal.API.Models;

namespace SupplierPortal.API.Services.Interfaces
{
    public interface IProductoService
    {
        /// <summary>
        /// Obtiene todos los productos.
        /// </summary>
        /// <returns>Una lista de productos.</returns>
        Task<IEnumerable<ProductoDto>> GetAllAsync();

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto.</param>
        /// <returns>El producto si existe, de lo contrario null.</returns>
        Task<ProductoDto?> GetByIdAsync(int id);

        /// <summary>
        /// Agrega un nuevo producto.
        /// </summary>
        /// <param name="producto">El producto a agregar.</param>
        /// <returns>El producto agregado.</returns>
        Task<ProductoDto> AddAsync(ProductoDto producto);

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="producto">El producto con los datos actualizados.</param>
        /// <returns>El producto actualizado.</returns>
        Task<ProductoDto> UpdateAsync(ProductoDto producto);

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto a eliminar.</param>
        /// <returns>True si se eliminó correctamente, de lo contrario false.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
