//  Erasmo Cardoso


using AcessoAPI.Data;
using AcessoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcessoAPI.Repositories
{
    public class FornecedorRepository
    {
        private readonly ControleSistemaContext _context;

        public FornecedorRepository(ControleSistemaContext context)
        {
            _context = context;
        }

        // Inserir fornecedor
        public async Task InserirFornecedorAsync(Fornecedor fornecedor)
        {
            _context.Fornecedores.Add(fornecedor);
            await _context.SaveChangesAsync();
        }

        // Buscar todos os fornecedores
        public async Task<IEnumerable<Fornecedor>> BuscarFornecedoresAsync()
        {
            return await _context.Fornecedores.ToListAsync();
        }

        // Buscar um fornecedor por ID
        public async Task<Fornecedor?> BuscarFornecedorPorIDAsync(int fornecedorID)
        {
            return await _context.Fornecedores.FindAsync(fornecedorID);
        }

        // Atualizar um fornecedor existente
        public async Task AtualizarFornecedorAsync(Fornecedor fornecedor)
        {
            _context.Fornecedores.Update(fornecedor);
            await _context.SaveChangesAsync();
        }

        // Deletar um fornecedor
        public async Task DeletarFornecedorAsync(int fornecedorID)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(fornecedorID);
            if (fornecedor != null)
            {
                _context.Fornecedores.Remove(fornecedor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
