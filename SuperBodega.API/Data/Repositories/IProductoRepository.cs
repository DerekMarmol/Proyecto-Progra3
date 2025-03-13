// Data/Repositories/IProductoRepository.cs
using SuperBodega.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperBodega.API.Data.Repositories
{
    public interface IProductoRepository : IRepository<Producto>
    {
        Task<IEnumerable<Producto>> GetProductosByProveedorAsync(int proveedorId);
        Task<bool> ReducirExistenciaAsync(int productoId, int cantidad);
        Task<bool> AumentarExistenciaAsync(int productoId, int cantidad);
        Task<Producto> GetByIdWithProveedorAsync(int id); // Nuevo método
        Task<IEnumerable<Producto>> GetAllWithProveedorAsync(); // Nuevo método
    }
}