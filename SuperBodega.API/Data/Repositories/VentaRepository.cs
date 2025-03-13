using Microsoft.EntityFrameworkCore;
using SuperBodega.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperBodega.API.Data.Repositories
{
    public class VentaRepository : Repository<Venta>, IVentaRepository
    {
        public VentaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Venta>> GetVentasByFechaAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Ventas
                .Where(v => v.Fecha >= fechaInicio && v.Fecha <= fechaFin)
                .Include(v => v.DetallesVenta)
                .ThenInclude(dv => dv.Producto)
                .Include(v => v.Cliente)
                .ToListAsync();
        }

        public async Task<IEnumerable<Venta>> GetVentasByClienteAsync(int clienteId)
        {
            return await _context.Ventas
                .Where(v => v.ClienteId == clienteId)
                .Include(v => v.DetallesVenta)
                .ThenInclude(dv => dv.Producto)
                .ToListAsync();
        }

        public async Task<IEnumerable<Venta>> GetVentasByProductoAsync(int productoId)
        {
            return await _context.Ventas
                .Where(v => v.DetallesVenta.Any(dv => dv.ProductoId == productoId))
                .Include(v => v.DetallesVenta)
                .ThenInclude(dv => dv.Producto)
                .Include(v => v.Cliente)
                .ToListAsync();
        }

        public async Task<IEnumerable<Venta>> GetVentasByProveedorAsync(int proveedorId)
        {
            return await _context.Ventas
                .Where(v => v.DetallesVenta.Any(dv => dv.Producto.ProveedorId == proveedorId))
                .Include(v => v.DetallesVenta)
                .ThenInclude(dv => dv.Producto)
                .Include(v => v.Cliente)
                .ToListAsync();
        }

        public async Task<bool> CambiarEstadoAsync(int ventaId, EstadoVenta nuevoEstado)
        {
            var venta = await _context.Ventas.FindAsync(ventaId);
            if (venta == null)
                return false;

            venta.Estado = nuevoEstado;
            venta.FechaActualizacion = DateTime.Now;
            
            return await SaveChangesAsync();
        }
    }
}