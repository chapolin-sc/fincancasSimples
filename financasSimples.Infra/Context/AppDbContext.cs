using financasSimples.Domain.Entities;
using financasSimples.Infra.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace financasSimples.Infra.Context;

public class AppDbContext : DbContext
{

    public DbSet<Produtos> Produto { get; set; }
    public DbSet<Vendas> Venda { get; set; }
    public DbSet<ItensVendas> ItensVenda { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
   
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ProdutoConfiguration());
        builder.ApplyConfiguration(new VendasConfiguration());
        builder.ApplyConfiguration(new ItensVendasConfiguration());
    }
         
    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=127.0.0.1;uid=root;pwd=;database=financasSimples", ServerVersion.AutoDetect("server=127.0.0.1;uid=root;pwd=;database=financasSimples"));
    }*/


}

