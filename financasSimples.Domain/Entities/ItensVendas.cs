namespace financasSimples.Domain.Entities;

public class ItensVendas
{
    public int IdItensVendas { get; set; }
    public decimal IvPreco { get; set; }
    public decimal IvCusto { get; set; }
    public int IvQuantidade { get; set; }
    public int? IdVenda { get; set; }
    public Vendas? Venda { get; set; }
    public int? IdProduto { get; set; }
    public Produtos? Produto { get; set; }
}