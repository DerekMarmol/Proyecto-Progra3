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
    public class VentasController : ControllerBase
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IProductoRepository _productoRepository;

        public VentasController(IVentaRepository ventaRepository, IProductoRepository productoRepository)
        {
            _ventaRepository = ventaRepository;
            _productoRepository = productoRepository;
        }

        // GET: api/Ventas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> GetVentas()
        {
            var ventas = await _ventaRepository.GetAllAsync();
            var ventasDTO = ventas.Select(v => new VentaDTO
            {
                Id = v.Id,
                Fecha = v.Fecha,
                Total = v.Total,
                Estado = v.Estado,
                Observaciones = v.Observaciones,
                ClienteId = v.ClienteId,
                NombreCliente = v.Cliente?.Nombre + " " + v.Cliente?.Apellido,
                DetallesVenta = v.DetallesVenta?.Select(d => new DetalleVentaDTO
                {
                    Id = d.Id,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal,
                    ProductoId = d.ProductoId,
                    NombreProducto = d.Producto?.Nombre
                }).ToList()
            });

            return Ok(ventasDTO);
        }

        // GET: api/Ventas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VentaDTO>> GetVenta(int id)
        {
            var venta = await _ventaRepository.GetByIdAsync(id);

            if (venta == null)
            {
                return NotFound();
            }

            var ventaDTO = new VentaDTO
            {
                Id = venta.Id,
                Fecha = venta.Fecha,
                Total = venta.Total,
                Estado = venta.Estado,
                Observaciones = venta.Observaciones,
                ClienteId = venta.ClienteId,
                NombreCliente = venta.Cliente?.Nombre + " " + venta.Cliente?.Apellido,
                DetallesVenta = venta.DetallesVenta?.Select(d => new DetalleVentaDTO
                {
                    Id = d.Id,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal,
                    ProductoId = d.ProductoId,
                    NombreProducto = d.Producto?.Nombre
                }).ToList()
            };

            return ventaDTO;
        }

        // GET: api/Ventas/Cliente/5
        [HttpGet("Cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> GetVentasByCliente(int clienteId)
        {
            var ventas = await _ventaRepository.GetVentasByClienteAsync(clienteId);
            var ventasDTO = ventas.Select(v => new VentaDTO
            {
                Id = v.Id,
                Fecha = v.Fecha,
                Total = v.Total,
                Estado = v.Estado,
                Observaciones = v.Observaciones,
                ClienteId = v.ClienteId,
                NombreCliente = v.Cliente?.Nombre + " " + v.Cliente?.Apellido,
                DetallesVenta = v.DetallesVenta?.Select(d => new DetalleVentaDTO
                {
                    Id = d.Id,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal,
                    ProductoId = d.ProductoId,
                    NombreProducto = d.Producto?.Nombre
                }).ToList()
            });

            return Ok(ventasDTO);
        }

        // GET: api/Ventas/Producto/5
        [HttpGet("Producto/{productoId}")]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> GetVentasByProducto(int productoId)
        {
            var ventas = await _ventaRepository.GetVentasByProductoAsync(productoId);
            var ventasDTO = ventas.Select(v => new VentaDTO
            {
                Id = v.Id,
                Fecha = v.Fecha,
                Total = v.Total,
                Estado = v.Estado,
                Observaciones = v.Observaciones,
                ClienteId = v.ClienteId,
                NombreCliente = v.Cliente?.Nombre + " " + v.Cliente?.Apellido,
                DetallesVenta = v.DetallesVenta?.Select(d => new DetalleVentaDTO
                {
                    Id = d.Id,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal,
                    ProductoId = d.ProductoId,
                    NombreProducto = d.Producto?.Nombre
                }).ToList()
            });

            return Ok(ventasDTO);
        }

        // GET: api/Ventas/Proveedor/5
        [HttpGet("Proveedor/{proveedorId}")]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> GetVentasByProveedor(int proveedorId)
        {
            var ventas = await _ventaRepository.GetVentasByProveedorAsync(proveedorId);
            var ventasDTO = ventas.Select(v => new VentaDTO
            {
                Id = v.Id,
                Fecha = v.Fecha,
                Total = v.Total,
                Estado = v.Estado,
                Observaciones = v.Observaciones,
                ClienteId = v.ClienteId,
                NombreCliente = v.Cliente?.Nombre + " " + v.Cliente?.Apellido,
                DetallesVenta = v.DetallesVenta?.Select(d => new DetalleVentaDTO
                {
                    Id = d.Id,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal,
                    ProductoId = d.ProductoId,
                    NombreProducto = d.Producto?.Nombre
                }).ToList()
            });

            return Ok(ventasDTO);
        }

        // GET: api/Ventas/Fecha/2023-01-01/2023-12-31
        [HttpGet("Fecha/{fechaInicio}/{fechaFin}")]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> GetVentasByFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            var ventas = await _ventaRepository.GetVentasByFechaAsync(fechaInicio, fechaFin);
            var ventasDTO = ventas.Select(v => new VentaDTO
            {
                Id = v.Id,
                Fecha = v.Fecha,
                Total = v.Total,
                Estado = v.Estado,
                Observaciones = v.Observaciones,
                ClienteId = v.ClienteId,
                NombreCliente = v.Cliente?.Nombre + " " + v.Cliente?.Apellido,
                DetallesVenta = v.DetallesVenta?.Select(d => new DetalleVentaDTO
                {
                    Id = d.Id,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal,
                    ProductoId = d.ProductoId,
                    NombreProducto = d.Producto?.Nombre
                }).ToList()
            });

            return Ok(ventasDTO);
        }

        // POST: api/Ventas
        [HttpPost]
        public async Task<ActionResult<VentaDTO>> PostVenta(VentaCreacionDTO ventaDTO)
        {
            var venta = new Venta
            {
                Fecha = DateTime.Now,
                Estado = EstadoVenta.Pendiente,
                Observaciones = ventaDTO.Observaciones,
                ClienteId = ventaDTO.ClienteId,
                DetallesVenta = new List<DetalleVenta>()
            };

            decimal total = 0;

            foreach (var detalle in ventaDTO.DetallesVenta)
            {
                // Obtener el producto para verificar existencia y precio
                var producto = await _productoRepository.GetByIdAsync(detalle.ProductoId);
                if (producto == null)
                {
                    return BadRequest($"El producto con ID {detalle.ProductoId} no existe.");
                }

                if (producto.Existencia < detalle.Cantidad)
                {
                    return BadRequest($"No hay suficiente existencia del producto {producto.Nombre}. Disponible: {producto.Existencia}, Solicitado: {detalle.Cantidad}");
                }

                var precioUnitario = producto.Precio;
                var subtotal = detalle.Cantidad * precioUnitario;
                total += subtotal;

                var detalleVenta = new DetalleVenta
                {
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = precioUnitario,
                    Subtotal = subtotal,
                    ProductoId = detalle.ProductoId
                };

                venta.DetallesVenta.Add(detalleVenta);

                // Reducir existencia del producto
                await _productoRepository.ReducirExistenciaAsync(detalle.ProductoId, detalle.Cantidad);
            }

            venta.Total = total;

            await _ventaRepository.AddAsync(venta);
            await _ventaRepository.SaveChangesAsync();

            var nuevaVentaDTO = new VentaDTO
            {
                Id = venta.Id,
                Fecha = venta.Fecha,
                Total = venta.Total,
                Estado = venta.Estado,
                Observaciones = venta.Observaciones,
                ClienteId = venta.ClienteId
            };

            return CreatedAtAction(nameof(GetVenta), new { id = venta.Id }, nuevaVentaDTO);
        }

        // PUT: api/Ventas/CambiarEstado/5
        [HttpPut("CambiarEstado/{id}")]
        public async Task<IActionResult> CambiarEstadoVenta(int id, CambioEstadoVentaDTO cambioEstadoDTO)
        {
            var resultado = await _ventaRepository.CambiarEstadoAsync(id, cambioEstadoDTO.NuevoEstado);
            
            if (!resultado)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT: api/Ventas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenta(int id, VentaCreacionDTO ventaDTO)
        {
            var venta = await _ventaRepository.GetByIdAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            // Actualizar solo campos básicos, no se permite modificar detalles para mantener integridad
            venta.Observaciones = ventaDTO.Observaciones;
            venta.FechaActualizacion = DateTime.Now;

            _ventaRepository.Update(venta);
            await _ventaRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Ventas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenta(int id)
        {
            // No implementaremos eliminación de ventas para mantener la integridad de los datos
            // y el control de inventario
            return BadRequest("No se permite eliminar ventas para mantener la integridad del inventario.");
        }
    }
}