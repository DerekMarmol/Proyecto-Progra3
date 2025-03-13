using SuperBodega.API.Data.Repositories;
using SuperBodega.API.DTOs;
using SuperBodega.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperBodega.API.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<IEnumerable<ProductoDTO>> GetAllAsync()
        {
            var productos = await _productoRepository.GetAllWithProveedorAsync();
            return productos.Select(MapToDTO);
        }


        public async Task<ProductoDTO> GetByIdAsync(int id)
        {
            var producto = await _productoRepository.GetByIdWithProveedorAsync(id);
            if (producto == null)
                return null;

            return MapToDTO(producto);
        }

        public async Task<IEnumerable<ProductoDTO>> GetByProveedorAsync(int proveedorId)
        {
            var productos = await _productoRepository.GetProductosByProveedorAsync(proveedorId);
            return productos.Select(MapToDTO);
        }

        public async Task<ProductoDTO> CreateAsync(ProductoCreacionDTO productoDTO)
        {
            var producto = new Producto
            {
                Nombre = productoDTO.Nombre,
                Descripcion = productoDTO.Descripcion,
                Precio = productoDTO.Precio,
                Existencia = productoDTO.Existencia,
                Categoria = productoDTO.Categoria,
                ImagenUrl = productoDTO.ImagenUrl,
                ProveedorId = productoDTO.ProveedorId,
                FechaCreacion = DateTime.Now,
                Activo = true
            };

            await _productoRepository.AddAsync(producto);
            await _productoRepository.SaveChangesAsync();

            return MapToDTO(producto);
        }

        public async Task<bool> UpdateAsync(int id, ProductoCreacionDTO productoDTO)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto == null)
                return false;

            producto.Nombre = productoDTO.Nombre;
            producto.Descripcion = productoDTO.Descripcion;
            producto.Precio = productoDTO.Precio;
            producto.Existencia = productoDTO.Existencia;
            producto.Categoria = productoDTO.Categoria;
            producto.ImagenUrl = productoDTO.ImagenUrl;
            producto.ProveedorId = productoDTO.ProveedorId;
            producto.FechaActualizacion = DateTime.Now;

            _productoRepository.Update(producto);
            return await _productoRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto == null)
                return false;

            // En lugar de eliminar, marcar como inactivo
            producto.Activo = false;
            producto.FechaActualizacion = DateTime.Now;
            
            _productoRepository.Update(producto);
            return await _productoRepository.SaveChangesAsync();
        }

        public async Task<bool> ReducirExistenciaAsync(int productoId, int cantidad)
        {
            return await _productoRepository.ReducirExistenciaAsync(productoId, cantidad);
        }

        public async Task<bool> AumentarExistenciaAsync(int productoId, int cantidad)
        {
            return await _productoRepository.AumentarExistenciaAsync(productoId, cantidad);
        }

        private ProductoDTO MapToDTO(Producto producto)
        {
            return new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Existencia = producto.Existencia,
                Categoria = producto.Categoria,
                ImagenUrl = producto.ImagenUrl,
                ProveedorId = producto.ProveedorId,
                NombreProveedor = producto.Proveedor?.Nombre  // El operador ?. previene NullReferenceException
            };
        }

    }
}