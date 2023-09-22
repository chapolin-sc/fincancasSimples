using financasSimples.Infra.Context;
using financasSimples.Infra.Repositories;
using financasSimples.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace financasSimples.IoC;

public static class DbInjection 
{
    public static IServiceCollection AddDbInfra(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString  = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options => options.UseMySql(
            connectionString, ServerVersion.AutoDetect(connectionString),
                 b => b.MigrationsAssembly(typeof(AppDbContext)
                            .Assembly.FullName)));
        return services;
    }

    public static IServiceCollection AddRepositeriesInfra(this IServiceCollection services)
    {
        services.AddScoped<IProdutosRepository, ProdutosRepository>();    
        return services;
    }
}

/*public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    /*public readonly IConfiguration _configuration;
    public AppDbContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AppDbContextFactory()
    {

    }

    /*public AppDbContext CreateDbContext(string[] args)
    {
            /*IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Criando o DbContextOptionsBuilder manualmente
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            
            // cria a connection string. 
            // requer a connectionstring no appsettings.json
            //var connectionString = _configuration.GetConnectionString("DefaultConnection");
            //builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            builder.UseMySql("server=127.0.0.1;uid=root;pwd=;database=financasSimples", ServerVersion.AutoDetect("server=127.0.0.1;uid=root;pwd=;database=financasSimples"));

            // Cria o contexto
            return new AppDbContext(builder.Options);
    }

    /*public AppDbContext CreateDbContext(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            configuration.SetBasePath(Directory.GetCurrentDirectory());
            configuration.AddJsonFile("appsettings.json");

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            return new AppDbContext(dbContextOptionsBuilder.Options);
        }
}*/