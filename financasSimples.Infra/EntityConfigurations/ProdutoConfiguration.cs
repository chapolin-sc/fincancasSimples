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

        builder.HasData(
            new Produtos
            {
                IdProduto = 1,
                NomeProduto = "Floratta",
                ImagemProduto = "dc1e6fa65e8042b8b74ff010937f975b_45551560x950c.webp",
                VolumeProduto = "50ml",
                MarcaProduto = "Boticário",
                DescricaoProduto = "Perfume"
            },
            new Produtos
            {
                IdProduto = 2,
                NomeProduto = "Ameixa Negra",
                ImagemProduto = "635b003fcb1140dd88f6aca051c2fd6d_45551560x950c.webp",
                VolumeProduto = "40ml",
                MarcaProduto = "Boticário",
                DescricaoProduto = "Hidratante"
            }
        );
        
    }
}