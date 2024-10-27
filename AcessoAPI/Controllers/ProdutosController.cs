//Erasmo Cardoso
using Microsoft.AspNetCore.Mvc;
using AcessoAPI.Models;
using AcessoAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace AcessoAPI.Controllers
{
    [Route("api/produtos")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutosController(ProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        // GET: api/produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            var produtos = await _produtoRepository.GetAllProdutosAsync();
            if (produtos == null || !produtos.Any())
            {
                return NotFound("Nenhum produto encontrado.");
            }
            return Ok(produtos);
        }

        // GET: api/produtos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _produtoRepository.GetProdutoByIdAsync(id);
            if (produto == null)
            {
                return NotFound($"Produto com ID {id} não encontrado.");
            }
            return Ok(produto);
        }

        // POST: api/produtos
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto([FromBody] Produto produto)
        {
            if (produto == null)
            {
                return BadRequest("Produto não pode ser nulo.");
            }

            await _produtoRepository.AddProdutoAsync(produto.Nome, produto.Preco, produto.FornecedorID);

            return CreatedAtAction(nameof(GetProduto), new { id = produto.ProdutoID }, produto);
        }

        // PUT: api/produtos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, [FromBody] Produto produtoAtualizado)
        {
            if (produtoAtualizado == null)
            {
                return BadRequest("Dados do produto não podem ser nulos.");
            }

            // Certifique-se de que o ID corresponde ao produto a ser atualizado
            produtoAtualizado.ProdutoID = id;

            // Bem-sucedida  ?
            var sucesso = await _produtoRepository.UpdateProdutoAsync(produtoAtualizado);
            if (!sucesso)
            {
                return NotFound($"Produto com ID {id} não encontrado.");
            }

            return NoContent();
        }

        // DELETE: api/produtos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produtoExistente = await _produtoRepository.GetProdutoByIdAsync(id);
            if (produtoExistente == null)
            {
                return NotFound($"Produto com ID {id} não encontrado.");
            }

            await _produtoRepository.DeleteProdutoAsync(id);
            return NoContent();
        }
    }
}
