//  Erasmo Cardoso

using Microsoft.EntityFrameworkCore;
using AcessoAPI.Data;
using AcessoAPI.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AcessoAPI.API;

var builder = WebApplication.CreateBuilder(args);

// Entity Framework Core para usar o SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada.");
}

// Configura o DbContext para usar o SQL Server
builder.Services.AddDbContext<ControleSistemaContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions => 
        sqlOptions.EnableRetryOnFailure()));

// controladores
builder.Services.AddControllers();

// HttpClient 
builder.Services.AddHttpClient();



//  CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodos", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//  Repositórios
builder.Services.AddScoped<FornecedorRepository>();
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<AcessoAPI.Repository.NotaFiscalRepository>();



// Analisando se uso ainda deixar para final
builder.Services.AddScoped<DadosApiExt>();

// Swagger 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//  Configuração do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplicar  CORS
app.UseCors("PermitirTodos");

// Middlewares
app.UseAuthorization();
app.MapControllers();

// Start
app.Run();
