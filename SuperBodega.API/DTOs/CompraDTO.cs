using System;
using System.Collections.Generic;

namespace SuperBodega.API.DTOs
{
    public class CompraDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Observaciones { get; set; }
        public int ProveedorId { get; set; }
        public string NombreProveedor { get; set; }
        public List<DetalleCompraDTO> DetallesCompra { get; set; }
    }

    public class CompraCreacionDTO
    {
        public string Observaciones { get; set; }
        public int ProveedorId { get; set; }
        public List<DetalleCompraCreacionDTO> DetallesCompra { get; set; }
    }

    public class DetalleCompraDTO
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; }
    }

    public class DetalleCompraCreacionDTO
    {
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int ProductoId { get; set; }
    }
}