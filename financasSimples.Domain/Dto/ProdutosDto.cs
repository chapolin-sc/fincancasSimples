using System.ComponentModel.DataAnnotations;
using financasSimples.Domain.Entities;

namespace financasSimples.Domain.Dto;
public class ProdutosDto
{
    public int? IdProdutoDto { get; set; }

    [Display(Name="Nome")]
    [StringLength(100, ErrorMessage="Ultrapassado o número de caracteres!")]
    [Required(ErrorMessage="O nome é obrigatório!", AllowEmptyStrings=false)]
    public string NomeProdutoDto { get; set; } = null!;

    [Display(Name="Imagem")]
    [StringLength(100, ErrorMessage="Ultrapassado o número de caracteres!")]
    public string? ImagemProdutoDto { get; set; }

    [Display(Name="Volume")]
    [StringLength(10, ErrorMessage="Ultrapassado o número de caracteres!")]
    [Required(ErrorMessage="O volume é obrigatório!", AllowEmptyStrings=false)]
    public string VolumeProdutoDto { get; set; } = null!;

    [Display(Name="Marca")]
    [StringLength(50, ErrorMessage="Ultrapassado o número de caracteres!")]
    public string? MarcaProdutoDto { get; set; }

    [Display(Name="Descrição")]
    [StringLength(250, ErrorMessage="Ultrapassado o número de caracteres!")]
    public string? DescricaoProdutoDto { get; set; }

    public ProdutosDto()
    {
    }

    public ProdutosDto(Produtos produtos)
    {
        IdProdutoDto = produtos.IdProduto;
        NomeProdutoDto = produtos.NomeProduto;
        ImagemProdutoDto = produtos.ImagemProduto;
        VolumeProdutoDto = produtos.VolumeProduto;
        MarcaProdutoDto = produtos.MarcaProduto;
        DescricaoProdutoDto = produtos.DescricaoProduto;
    }

    public void recebeProduto(Produtos produtos)
    {
        IdProdutoDto = produtos.IdProduto;
        NomeProdutoDto = produtos.NomeProduto;
        ImagemProdutoDto = produtos.ImagemProduto;
        VolumeProdutoDto = produtos.VolumeProduto;
        MarcaProdutoDto = produtos.MarcaProduto;
        DescricaoProdutoDto = produtos.DescricaoProduto;
    }
}
