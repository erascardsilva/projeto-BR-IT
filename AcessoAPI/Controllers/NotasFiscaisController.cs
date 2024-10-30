// Erasmo Cardoso 

using Microsoft.AspNetCore.Mvc;
using AcessoAPI.Models;
using AcessoAPI.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcessoAPI.Controllers
{
    [Route("api/notafiscal")] 
    [ApiController]
    public class NotasFiscaisController : ControllerBase
    {
        private readonly NotaFiscalRepository _notaFiscalRepository;

        public NotasFiscaisController(NotaFiscalRepository notaFiscalRepository)
        {
            _notaFiscalRepository = notaFiscalRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotaFiscal>>> GetNotasFiscais()
        {
            var notasFiscais = await _notaFiscalRepository.GetAllNotasFiscaisAsync();
            if (notasFiscais == null || !notasFiscais.Any())
            {
                return NotFound("Nenhuma nota fiscal encontrada.");
            }
            return Ok(notasFiscais);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NotaFiscal>> GetNotaFiscal(int id)
        {
            var notaFiscal = await _notaFiscalRepository.GetNotaFiscalByIdAsync(id);
            if (notaFiscal == null)
            {
                return NotFound($"Nota fiscal com ID {id} não encontrada.");
            }
            return Ok(notaFiscal);
        }

        [HttpPost]
        public async Task<ActionResult<NotaFiscal>> PostNotaFiscal([FromBody] NotaFiscal notaFiscal)
        {
            if (notaFiscal == null)
            {
                return BadRequest("Nota fiscal não pode ser nula.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var clienteExistente = await _notaFiscalRepository.VerificaClienteExistenteAsync(notaFiscal.ClienteID);
            if (!clienteExistente)
            {
                return BadRequest($"ClienteID {notaFiscal.ClienteID} não existe.");
            }

            await _notaFiscalRepository.AddNotaFiscalAsync(notaFiscal);

            return  CreatedAtAction(nameof(GetNotaFiscal), new { id = notaFiscal }, notaFiscal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotaFiscal(int id, [FromBody] NotaFiscal notaFiscalAtualizada)
        {
            if (notaFiscalAtualizada == null)
            {
                return BadRequest("Dados da nota fiscal não podem ser nulos.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            
            if (!await _notaFiscalRepository.NotaFiscalExistsAsync(id))
            {
                return NotFound($"Nota fiscal com ID {id} não encontrada.");
            }

            await _notaFiscalRepository.UpdateNotaFiscalAsync(notaFiscalAtualizada);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotaFiscal(int id)
        {
            if (!await _notaFiscalRepository.NotaFiscalExistsAsync(id))
            {
                return NotFound($"Nota fiscal com ID {id} não encontrada.");
            }

            await _notaFiscalRepository.DeleteNotaFiscalAsync(id);
            return NoContent();
        }
    }
}
