using financasSimples.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace financasSimples.Infra.EntityConfiguration;

public class VendasConfiguration : IEntityTypeConfiguration<Vendas>
{
    public void Configure(EntityTypeBuilder<Vendas> builder)
    {
        builder.HasKey(x => x.IdVenda);
        builder.Property(x => x.DataVenda).IsRequired();
    }
}