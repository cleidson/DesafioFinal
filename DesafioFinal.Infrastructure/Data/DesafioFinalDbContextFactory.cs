using DesafioFinal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DesafioFinal.Infrastructure.Data
{ 
    public class DesafioFinalDbContextFactory : IDesignTimeDbContextFactory<DesafioFinalContext>
    {
        public DesafioFinalContext CreateDbContext(string[] args)
        {
            // Carregar a configuração do appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName + "/DesafioFinal.Api")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Configurar o DbContextOptions para PostgreSQL
            var optionsBuilder = new DbContextOptionsBuilder<DesafioFinalContext>();
            var connectionString = configuration.GetConnectionString("DesafioFinalDb");

            optionsBuilder.UseNpgsql(connectionString);

            // Retornar a instância do DbContext
            return new DesafioFinalContext(optionsBuilder.Options);
        }
    }
}
