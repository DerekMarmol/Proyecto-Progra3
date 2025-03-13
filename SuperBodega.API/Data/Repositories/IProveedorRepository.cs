using SuperBodega.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperBodega.API.Data.Repositories
{
    public interface IProveedorRepository : IRepository<Proveedor>
    {
        Task<IEnumerable<Proveedor>> GetProveedoresActivosAsync();
    }
}