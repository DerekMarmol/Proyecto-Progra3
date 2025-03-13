using SuperBodega.API.Models;
using System;
using System.Collections.Generic;

namespace SuperBodega.API.DTOs
{
    public class VentaDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public EstadoVenta Estado { get; set; }
        public string Observaciones { get; set; }
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; }
        public List<DetalleVentaDTO> DetallesVenta { get; set; }
    }

    public class VentaCreacionDTO
    {
        public string Observaciones { get; set; }
        public int ClienteId { get; set; }
        public List<DetalleVentaCreacionDTO> DetallesVenta { get; set; }
    }

    public class DetalleVentaDTO
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; }
    }

    public class DetalleVentaCreacionDTO
    {
        public int Cantidad { get; set; }
        public int ProductoId { get; set; }
    }

    public class CambioEstadoVentaDTO
    {
        public EstadoVenta NuevoEstado { get; set; }
    }
}