// Erasmo Cardoso 

using AcessoAPI.Data;
using AcessoAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
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

        // Obter  notas fiscais
        public async Task<IEnumerable<NotaFiscal>> GetAllNotasFiscaisAsync()
        {
            return await _context.NotaFiscal.FromSqlRaw("EXEC sp_BuscarNotasFiscais").ToListAsync();
        }

        // Obter uma nota fiscal por ID  ??? ainda refazer
        public async Task<NotaFiscal> GetNotaFiscalByIdAsync(int id)
        {
            var notasFiscais = await _context.NotaFiscal
                .FromSqlRaw("EXEC sp_BuscarNotaFiscalPorID @Id = {0}", id)
                .ToListAsync();
            return notasFiscais.FirstOrDefault();
        }

        // Adicionar uma nova nota fiscal
        public async Task<int> AddNotaFiscalAsync(NotaFiscal notaFiscal)
        {
            if (notaFiscal.DataEmissao == default(DateTime))
            {
                notaFiscal.DataEmissao = DateTime.Now;
            }

            // Gerar NotaFiscalID
            var random = new Random();
            int randomId = random.Next(1000, 9999);

            var parametros = new[]
            {
        new SqlParameter("@Numero", notaFiscal.Numero),
        new SqlParameter("@DataEmissao", notaFiscal.DataEmissao),
        new SqlParameter("@Valor", notaFiscal.Valor),
        new SqlParameter("@Descricao", notaFiscal.Descricao),
        new SqlParameter("@ClienteID", notaFiscal.ClienteID),
        new SqlParameter("@NotaFiscalID", randomId) 
    };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_InserirNotaFiscal @Numero, @DataEmissao, @Valor, @Descricao, @ClienteID, @NotaFiscalID",
                parametros);

            
            return randomId;
        }


       public async Task UpdateNotaFiscalAsync(NotaFiscal notaFiscal)
{
    try
    {
        
        if (notaFiscal.NotaFiscalID <= 0)
        {
            throw new ArgumentException("NotaFiscalID deve ser um valor positivo.", nameof(notaFiscal.NotaFiscalID));
        }

        // Verificar se a DataEmissao e ajusta
        if (notaFiscal.DataEmissao == default(DateTime))
        {
            notaFiscal.DataEmissao = DateTime.Now;
        }

        // Preparar parâmetros, utilizando DBNull para nulos 
        var parametros = new[]
        {
            new SqlParameter("@NotaFiscalID", notaFiscal.NotaFiscalID),
            new SqlParameter("@Numero", (object)notaFiscal.Numero ?? DBNull.Value),
            new SqlParameter("@DataEmissao", notaFiscal.DataEmissao), 
            new SqlParameter("@Valor", notaFiscal.Valor), 
            new SqlParameter("@Descricao", (object)notaFiscal.Descricao ?? DBNull.Value),
            new SqlParameter("@ClienteID", notaFiscal.ClienteID), 
          
        };
        //gravar
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_AtualizarNotaFiscal @NotaFiscalID, @Numero, @DataEmissao, @Valor, @Descricao, @ClienteID",
            parametros);
    }
    catch (SqlException sqlEx)
    {
        throw new Exception("Erro ao atualizar nota fiscal no banco de dados: " + sqlEx.Message, sqlEx);
    }
    catch (Exception ex)
    {
        // Tratamento 
        throw new Exception("Erro ao atualizar nota fiscal: " + ex.Message, ex);
    }
}


        // deleta 
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
            return await _context.NotaFiscal.AnyAsync();
        }

        public async Task<bool> VerificaClienteExistenteAsync(int clienteId)
        {
            return await _context.Clientes.AnyAsync(c => c.ClienteID == clienteId);
        }
    }
}
