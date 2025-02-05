
using DesafioFinal.Api.Middlewares;
using DesafioFinal.Core.Logic.Interfaces.ExceptionHandler;
using DesafioFinal.Core.Logic.Interfaces.Repositories;
using DesafioFinal.Core.Logic.Interfaces.Services;
using DesafioFinal.Core.Services;
using DesafioFinal.Infrastructure.Data;
using DesafioFinal.Infrastructure.ExceptionHandler;
using DesafioFinal.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace DesafioFinal.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adicionar serviços ao contêiner
            builder.Services.AddControllers();

            // Configurar Swagger/OpenAPI
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Desafio final API",
                    Version = "v1"
                });
            });

            // Configurar CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", corsBuilder =>
                {
                    corsBuilder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                });
            });

            // Registrar os handlers no contêiner de DI
            builder.Services.AddScoped<IExceptionHandler, TimeoutExceptionHandler>();
            builder.Services.AddScoped<IExceptionHandler, InvalidOperationExceptionHandler>();
            builder.Services.AddScoped<IExceptionHandler, GenericExceptionHandler>();
            
            // Registro do repositório genérico
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Registro do repositório específico de clientes
            builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

            // Registro do serviço ClienteService
            builder.Services.AddScoped<IClienteService, ClienteService>(); 

            // Registro do DesafioFinalDb 
            builder.Services.AddDbContext<DesafioFinalContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DesafioFinalDb"),
                    npgsqlOptions =>
                    {
                        npgsqlOptions.EnableRetryOnFailure();
                        npgsqlOptions.CommandTimeout(60);
                        npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    }));


            var app = builder.Build();

            // Aplicar migrations automaticamente no banco primário(apenas em dev)
            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<DesafioFinalContext>();

                    try
                    {
                        Console.WriteLine("Aplicando migrations...");
                        dbContext.Database.Migrate();
                        Console.WriteLine("Migrations aplicadas com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Falha ao aplicar migrations: {ex.Message}");
                    }

                }
            }

            // Configurações do pipeline HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(options =>
                {
                    options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        swaggerDoc.Servers = new List<OpenApiServer>
                        {
                            new OpenApiServer
                            {
                                Url = $"{httpReq.Scheme}://{httpReq.Host}"
                            }
                        };
                    });
                });

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio final API -  v1");
                });
            }
            //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // Configurar CORS
            app.UseCors("AllowAllOrigins");

            // Configurar HTTPS
            app.UseHttpsRedirection();

            // Configurar autorização
            app.UseAuthorization();

            // Configurar o middleware de exceções
            app.UseMiddleware<ExceptionMiddleware>();

            // Mapear os controllers
            app.MapControllers();

            // Log de requisições (opcional)
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"[{DateTime.Now}] Request: {context.Request.Method} {context.Request.Path}");
                await next.Invoke();
            });

            // Iniciar o aplicativo
            app.Run();
        }
    }
}
