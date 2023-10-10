using financasSimples.Domain.Dto;

namespace financasSimples.Domain.Entities;
public class Produtos
{
  
    public int IdProduto { get; set; }
    public string NomeProduto { get; set; } = null!;
    public string? ImagemProdutoNome { get; set; }
    public string VolumeProduto { get; set; } = null!;
    public string? MarcaProduto { get; set; }
    public string? DescricaoProduto { get; set; }
    public ItensVendas? ItensVendas { get; set; }

    public Produtos()
    {
        
    }
    public Produtos(ProdutosDto produtos)
    {
        NomeProduto = produtos.NomeProdutoDto;
        ImagemProdutoNome = produtos.ImagemProdutoNomeDto;
        VolumeProduto = produtos.VolumeProdutoDto;
        MarcaProduto = produtos.MarcaProdutoDto;
        DescricaoProduto = produtos.DescricaoProdutoDto;
    }

    public Produtos(int id, ProdutosDto produtos)
    {
        IdProduto = id;
        NomeProduto = produtos.NomeProdutoDto;
        ImagemProdutoNome = produtos.ImagemProdutoNomeDto;
        VolumeProduto = produtos.VolumeProdutoDto;
        MarcaProduto = produtos.MarcaProdutoDto;
        DescricaoProduto = produtos.DescricaoProdutoDto;
    }
}
