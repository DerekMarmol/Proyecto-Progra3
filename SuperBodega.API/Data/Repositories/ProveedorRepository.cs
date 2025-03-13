using Microsoft.EntityFrameworkCore;
using SuperBodega.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperBodega.API.Data.Repositories
{
    public class ProveedorRepository : Repository<Proveedor>, IProveedorRepository
    {
        public ProveedorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Proveedor>> GetProveedoresActivosAsync()
        {
            return await _context.Proveedores
                .Where(p => p.Activo)
                .ToListAsync();
        }
    }
}