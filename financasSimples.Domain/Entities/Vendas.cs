namespace financasSimples.Domain.Entities;
public class Vendas
{
    public int IdVenda { get; set; }
    public DateOnly DataVenda { get; set; }
    public ICollection<ItensVendas>? ItensVendas { get; set; }
}
