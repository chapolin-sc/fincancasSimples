using financasSimples.Domain.Dto;
using financasSimples.Domain.Entities;

namespace financasSimples.Domain.Interfaces;

public interface IProdutosRepository
{
    public Task<Produtos> Incluir(ProdutosDto produto);
    public Task<Produtos> Alterar(int id, ProdutosDto produto);
    public Task<Produtos> Excluir(int id);
    public Task<ProdutosDto> Selecionar(int id);
    public Task<IEnumerable<ProdutosDto>> SelecionarTodos();

}