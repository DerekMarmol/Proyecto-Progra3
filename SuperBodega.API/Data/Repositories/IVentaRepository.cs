using SuperBodega.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperBodega.API.Data.Repositories
{
    public interface IVentaRepository : IRepository<Venta>
    {
        Task<IEnumerable<Venta>> GetVentasByFechaAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<IEnumerable<Venta>> GetVentasByClienteAsync(int clienteId);
        Task<IEnumerable<Venta>> GetVentasByProductoAsync(int productoId);
        Task<IEnumerable<Venta>> GetVentasByProveedorAsync(int proveedorId);
        Task<bool> CambiarEstadoAsync(int ventaId, EstadoVenta nuevoEstado);
    }
}