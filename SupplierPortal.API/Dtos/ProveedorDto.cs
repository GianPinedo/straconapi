namespace SupplierPortal.API.Dtos
{
    public class ProveedorDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Contacto { get; set; } = string.Empty;
    }
}
