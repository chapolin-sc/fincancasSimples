using financasSimples.Infra.Context;
using financasSimples.Infra.Repositories;
using financasSimples.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;


namespace financasSimples.IoC;

public static class DbInjection 
{
    public static async Task<IServiceCollection> AddDbInfra(this IServiceCollection services, IConfiguration configuration, string banco)
    {
        if(banco == "sqlite")
        {
            //var connectionString = configuration.GetConnectionString("DefaultConnection");
            SqliteConnection connection = new SqliteConnection("Data Source=:memory:");
            await connection.OpenAsync();

            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlite(connection));

            AppDbContext context = services.BuildServiceProvider().GetService<AppDbContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            
            return services;
        }

        var connectionString = configuration.GetConnectionString("DefaultConnection");
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