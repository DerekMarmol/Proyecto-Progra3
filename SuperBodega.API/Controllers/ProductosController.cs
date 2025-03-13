using Microsoft.AspNetCore.Mvc;
using SuperBodega.API.DTOs;
using SuperBodega.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperBodega.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        // GET: api/Productos
        /// <summary>
        /// Obtiene todos los productos
        /// </summary>
        /// <returns>Lista de productos</returns>
        [HttpGet]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProductos()
        {
            var productos = await _productoService.GetAllAsync();
            return Ok(productos);
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> GetProducto(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // GET: api/Productos/Proveedor/5
        [HttpGet("Proveedor/{proveedorId}")]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProductosByProveedor(int proveedorId)
        {
            var productos = await _productoService.GetByProveedorAsync(proveedorId);
            return Ok(productos);
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> PostProducto(ProductoCreacionDTO productoDTO)
        {
            var nuevoProducto = await _productoService.CreateAsync(productoDTO);
            return CreatedAtAction(nameof(GetProducto), new { id = nuevoProducto.Id }, nuevoProducto);
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, ProductoCreacionDTO productoDTO)
        {
            var result = await _productoService.UpdateAsync(id, productoDTO);
            
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var result = await _productoService.DeleteAsync(id);
            
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}