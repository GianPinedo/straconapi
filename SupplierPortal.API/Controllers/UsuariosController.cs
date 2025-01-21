using Microsoft.AspNetCore.Mvc;
using SupplierPortal.API.Dtos;
using SupplierPortal.API.Models;
using SupplierPortal.API.Services.Interfaces;

namespace SupplierPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(new StatusResponse<IEnumerable<Usuario>>(true, "Usuarios obtenidos exitosamente.", usuarios));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
                return NotFound(new StatusResponse<string>(false, $"El usuario con ID {id} no existe."));

            return Ok(new StatusResponse<Usuario>(true, "Usuario obtenido exitosamente.", usuario));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            var nuevoUsuario = await _usuarioService.AddAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { id = nuevoUsuario.Id },
                new StatusResponse<Usuario>(true, "Usuario creado exitosamente.", nuevoUsuario));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id)
                return BadRequest(new StatusResponse<string>(false, "El ID proporcionado no coincide con el del usuario."));

            var actualizado = await _usuarioService.UpdateAsync(usuario);
            return Ok(new StatusResponse<Usuario>(true, "Usuario actualizado exitosamente.", actualizado));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _usuarioService.DeleteAsync(id);
            if (!eliminado)
                return NotFound(new StatusResponse<string>(false, $"El usuario con ID {id} no existe."));

            return Ok(new StatusResponse<string>(true, "Usuario eliminado exitosamente."));
        }
    }
}
