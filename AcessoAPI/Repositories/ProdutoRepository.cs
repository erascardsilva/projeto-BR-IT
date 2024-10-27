//  Erasmo Cardoso

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AcessoAPI.Models;
using AcessoAPI.Data;

namespace AcessoAPI.Data
{
    public class ProdutoRepository
    {
        private readonly ControleSistemaContext _context;

        public ProdutoRepository(ControleSistemaContext context)
        {
            _context = context;
        }

        // Obter todos os produtos por stored procedure
        public async Task<List<Produto>> GetAllProdutosAsync()
        {
            return await _context.Produtos.FromSqlRaw("EXEC sp_ListarProdutos").ToListAsync();
        }

        // Obter um produto por ID por stored procedure
        public async Task<Produto?> GetProdutoByIdAsync(int id)
        {
            var produto = await _context.Produtos.FromSqlRaw("EXEC sp_BuscarProdutoPorId @ProdutoID = {0}", id).ToListAsync();
            return produto.FirstOrDefault(); //  produto ou null
        }

        // Adicionar um produto por stored procedure
        public async Task AddProdutoAsync(string nome, decimal preco, int fornecedorID)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_InserirProduto @Nome = {0}, @Preco = {1}, @FornecedorID = {2}",
                nome, preco, fornecedorID
            );
        }

        // Atualizar um produto 
        public async Task<bool> UpdateProdutoAsync(Produto produto)
        {
            // Validação  logica
            if (produto == null)
                throw new ArgumentNullException(nameof(produto), "Produto não pode ser nulo.");

            // Execução da procedure
            try
            {
                var resultado = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_AtualizarProduto @ProdutoID = {0}, @Nome = {1}, @Preco = {2}, @FornecedorID = {3}",
                    produto.ProdutoID, produto.Nome, produto.Preco, produto.FornecedorID
                );

                
                return resultado > 0; 
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar o produto.", ex);
            }
            catch (Exception ex)
            {
                // Erros erros erros
                throw new Exception("Ocorreu um erro inesperado ao atualizar o produto.", ex);
            }
        }


        // MDeletar um produto 
        public async Task<bool> DeleteProdutoAsync(int id)
        {
            var resultado = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_DeletarProduto @ProdutoID = {0}",
                id
            );

            // Falhou ?
            return resultado > 0; 
        }
    }
}
