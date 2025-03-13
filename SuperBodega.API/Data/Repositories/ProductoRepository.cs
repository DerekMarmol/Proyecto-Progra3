using Microsoft.EntityFrameworkCore;
using SuperBodega.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperBodega.API.Data.Repositories
{
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        public ProductoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Producto>> GetProductosByProveedorAsync(int proveedorId)
        {
            return await _context.Productos
                .Where(p => p.ProveedorId == proveedorId)
                .ToListAsync();
        }

        public async Task<bool> ReducirExistenciaAsync(int productoId, int cantidad)
        {
            var producto = await _context.Productos.FindAsync(productoId);
            if (producto == null || producto.Existencia < cantidad)
                return false;

            producto.Existencia -= cantidad;
            return await SaveChangesAsync();
        }

        public async Task<bool> AumentarExistenciaAsync(int productoId, int cantidad)
        {
            var producto = await _context.Productos.FindAsync(productoId);
            if (producto == null)
                return false;

            producto.Existencia += cantidad;
            return await SaveChangesAsync();
        }

         public async Task<Producto> GetByIdWithProveedorAsync(int id)
        {
            return await _context.Productos
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Producto>> GetAllWithProveedorAsync()
        {
            return await _context.Productos
                .Include(p => p.Proveedor)
                .ToListAsync();
        }
    }
}