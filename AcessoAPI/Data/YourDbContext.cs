//  Erasmo Cardoso

using Microsoft.EntityFrameworkCore;
using AcessoAPI.Models;
using static AcessoAPI.API.DadosApiExt;

namespace AcessoAPI.Data
{
    public class ControleSistemaContext : DbContext
    {
        public ControleSistemaContext(DbContextOptions<ControleSistemaContext> options) : base(options) 
        {
            //desnecessario analisando
        }

        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<NotaFiscal> NotaFiscal { get; set; } = null!;
        public DbSet<Produto> Produtos { get; set; } = null!;
        public DbSet<Fornecedor> Fornecedores { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // entidades
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<NotaFiscal>().ToTable("NotaFiscal");
            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<Fornecedor>().ToTable("Fornecedores");
            modelBuilder.Entity<Post>().ToTable("Posts");

            base.OnModelCreating(modelBuilder);
        }

        // nao necessario depois limpar
        public async Task<Cliente?> GetClienteByIdUsingStoredProcedureAsync(int id)
        {
            return await Clientes
                .FromSqlRaw("EXEC GetClienteById @Id = {0}", id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        // nao necessario depois limpar
        public async Task<Produto?> GetProdutoByIdUsingStoredProcedureAsync(int id)
        {
            return await Produtos
                .FromSqlRaw("EXEC GetProdutoById @Id = {0}", id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        // nao necessario depois limpar
        public async Task<Fornecedor?> GetFornecedorByIdUsingStoredProcedureAsync(int id)
        {
            return await Fornecedores
                .FromSqlRaw("EXEC GetFornecedorById @Id = {0}", id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        // nao necessario depois limpar
        public async Task<NotaFiscal?> GetNotaFiscalByIdUsingStoredProcedureAsync(int id)
        {
            return await NotaFiscal
                .FromSqlRaw("EXEC GetNotaFiscalById @Id = {0}", id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
