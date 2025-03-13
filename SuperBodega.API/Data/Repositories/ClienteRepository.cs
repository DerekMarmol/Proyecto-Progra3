using Microsoft.EntityFrameworkCore;
using SuperBodega.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperBodega.API.Data.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Cliente>> GetClientesActivosAsync()
        {
            return await _context.Clientes
                .Where(c => c.Activo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Venta>> GetComprasByClienteAsync(int clienteId)
        {
            return await _context.Ventas
                .Where(v => v.ClienteId == clienteId)
                .Include(v => v.DetallesVenta)
                .ThenInclude(dv => dv.Producto)
                .ToListAsync();
        }
    }
}