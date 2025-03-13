using SuperBodega.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperBodega.API.Data.Repositories
{
    public interface ICompraRepository : IRepository<Compra>
    {
        Task<IEnumerable<Compra>> GetComprasByFechaAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<IEnumerable<Compra>> GetComprasByProveedorAsync(int proveedorId);
    }
}