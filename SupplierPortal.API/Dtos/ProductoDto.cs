namespace SupplierPortal.API.Dtos
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Enlace { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
    }
}
