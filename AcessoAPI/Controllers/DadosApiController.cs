//Erasmo Cardoso

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcessoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DadosApiController : ControllerBase
    {
        private readonly string _connectionString;

        public DadosApiController(IConfiguration configuration)
        {
            // start _connectionString
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada.");
        }

        // Buscar dados Posts com stored procedure sp_GetAllPosts
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dados = new List<DadosApi>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("sp_GetAllPosts", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        var reader = await command.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            var dado = new DadosApi
                            {
                                UserId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                Id = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                                Title = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Body = reader.IsDBNull(3) ? null : reader.GetString(3)
                            };
                            dados.Add(dado);
                        }
                    }
                }

                return Ok(dados);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, "Erro ao acessar o banco de dados: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro inesperado: " + ex.Message);
            }
        }
    }

    public class DadosApi
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
    }
}
