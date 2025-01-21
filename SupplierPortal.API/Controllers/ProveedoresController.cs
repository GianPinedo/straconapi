using Microsoft.AspNetCore.Mvc;
using SupplierPortal.API.Dtos;
using SupplierPortal.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization; 

namespace SupplierPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedorService _proveedorService;

        public ProveedoresController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proveedores = await _proveedorService.GetAllAsync();
            return Ok(new StatusResponse<IEnumerable<ProveedorDto>>(true, "Proveedores obtenidos exitosamente.", proveedores));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var proveedor = await _proveedorService.GetByIdAsync(id);
            if (proveedor == null)
                return NotFound(new StatusResponse<string>(false, $"El proveedor con ID {id} no existe."));

            return Ok(new StatusResponse<ProveedorDto>(true, "Proveedor obtenido exitosamente.", proveedor));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProveedorDto proveedor)
        {
            var nuevoProveedor = await _proveedorService.AddAsync(proveedor);
            return CreatedAtAction(nameof(GetById), new { id = nuevoProveedor.Id },
                new StatusResponse<ProveedorDto>(true, "Proveedor creado exitosamente.", nuevoProveedor));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProveedorDto proveedor)
        {
            if (id != proveedor.Id)
                return BadRequest(new StatusResponse<string>(false, "El ID proporcionado no coincide con el del proveedor."));

            var actualizado = await _proveedorService.UpdateAsync(proveedor);
            return Ok(new StatusResponse<ProveedorDto>(true, "Proveedor actualizado exitosamente.", actualizado));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _proveedorService.DeleteAsync(id);
            if (!eliminado)
                return NotFound(new StatusResponse<string>(false, $"El proveedor con ID {id} no existe."));

            return Ok(new StatusResponse<string>(true, "Proveedor eliminado exitosamente."));
        }
    }
}
