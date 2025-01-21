namespace SupplierPortal.API.Models
{
    public class ProductoSolicitudCompra
    {
        public int Id { get; set; }
        public int SolicitudCompraId { get; set; }
        public SolicitudCompra? SolicitudCompra { get; set; }

        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario; // Calculado automáticamente
    }
}
