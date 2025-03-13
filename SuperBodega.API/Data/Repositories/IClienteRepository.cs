using SuperBodega.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperBodega.API.Data.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> GetClientesActivosAsync();
        Task<IEnumerable<Venta>> GetComprasByClienteAsync(int clienteId);
    }
}