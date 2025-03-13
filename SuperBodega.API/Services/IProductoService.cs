using SuperBodega.API.DTOs;
using SuperBodega.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperBodega.API.Services
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDTO>> GetAllAsync();
        Task<ProductoDTO> GetByIdAsync(int id);
        Task<IEnumerable<ProductoDTO>> GetByProveedorAsync(int proveedorId);
        Task<ProductoDTO> CreateAsync(ProductoCreacionDTO productoDTO);
        Task<bool> UpdateAsync(int id, ProductoCreacionDTO productoDTO);
        Task<bool> DeleteAsync(int id);
        Task<bool> ReducirExistenciaAsync(int productoId, int cantidad);
        Task<bool> AumentarExistenciaAsync(int productoId, int cantidad);
    }
}