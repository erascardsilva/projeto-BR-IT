//Erasmo Cardoso
using Microsoft.AspNetCore.Mvc;
using AcessoAPI.Models;
using AcessoAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AcessoAPI.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteRepository _clienteRepository;

        public ClientesController(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _clienteRepository.GetClientesAsync();
            if (clientes == null || !clientes.Any())
            {
                return NotFound("Nenhum cliente encontrado.");
            }
            return Ok(clientes);
        }

        // GET: api/clientes/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return NotFound($"Cliente com ID {id} não encontrado.");
            }
            return Ok(cliente);
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente([FromBody] Cliente cliente)
        {
            // Validação ?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _clienteRepository.InsertClienteAsync(cliente);
                return CreatedAtAction(nameof(GetCliente), new { id = cliente.ClienteID }, cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao adicionar cliente: " + ex.Message);
            }
        }

        // PUT: api/clientes/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, [FromBody] Cliente clienteAtualizado)
        {
            // Validação ??
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var clienteExistente = await _clienteRepository.GetClienteByIdAsync(id);
                if (clienteExistente == null)
                {
                    return NotFound($"Cliente com ID {id} não encontrado.");
                }

                clienteAtualizado.ClienteID = id; 
                await _clienteRepository.UpdateClienteAsync(clienteAtualizado);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _clienteRepository.GetClienteByIdAsync(id) == null)
                {
                    return NotFound($"Cliente com ID {id} não encontrado.");
                }
                return StatusCode(500, "Erro ao atualizar cliente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar cliente: " + ex.Message);
            }
        }

        // DELETE: api/clientes/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var clienteExistente = await _clienteRepository.GetClienteByIdAsync(id);
            if (clienteExistente == null)
            {
                return NotFound($"Cliente com ID {id} não encontrado.");
            }

            try
            {
                await _clienteRepository.DeleteClienteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao deletar cliente: " + ex.Message);
            }
        }
    }
}
