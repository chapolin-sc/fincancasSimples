using financasSimples.Domain.Dto;

namespace financasSimples.Views.ViewsModels;
public class ProdutosViewModel
{
    public int IdProduto { get; set; }
    public string NomeProduto { get; set; } = null!;
    public IFormFile? ImagemProduto { get; set; }
    public string VolumeProduto { get; set; } = null!;
    public string? MarcaProduto { get; set; }
    public string? DescricaoProduto { get; set; }
}
