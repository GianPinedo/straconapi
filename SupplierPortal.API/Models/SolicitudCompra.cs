namespace SupplierPortal.API.Models
{
    public class SolicitudCompra
    {
        public int Id { get; set; }
        public int ProveedorId { get; set; }
        public Proveedor? Proveedor { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = "Pendiente";

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public ICollection<ProductoSolicitudCompra> Detalles { get; set; } = new List<ProductoSolicitudCompra>();
    }
}
