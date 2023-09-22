using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using financasSimples.Infra.Context;
using Microsoft.Extensions.Configuration;
using financasSimples.Infra;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace financasSimples.Api;
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    //private readonly string connectionString;
    public AppDbContext CreateDbContext(string[] args)
    {
            // Criando o DbContextOptionsBuilder manualmente
            var builder = new DbContextOptionsBuilder<AppDbContext>();

            builder.UseMySql("server=127.0.0.1;uid=root;pwd=;database=financasSimples", ServerVersion.AutoDetect("server=127.0.0.1;uid=root;pwd=;database=financasSimples"));

            // Cria o contexto
            return new AppDbContext(builder.Options);
    }

    

}