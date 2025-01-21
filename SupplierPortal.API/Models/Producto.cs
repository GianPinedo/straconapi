namespace SupplierPortal.API.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Enlace { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }

        public ICollection<ProductoSolicitudCompra> Detalles { get; set; } = new List<ProductoSolicitudCompra>();
    }
}
