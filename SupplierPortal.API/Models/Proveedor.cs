using  System.Collections.Generic;
using SupplierPortal.API.Models; // Asegúrate de que este using esté presente

namespace SupplierPortal.API.Models
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Contacto { get; set; } = string.Empty;

        // Relación con Solicitudes de Compra
        public ICollection<SolicitudCompra> SolicitudesCompra { get; set; } = new List<SolicitudCompra>();
    }
}
