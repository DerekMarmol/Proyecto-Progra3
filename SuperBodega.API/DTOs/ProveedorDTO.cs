namespace SuperBodega.API.DTOs
{
    public class ProveedorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string NIT { get; set; }
        public bool Activo { get; set; }
    }

    public class ProveedorCreacionDTO
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string NIT { get; set; }
    }
}