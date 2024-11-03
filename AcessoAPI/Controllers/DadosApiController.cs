//Erasmo Cardoso

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AcessoAPI.Models;

namespace AcessoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DadosApiController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly HttpClient _httpClient;
        private readonly ILogger<DadosApiController> _logger;

        public DadosApiController(IConfiguration configuration, HttpClient httpClient, ILogger<DadosApiController> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada.");
            _httpClient = httpClient;
            _logger = logger;
        }

        // Rota para buscar dados do banco
        [HttpGet("db-posts")]
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
                        using (var reader = await command.ExecuteReaderAsync())
                        {
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
                }

                return Ok(dados);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erro ao acessar o banco de dados.");
                return StatusCode(500, "Erro ao acessar o banco de dados: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado.");
                return StatusCode(500, "Erro inesperado: " + ex.Message);
            }
        }

        //  API externa
        [HttpGet("external-posts")]
        public async Task<IActionResult> GetExternalPosts()
        {
            try
            {
                var posts = await _httpClient.GetFromJsonAsync<List<DadosApi>>("https://jsonplaceholder.typicode.com/posts");

                if (posts == null || posts.Count == 0)
                {
                    _logger.LogWarning("Nenhum post encontrado na API externa.");
                    return NotFound("Nenhum post encontrado na API externa.");
                }

                return Ok(posts);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro ao chamar a API externa.");
                return StatusCode(500, "Erro ao acessar a API externa: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao acessar a API externa.");
                return StatusCode(500, "Erro inesperado: " + ex.Message);
            }
        }

        // API externa POST
        [HttpPost("save-external-posts")]
        public async Task<IActionResult> SaveExternalPosts()
        {
            try
            {
                var posts = await _httpClient.GetFromJsonAsync<List<DadosApi>>("https://jsonplaceholder.typicode.com/posts");

                if (posts == null || posts.Count == 0)
                {
                    _logger.LogWarning("Nenhum post encontrado na API externa.");
                    return NotFound("Nenhum post encontrado na API externa.");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    foreach (var post in posts)
                    {
                        // Verifica se o registro já existe
                        using var checkCommand = new SqlCommand("SELECT COUNT(1) FROM Posts WHERE Id = @Id", connection);
                        checkCommand.Parameters.AddWithValue("@Id", post.Id);

#pragma warning disable CS8605 // Executando a conversão unboxing de um valor possivelmente nulo.
                        var exists = (int)await checkCommand.ExecuteScalarAsync() > 0;
#pragma warning restore CS8605 // Executando a conversão unboxing de um valor possivelmente nulo.

                        if (!exists)
                        {
                            // Insere o registro caso não exista
                            using var insertCommand = new SqlCommand(
                                "INSERT INTO Posts (UserId, Id, Title, Body) VALUES (@UserId, @Id, @Title, @Body)", connection);
                            insertCommand.Parameters.AddWithValue("@UserId", post.UserId);
                            insertCommand.Parameters.AddWithValue("@Id", post.Id);
                            insertCommand.Parameters.AddWithValue("@Title", post.Title ?? (object)DBNull.Value);
                            insertCommand.Parameters.AddWithValue("@Body", post.Body ?? (object)DBNull.Value);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                }

                return Ok("Dados da API externa salvos no banco de dados com sucesso.");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro ao chamar a API externa.");
                return StatusCode(500, "Erro ao acessar a API externa: " + ex.Message);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erro ao salvar dados no banco de dados.");
                return StatusCode(500, "Erro ao salvar dados no banco de dados: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao salvar dados no banco de dados.");
                return StatusCode(500, "Erro inesperado: " + ex.Message);
            }
        }
    }
}
