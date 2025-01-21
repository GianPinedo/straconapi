using System; // Asegúrate de que este using esté presente para DateTime
using SupplierPortal.API.Models; 
namespace SupplierPortal.API.Models;
public class Usuario
{
    public int Id { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Contraseña { get; set; } = string.Empty; // Nota: Hash y Salt para contraseñas en producción
    public string Rol { get; set; } = "Colocador"; // Valores posibles: "Colocador", "Aprobador"

    // Relación opcional: solicitudes registradas por este usuario
    public ICollection<SolicitudCompra> SolicitudesCompra { get; set; } = new List<SolicitudCompra>();
}
