using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SupplierPortal.API.Dtos;
using SupplierPortal.API.Services.Interfaces;

namespace SupplierPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesCompraController : ControllerBase
    {
        private readonly ISolicitudCompraService _solicitudCompraService;

        public SolicitudesCompraController(ISolicitudCompraService solicitudCompraService)
        {
            _solicitudCompraService = solicitudCompraService;
        }

        
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var solicitudes = await _solicitudCompraService.GetAllAsync();
            return Ok(new StatusResponse<IEnumerable<DetalleSolicitudDto>>(true, "Solicitudes obtenidas exitosamente.", solicitudes));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var solicitud = await _solicitudCompraService.GetByIdAsync(id);
            if (solicitud == null)
                return NotFound(new StatusResponse<string>(false, $"La solicitud con ID {id} no existe."));

            return Ok(new StatusResponse<DetalleSolicitudDto>(true, "Solicitud obtenida exitosamente.", solicitud));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DetalleSolicitudDto solicitudDto)
        {
            var nuevaSolicitud = await _solicitudCompraService.AddAsync(solicitudDto);
            return CreatedAtAction(nameof(GetById), new { id = nuevaSolicitud.SolicitudId },
                new StatusResponse<DetalleSolicitudDto>(true, "Solicitud creada exitosamente.", nuevaSolicitud));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DetalleSolicitudDto solicitudDto)
        {
            if (id != solicitudDto.SolicitudId)
                return BadRequest(new StatusResponse<string>(false, "El ID proporcionado no coincide con el de la solicitud."));

            var actualizada = await _solicitudCompraService.UpdateAsync(solicitudDto);
            return Ok(new StatusResponse<DetalleSolicitudDto>(true, "Solicitud actualizada exitosamente.", actualizada));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _solicitudCompraService.DeleteAsync(id);
            if (!eliminado)
                return NotFound(new StatusResponse<string>(false, $"La solicitud con ID {id} no existe."));

            return Ok(new StatusResponse<string>(true, "Solicitud eliminada exitosamente."));
        }

        [Authorize]
        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> UpdateEstado(int id, [FromBody] ActualizarEstadoDto estadoDto)
        {
            if (estadoDto == null || string.IsNullOrEmpty(estadoDto.Estado))
            {
                return BadRequest(new StatusResponse<string>(false, "El campo 'Estado' es obligatorio."));
            }

            try
            {
                // Llama al servicio para actualizar el estado
                var solicitudActualizada = await _solicitudCompraService.UpdateEstadoAsync(id, estadoDto.Estado);
                return Ok(new StatusResponse<string>(true, "Estado actualizado exitosamente."));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new StatusResponse<string>(false, ex.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateEstado: {ex.Message}");
                return StatusCode(500, new StatusResponse<string>(false, "Ocurrió un error al actualizar el estado."));
            }
        }

        
    }
}
