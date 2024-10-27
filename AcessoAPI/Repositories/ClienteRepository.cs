//  Erasmo Cardoso


using AcessoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcessoAPI.Data;

namespace AcessoAPI.Repositories
{
    public class ClienteRepository
    {
        private readonly ControleSistemaContext _context;

        public ClienteRepository(ControleSistemaContext context)
        {
            _context = context;
        }

        // Buscar todos os clientes
        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            return await _context.Clientes.FromSqlRaw("EXEC sp_BuscarClientes").ToListAsync();
        }

       
        // Buscar um cliente por ID
public async Task<Cliente?> GetClienteByIdAsync(int id)
{
    var paramId = new SqlParameter("@ClienteID", id);

    // consulta  assíncrona.
    var cliente = await Task.Run(() => 
        _context.Clientes
            .FromSqlRaw("EXEC sp_BuscarClientePorID @ClienteID", paramId)
            .AsEnumerable() 
            .FirstOrDefault()
    );

    // Cliente existe ???
    if (cliente == null)
    {
        return null; 
    }
    
    return cliente;
}


        // Inserir  novo cliente
        public async Task InsertClienteAsync(Cliente cliente)
        {
            var parametros = new[]
            {
                new SqlParameter("@Nome", cliente.Nome),
                new SqlParameter("@Email", cliente.Email),
                new SqlParameter("@Telefone", cliente.Telefone)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_InserirCliente @Nome, @Email, @Telefone", parametros);
        }

        // Atualizar um cliente
        public async Task UpdateClienteAsync(Cliente cliente)
        {
            var parametros = new[]
            {
                new SqlParameter("@ClienteID", cliente.ClienteID),
                new SqlParameter("@Nome", cliente.Nome),
                new SqlParameter("@Email", cliente.Email),
                new SqlParameter("@Telefone", cliente.Telefone)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_AtualizarCliente @ClienteID, @Nome, @Email, @Telefone", parametros);
        }

        // Deletar um cliente
        public async Task DeleteClienteAsync(int id)
        {
            var paramId = new SqlParameter("@ClienteID", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeletarCliente @ClienteID", paramId);
        }
    }
}
