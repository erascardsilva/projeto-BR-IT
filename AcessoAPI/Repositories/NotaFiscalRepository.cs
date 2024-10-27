//  Erasmo Cardoso


using AcessoAPI.Data;
using AcessoAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcessoAPI.Repository
{
    public class NotaFiscalRepository
    {
        private readonly ControleSistemaContext _context;

        public NotaFiscalRepository(ControleSistemaContext context)
        {
            _context = context;
        }

        // Obter todas as notas fiscais
        public async Task<List<NotaFiscal>> GetAllNotasFiscaisAsync()
        {
            return await _context.NotaFiscal.FromSqlRaw("EXEC sp_BuscarNotasFiscais").ToListAsync();
        }

        // Obter uma nota fiscal por ID
        public async Task<NotaFiscal> GetNotaFiscalByIdAsync(int id)
        {
            var notasFiscais = await _context.NotaFiscal
                .FromSqlRaw("EXEC sp_BuscarNotaFiscalPorID @Id = {0}", id)
                .ToListAsync();
            return notasFiscais.FirstOrDefault(); // Retorna o primeiro ou null
        }

        // Adicionar uma nova nota fiscal ainda erro
        public async Task AddNotaFiscalAsync(NotaFiscal notaFiscal)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_InserirNotaFiscal @Numero, @Data, @Valor, @Descricao, @ClienteID",
                    new SqlParameter("@Numero", notaFiscal.Numero),
                    new SqlParameter("@Data", notaFiscal.Data),
                    new SqlParameter("@Valor", notaFiscal.Valor),
                    new SqlParameter("@Descricao", notaFiscal.Descricao),
                    new SqlParameter("@ClienteID", notaFiscal.ClienteID)
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir nota fiscal: " + ex.Message, ex);
            }
        }

        public async Task UpdateNotaFiscalAsync(NotaFiscal notaFiscal)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_AtualizarNotaFiscal @Id, @Numero, @Data, @Valor, @Descricao, @ClienteID",
                    new SqlParameter("@Id", notaFiscal.NotaFiscalID),
                    new SqlParameter("@Numero", notaFiscal.Numero),
                    new SqlParameter("@Data", notaFiscal.Data),
                    new SqlParameter("@Valor", notaFiscal.Valor),
                    new SqlParameter("@Descricao", notaFiscal.Descricao),
                    new SqlParameter("@ClienteID", notaFiscal.ClienteID)
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar nota fiscal: " + ex.Message, ex);
            }
        }

        public async Task DeleteNotaFiscalAsync(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeletarNotaFiscal @Id", new SqlParameter("@Id", id));
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar nota fiscal: " + ex.Message, ex);
            }
        }

        public async Task<bool> NotaFiscalExistsAsync(int id)
        {
            return await _context.NotaFiscal.AnyAsync(e => e.NotaFiscalID == id);
        }

        public async Task<bool> VerificaClienteExistenteAsync(int clienteId)
        {
            return await _context.Clientes.AnyAsync(c => c.ClienteID == clienteId);
        }

    }
}
