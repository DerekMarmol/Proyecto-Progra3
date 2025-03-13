using Microsoft.EntityFrameworkCore;
using SuperBodega.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperBodega.API.Data.Repositories
{
    public class CompraRepository : Repository<Compra>, ICompraRepository
    {
        public CompraRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Compra>> GetComprasByFechaAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Compras
                .Where(c => c.Fecha >= fechaInicio && c.Fecha <= fechaFin)
                .Include(c => c.DetallesCompra)
                .ThenInclude(dc => dc.Producto)
                .Include(c => c.Proveedor)
                .ToListAsync();
        }

        public async Task<IEnumerable<Compra>> GetComprasByProveedorAsync(int proveedorId)
        {
            return await _context.Compras
                .Where(c => c.ProveedorId == proveedorId)
                .Include(c => c.DetallesCompra)
                .ThenInclude(dc => dc.Producto)
                .ToListAsync();
        }
    }
}