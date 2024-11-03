//Erasmo Cardoso
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AcessoAPI.API
{
    public class DadosApiExt
    {
        private readonly string _connectionString;
        private readonly HttpClient _httpClient;
        private readonly ILogger<DadosApiExt> _logger;

        public DadosApiExt(IConfiguration configuration, HttpClient httpClient, ILogger<DadosApiExt> logger)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("A string é nula ou vazia.");
            }

            Console.WriteLine("A string não está nula ou vazia. Tudo OK."); 

            _connectionString = connectionString;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task InsertPostsIntoDatabase(CancellationToken cancellationToken = default)
        {
            var posts = await FetchPostsAsync(cancellationToken);

            if (posts == null || posts.Count == 0)
            {
                _logger.LogWarning("Nenhum post foi encontrado para inserir no banco de dados.");
                return;
            }

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            foreach (var post in posts)
            {
                if (post == null || string.IsNullOrWhiteSpace(post.Title) || string.IsNullOrWhiteSpace(post.Body))
                {
                    _logger.LogWarning($"Post inválido encontrado: {JsonSerializer.Serialize(post)}");
                    continue;
                }

                using var command = new SqlCommand("sp_InsertPost", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@UserId", post.UserId);
                command.Parameters.AddWithValue("@Id", post.Id);
                command.Parameters.AddWithValue("@Title", post.Title);
                command.Parameters.AddWithValue("@Body", post.Body);

                try
                {
                    await command.ExecuteNonQueryAsync(cancellationToken);
                    _logger.LogInformation($"Post inserido: {post.Id}");
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, $"Erro ao inserir o post com ID {post.Id}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Erro inesperado ao inserir o post com ID {post.Id}: {ex.Message}");
                }
            }
        }

        private async Task<List<Post>> FetchPostsAsync(CancellationToken cancellationToken)
        {
            try
            {
                using var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts", cancellationToken);
                response.EnsureSuccessStatusCode();

                var posts = await response.Content.ReadFromJsonAsync<List<Post>>(cancellationToken: cancellationToken);
                
                if (posts == null)
                {
                    _logger.LogWarning("A resposta da API não contém posts válidos.");
                }

                return posts ?? new List<Post>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro ao chamar a API para buscar posts.");
                return new List<Post>();
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Erro ao desserializar a resposta da API.");
                return new List<Post>();
            }
        }

        public class Post
        {
            public int UserId { get; set; }
            public int Id { get; set; }
            public string? Title { get; set; }
            public string? Body { get; set; }
        }

        public class DadosApiBackgroundService : BackgroundService
        {
            private readonly DadosApiExt _dadosApiExt;
            private readonly ILogger<DadosApiBackgroundService> _logger;

            public DadosApiBackgroundService(DadosApiExt dadosApiExt, ILogger<DadosApiBackgroundService> logger)
            {
                _dadosApiExt = dadosApiExt;
                _logger = logger;
            }

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                _logger.LogInformation("Serviço de background inicializado para inserir posts no banco de dados.");

                try
                {
                    await _dadosApiExt.InsertPostsIntoDatabase(stoppingToken);

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                        await _dadosApiExt.InsertPostsIntoDatabase(stoppingToken);
                    }
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Operação cancelada. Serviço de background encerrado.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro inesperado no serviço de background.");
                }
            }
        }
    }
}
