using System;

namespace SuperBodega.API.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Existencia { get; set; }
        public string Categoria { get; set; }
        public string ImagenUrl { get; set; }
        public int? ProveedorId { get; set; }
        public string NombreProveedor { get; set; }
    }

    public class ProductoCreacionDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Existencia { get; set; }
        public string Categoria { get; set; }
        public string ImagenUrl { get; set; }
        public int? ProveedorId { get; set; }
    }
}