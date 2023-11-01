using financasSimples.Infra.Context;
using financasSimples.Infra.Repositories;
using financasSimples.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using financasSimples.Infra.ServicesInterfaces;
using financasSimples.Infra.Services;
using financasSimples.Identity.Context;
using financasSimples.Application.Interface;
using financasSimples.Identity.Repositories;
using Microsoft.AspNetCore.Identity;


namespace financasSimples.IoC;

public static class DbInjection 
{
    public static async Task<IServiceCollection> AddDbInfraAsync(this IServiceCollection services, IConfiguration configuration, string banco)
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

    public static IServiceCollection AddIdentity(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<IdentityDataContext>(options => options.UseMySql(
            connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityDataContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }

    //Adiciona as interfaces e implementações nos serviços do api
    public static IServiceCollection AddRepositeriesInfra(this IServiceCollection services)
    {
        services.AddScoped<IProdutosRepository, ProdutosRepository>();  
        services.AddScoped<IStoregeService, StoregeService>();
        services.AddScoped<IIdentityRepository, IdentityRepository>();

        return services;
    }
}