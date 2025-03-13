using Microsoft.AspNetCore.Mvc;
using SuperBodega.API.Data.Repositories;
using SuperBodega.API.DTOs;
using SuperBodega.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperBodega.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedorRepository _proveedorRepository;

        public ProveedoresController(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        // GET: api/Proveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorDTO>>> GetProveedores()
        {
            var proveedores = await _proveedorRepository.GetAllAsync();
            var proveedoresDTO = proveedores.Select(p => new ProveedorDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Direccion = p.Direccion,
                Telefono = p.Telefono,
                Email = p.Email,
                NIT = p.NIT,
                Activo = p.Activo
            });

            return Ok(proveedoresDTO);
        }

        // GET: api/Proveedores/Activos
        [HttpGet("Activos")]
        public async Task<ActionResult<IEnumerable<ProveedorDTO>>> GetProveedoresActivos()
        {
            var proveedores = await _proveedorRepository.GetProveedoresActivosAsync();
            var proveedoresDTO = proveedores.Select(p => new ProveedorDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Direccion = p.Direccion,
                Telefono = p.Telefono,
                Email = p.Email,
                NIT = p.NIT,
                Activo = p.Activo
            });

            return Ok(proveedoresDTO);
        }

        // GET: api/Proveedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProveedorDTO>> GetProveedor(int id)
        {
            var proveedor = await _proveedorRepository.GetByIdAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            var proveedorDTO = new ProveedorDTO
            {
                Id = proveedor.Id,
                Nombre = proveedor.Nombre,
                Direccion = proveedor.Direccion,
                Telefono = proveedor.Telefono,
                Email = proveedor.Email,
                NIT = proveedor.NIT,
                Activo = proveedor.Activo
            };

            return proveedorDTO;
        }

        // POST: api/Proveedores
        [HttpPost]
        public async Task<ActionResult<ProveedorDTO>> PostProveedor(ProveedorCreacionDTO proveedorDTO)
        {
            var proveedor = new Proveedor
            {
                Nombre = proveedorDTO.Nombre,
                Direccion = proveedorDTO.Direccion,
                Telefono = proveedorDTO.Telefono,
                Email = proveedorDTO.Email,
                NIT = proveedorDTO.NIT,
                Activo = true
            };

            await _proveedorRepository.AddAsync(proveedor);
            await _proveedorRepository.SaveChangesAsync();

            var nuevoProveedorDTO = new ProveedorDTO
            {
                Id = proveedor.Id,
                Nombre = proveedor.Nombre,
                Direccion = proveedor.Direccion,
                Telefono = proveedor.Telefono,
                Email = proveedor.Email,
                NIT = proveedor.NIT,
                Activo = proveedor.Activo
            };

            return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.Id }, nuevoProveedorDTO);
        }

        // PUT: api/Proveedores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, ProveedorCreacionDTO proveedorDTO)
        {
            var proveedor = await _proveedorRepository.GetByIdAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            proveedor.Nombre = proveedorDTO.Nombre;
            proveedor.Direccion = proveedorDTO.Direccion;
            proveedor.Telefono = proveedorDTO.Telefono;
            proveedor.Email = proveedorDTO.Email;
            proveedor.NIT = proveedorDTO.NIT;
            proveedor.FechaActualizacion = System.DateTime.Now;

            _proveedorRepository.Update(proveedor);
            await _proveedorRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Proveedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var proveedor = await _proveedorRepository.GetByIdAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            // En lugar de eliminar, marcamos como inactivo
            proveedor.Activo = false;
            proveedor.FechaActualizacion = System.DateTime.Now;
            
            _proveedorRepository.Update(proveedor);
            await _proveedorRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}