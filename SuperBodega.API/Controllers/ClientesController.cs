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
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientes()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            var clientesDTO = clientes.Select(c => new ClienteDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Direccion = c.Direccion,
                Telefono = c.Telefono,
                Email = c.Email,
                NIT = c.NIT,
                Activo = c.Activo
            });

            return Ok(clientesDTO);
        }

        // GET: api/Clientes/Activos
        [HttpGet("Activos")]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientesActivos()
        {
            var clientes = await _clienteRepository.GetClientesActivosAsync();
            var clientesDTO = clientes.Select(c => new ClienteDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Direccion = c.Direccion,
                Telefono = c.Telefono,
                Email = c.Email,
                NIT = c.NIT,
                Activo = c.Activo
            });

            return Ok(clientesDTO);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetCliente(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            var clienteDTO = new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Direccion = cliente.Direccion,
                Telefono = cliente.Telefono,
                Email = cliente.Email,
                NIT = cliente.NIT,
                Activo = cliente.Activo
            };

            return clienteDTO;
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> PostCliente(ClienteCreacionDTO clienteDTO)
        {
            var cliente = new Cliente
            {
                Nombre = clienteDTO.Nombre,
                Apellido = clienteDTO.Apellido,
                Direccion = clienteDTO.Direccion,
                Telefono = clienteDTO.Telefono,
                Email = clienteDTO.Email,
                NIT = clienteDTO.NIT,
                Activo = true
            };

            await _clienteRepository.AddAsync(cliente);
            await _clienteRepository.SaveChangesAsync();

            var nuevoClienteDTO = new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Direccion = cliente.Direccion,
                Telefono = cliente.Telefono,
                Email = cliente.Email,
                NIT = cliente.NIT,
                Activo = cliente.Activo
            };

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, nuevoClienteDTO);
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteCreacionDTO clienteDTO)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            cliente.Nombre = clienteDTO.Nombre;
            cliente.Apellido = clienteDTO.Apellido;
            cliente.Direccion = clienteDTO.Direccion;
            cliente.Telefono = clienteDTO.Telefono;
            cliente.Email = clienteDTO.Email;
            cliente.NIT = clienteDTO.NIT;
            cliente.FechaActualizacion = System.DateTime.Now;

            _clienteRepository.Update(cliente);
            await _clienteRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            // En lugar de eliminar, marcamos como inactivo
            cliente.Activo = false;
            cliente.FechaActualizacion = System.DateTime.Now;
            
            _clienteRepository.Update(cliente);
            await _clienteRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}