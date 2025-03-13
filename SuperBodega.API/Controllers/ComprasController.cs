using Microsoft.AspNetCore.Mvc;
using SuperBodega.API.Data.Repositories;
using SuperBodega.API.DTOs;
using SuperBodega.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperBodega.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly ICompraRepository _compraRepository;
        private readonly IProductoRepository _productoRepository;

        public ComprasController(ICompraRepository compraRepository, IProductoRepository productoRepository)
        {
            _compraRepository = compraRepository;
            _productoRepository = productoRepository;
        }

        // GET: api/Compras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompraDTO>>> GetCompras()
        {
            var compras = await _compraRepository.GetAllAsync();
            var comprasDTO = compras.Select(c => new CompraDTO
            {
                Id = c.Id,
                Fecha = c.Fecha,
                Total = c.Total,
                Observaciones = c.Observaciones,
                ProveedorId = c.ProveedorId,
                NombreProveedor = c.Proveedor?.Nombre,
                DetallesCompra = c.DetallesCompra?.Select(d => new DetalleCompraDTO
                {
                    Id = d.Id,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal,
                    ProductoId = d.ProductoId,
                    NombreProducto = d.Producto?.Nombre
                }).ToList()
            });

            return Ok(comprasDTO);
        }

        // GET: api/Compras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompraDTO>> GetCompra(int id)
        {
            var compra = await _compraRepository.GetByIdAsync(id);

            if (compra == null)
            {
                return NotFound();
            }

            var compraDTO = new CompraDTO
            {
                Id = compra.Id,
                Fecha = compra.Fecha,
                Total = compra.Total,
                Observaciones = compra.Observaciones,
                ProveedorId = compra.ProveedorId,
                NombreProveedor = compra.Proveedor?.Nombre,
                DetallesCompra = compra.DetallesCompra?.Select(d => new DetalleCompraDTO
                {
                    Id = d.Id,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal,
                    ProductoId = d.ProductoId,
                    NombreProducto = d.Producto?.Nombre
                }).ToList()
            };

            return compraDTO;
        }

        // GET: api/Compras/Proveedor/5
        [HttpGet("Proveedor/{proveedorId}")]
        public async Task<ActionResult<IEnumerable<CompraDTO>>> GetComprasByProveedor(int proveedorId)
        {
            var compras = await _compraRepository.GetComprasByProveedorAsync(proveedorId);
            var comprasDTO = compras.Select(c => new CompraDTO
            {
                Id = c.Id,
                Fecha = c.Fecha,
                Total = c.Total,
                Observaciones = c.Observaciones,
                ProveedorId = c.ProveedorId,
                NombreProveedor = c.Proveedor?.Nombre,
                DetallesCompra = c.DetallesCompra?.Select(d => new DetalleCompraDTO
                {
                    Id = d.Id,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal,
                    ProductoId = d.ProductoId,
                    NombreProducto = d.Producto?.Nombre
                }).ToList()
            });

            return Ok(comprasDTO);
        }

        // GET: api/Compras/Fecha/2023-01-01/2023-12-31
        [HttpGet("Fecha/{fechaInicio}/{fechaFin}")]
        public async Task<ActionResult<IEnumerable<CompraDTO>>> GetComprasByFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            var compras = await _compraRepository.GetComprasByFechaAsync(fechaInicio, fechaFin);
            var comprasDTO = compras.Select(c => new CompraDTO
            {
                Id = c.Id,
                Fecha = c.Fecha,
                Total = c.Total,
                Observaciones = c.Observaciones,
                ProveedorId = c.ProveedorId,
                NombreProveedor = c.Proveedor?.Nombre,
                DetallesCompra = c.DetallesCompra?.Select(d => new DetalleCompraDTO
                {
                    Id = d.Id,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal,
                    ProductoId = d.ProductoId,
                    NombreProducto = d.Producto?.Nombre
                }).ToList()
            });

            return Ok(comprasDTO);
        }

        // POST: api/Compras
        [HttpPost]
        public async Task<ActionResult<CompraDTO>> PostCompra(CompraCreacionDTO compraDTO)
        {
            var compra = new Compra
            {
                Fecha = DateTime.Now,
                Observaciones = compraDTO.Observaciones,
                ProveedorId = compraDTO.ProveedorId,
                DetallesCompra = new List<DetalleCompra>()
            };

            decimal total = 0;

            foreach (var detalle in compraDTO.DetallesCompra)
            {
                var subtotal = detalle.Cantidad * detalle.PrecioUnitario;
                total += subtotal;

                var detalleCompra = new DetalleCompra
                {
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.PrecioUnitario,
                    Subtotal = subtotal,
                    ProductoId = detalle.ProductoId
                };

                compra.DetallesCompra.Add(detalleCompra);

                // Aumentar existencia del producto
                await _productoRepository.AumentarExistenciaAsync(detalle.ProductoId, detalle.Cantidad);
            }

            compra.Total = total;

            await _compraRepository.AddAsync(compra);
            await _compraRepository.SaveChangesAsync();

            var nuevaCompraDTO = new CompraDTO
            {
                Id = compra.Id,
                Fecha = compra.Fecha,
                Total = compra.Total,
                Observaciones = compra.Observaciones,
                ProveedorId = compra.ProveedorId
            };

            return CreatedAtAction(nameof(GetCompra), new { id = compra.Id }, nuevaCompraDTO);
        }

        // PUT: api/Compras/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompra(int id, CompraCreacionDTO compraDTO)
        {
            var compra = await _compraRepository.GetByIdAsync(id);
            if (compra == null)
            {
                return NotFound();
            }

            // Actualizar solo campos básicos, no se permite modificar detalles para mantener integridad
            compra.Observaciones = compraDTO.Observaciones;
            compra.FechaActualizacion = DateTime.Now;

            _compraRepository.Update(compra);
            await _compraRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Compras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra(int id)
        {
            // No implementaremos eliminación de compras para mantener la integridad de los datos
            // y el control de inventario
            return BadRequest("No se permite eliminar compras para mantener la integridad del inventario.");
        }
    }
}