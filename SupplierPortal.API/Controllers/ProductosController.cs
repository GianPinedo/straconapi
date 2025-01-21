using Microsoft.AspNetCore.Mvc;
using SupplierPortal.API.Dtos;
using SupplierPortal.API.Services.Interfaces;

namespace SupplierPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _productoService.GetAllAsync();
            return Ok(new StatusResponse<IEnumerable<ProductoDto>>(true, "Productos obtenidos exitosamente.", productos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null)
                return NotFound(new StatusResponse<string>(false, $"El producto con ID {id} no existe."));

            return Ok(new StatusResponse<ProductoDto>(true, "Producto obtenido exitosamente.", producto));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductoDto producto)
        {
            var nuevoProducto = await _productoService.AddAsync(producto);
            return CreatedAtAction(nameof(GetById), new { id = nuevoProducto.Id },
                new StatusResponse<ProductoDto>(true, "Producto creado exitosamente.", nuevoProducto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductoDto producto)
        {
            if (id != producto.Id)
                return BadRequest(new StatusResponse<string>(false, "El ID proporcionado no coincide con el del producto."));

            var actualizado = await _productoService.UpdateAsync(producto);
            return Ok(new StatusResponse<ProductoDto>(true, "Producto actualizado exitosamente.", actualizado));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _productoService.DeleteAsync(id);
            if (!eliminado)
                return NotFound(new StatusResponse<string>(false, $"El producto con ID {id} no existe."));

            return Ok(new StatusResponse<string>(true, "Producto eliminado exitosamente."));
        }
    }
}
