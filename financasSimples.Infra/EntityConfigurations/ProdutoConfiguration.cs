using financasSimples.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace financasSimples.Infra.EntityConfiguration;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produtos>
{
    public void Configure(EntityTypeBuilder<Produtos> builder)
    {
        builder.HasKey(x => x.IdProduto);
        builder.Property(x => x.NomeProduto).HasMaxLength(100).IsRequired();
        builder.Property(x => x.ImagemProduto).HasMaxLength(100);
        builder.Property(x => x.VolumeProduto).HasMaxLength(10);
        builder.Property(x => x.MarcaProduto).HasMaxLength(50);
        builder.Property(x => x.DescricaoProduto).HasMaxLength(250);
    }
}