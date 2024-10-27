//Erasmo Cardoso

using AcessoAPI.Data;
using AcessoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcessoAPI.Controllers
{
    [Route("api/fornecedores")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly ControleSistemaContext _context;

        public FornecedorController(ControleSistemaContext context)
        {
            _context = context;
        }

        // GET: api/fornecedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> GetFornecedores()
        {
            var fornecedores = await _context.Fornecedores.FromSqlRaw("EXEC sp_BuscarFornecedores").ToListAsync();
            return Ok(fornecedores);
        }

        // GET: api/fornecedores/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Fornecedor>> GetFornecedor(int id)
        {
            var paramId = new SqlParameter("@FornecedorID", id);
            var fornecedor = await _context.Fornecedores
                .FromSqlRaw("EXEC sp_BuscarFornecedorPorID @FornecedorID", paramId)
                .FirstOrDefaultAsync();

            if (fornecedor == null)
            {
                return NotFound();
            }

            return Ok(fornecedor);
        }

        // POST: api/fornecedores
        [HttpPost]
        public async Task<ActionResult<Fornecedor>> PostFornecedor([FromBody] Fornecedor fornecedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parametros = new[]
            {
                new SqlParameter("@Nome", fornecedor.Nome),
                new SqlParameter("@Contato", fornecedor.Contato),
                new SqlParameter("@Telefone", fornecedor.Telefone)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_InserirFornecedor @Nome, @Contato, @Telefone", parametros);
            return CreatedAtAction(nameof(GetFornecedor), new { id = fornecedor.FornecedorID }, fornecedor);
        }

        // PUT: api/fornecedores/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> PutFornecedor(int id, [FromBody] Fornecedor fornecedor)
        {
            if (id != fornecedor.FornecedorID)
            {
                return BadRequest();
            }

            var parametros = new[]
            {
                new SqlParameter("@FornecedorID", fornecedor.FornecedorID),
                new SqlParameter("@Nome", fornecedor.Nome),
                new SqlParameter("@Contato", fornecedor.Contato),
                new SqlParameter("@Telefone", fornecedor.Telefone)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_AtualizarFornecedor @FornecedorID, @Nome, @Contato, @Telefone", parametros);
            return NoContent();
        }

        // DELETE: api/fornecedores/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFornecedor(int id)
        {
            var paramId = new SqlParameter("@FornecedorID", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeletarFornecedor @FornecedorID", paramId);
            return NoContent();
        }
    }
}
