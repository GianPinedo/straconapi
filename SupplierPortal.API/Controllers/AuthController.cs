using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SupplierPortal.API.Models;
using SupplierPortal.API.Data;
using SupplierPortal.API.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SupplierPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.NombreUsuario) || string.IsNullOrEmpty(loginRequest.Contraseña))
            {
                return BadRequest(new StatusResponse<string>(false, "Los campos de usuario y contraseña son obligatorios."));
            }

            // Validar las credenciales del usuario
            var user = _context.Usuarios
                .FirstOrDefault(u => u.NombreUsuario == loginRequest.NombreUsuario && u.Contraseña == loginRequest.Contraseña);

            if (user == null)
            {
                return Unauthorized(new StatusResponse<string>(false, "Credenciales incorrectas."));
            }

            // Generar el token JWT
            var token = GenerateJwtToken(user);

            // Construir la respuesta con el token y los datos del usuario
            var response = new
            {
                Token = token,
                Usuario = new
                {
                    user.Id,
                    user.NombreUsuario,
                    user.Rol
                }
            };

            return Ok(new StatusResponse<object>(true, "Inicio de sesión exitoso.", response));
        }

        private string GenerateJwtToken(Usuario user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];

            if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer))
            {
                throw new InvalidOperationException("Las configuraciones de JWT no están correctamente configuradas.");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.NombreUsuario),
                new Claim("rol", user.Rol),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtIssuer,
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
