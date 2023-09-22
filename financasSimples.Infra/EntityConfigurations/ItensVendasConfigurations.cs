using financasSimples.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace financasSimples.Infra.EntityConfiguration;

public class ItensVendasConfiguration : IEntityTypeConfiguration<ItensVendas>
{
    public void Configure(EntityTypeBuilder<ItensVendas> builder)
    {
        builder.HasKey(x => x.IdItensVendas);
        builder.Property(x => x.IvPreco).HasPrecision(10,2).IsRequired();
        builder.Property(x => x.IvCusto).HasPrecision(10,2).IsRequired();
        builder.Property(x => x.IvQuantidade).IsRequired();
        builder.Property(x => x.IdVenda).IsRequired();
        builder.Property(x => x.IdProduto).IsRequired();

        builder.HasOne(x => x.Venda).WithMany(x => x.ItensVendas)
            .HasForeignKey(x => x.IdVenda);
        builder.HasOne(x => x.Produto).WithOne(x => x.ItensVendas)
            .HasForeignKey<ItensVendas>(x => x.IdProduto);
    }
}