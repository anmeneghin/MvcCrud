using Microsoft.EntityFrameworkCore;
using MvcCrud.Models;
using System.Collections.Generic;

namespace MvcCrud.Data
{
    /// <summary>
    /// Contexto do Entity Framework Core da aplicação.
    /// 
    /// - Representa a sessão com o banco de dados.
    /// - Expõe DbSet&lt;T&gt; que representam "tabelas" (coleções) no banco.
    /// - É configurado em Program.cs via AddDbContext&lt;ApplicationDbContext&gt;(...).
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Construtor usado pelo mecanismo de Dependency Injection do ASP.NET Core.
        /// O parâmetro 'options' contém a configuração do provedor de banco (InMemory, SqlServer, Sqlite, etc.).
        /// Essa configuração é feita em Program.cs.
        /// </summary>
        /// <param name="options">Opções que configuram este DbContext.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // Aqui você poderia ajustar comportamentos do contexto, por exemplo:
            // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            // Mas no exemplo didático não é necessário alterar nada.
        }

        /// <summary>
        /// Representa a coleção de produtos no banco de dados.
        /// 
        /// - Use _context.Products para consultar, adicionar, editar e remover produtos.
        /// - O EF Core mapeia a classe Product para uma tabela e cada instância para uma linha.
        /// </summary>
        public DbSet<Product> Products => Set<Product>();

        /*
         * Observação:
         * Se precisar configurar mapeamentos mais avançados (nomes de tabela, precisões,
         * chaves compostas, relacionamentos, etc.), você pode sobrescrever OnModelCreating.
         *
         * Exemplo (comentado) para definir precisão do campo Price:
         *
         * protected override void OnModelCreating(ModelBuilder modelBuilder)
         * {
         *     base.OnModelCreating(modelBuilder);
         *     // Define precisão 18,2 para o decimal Price (opcional, dependendo do provedor)
         *     modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
         * }
         */
    }
}