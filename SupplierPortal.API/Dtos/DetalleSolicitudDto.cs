namespace SupplierPortal.API.Dtos
{
    public class DetalleSolicitudDto
    {
        public int SolicitudId { get; set; }
        public string? Proveedor { get; set; }
        public string? Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = string.Empty;
        public IEnumerable<DetalleProductoDto> Detalles { get; set; } = new List<DetalleProductoDto>();
    }

    public class DetalleProductoDto
    {
        public int ProductoId { get; set; }
        public string Producto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
