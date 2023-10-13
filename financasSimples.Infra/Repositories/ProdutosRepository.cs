using financasSimples.Domain.Dto;
using financasSimples.Domain.Entities;
using financasSimples.Domain.Interfaces;
using financasSimples.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace financasSimples.Infra.Repositories;

public class ProdutosRepository : IProdutosRepository
{
    public readonly AppDbContext _context;
    public Produtos? _produtos;

    public ProdutosRepository(AppDbContext context){
        _context = context;
    }

    public async Task<Produtos> Alterar(int id, ProdutosDto produto)
    {
        if(id != produto.IdProdutoDto)
        {
            return null;
        }

        _produtos = new Produtos(id, produto);
        _context.Produto.Update(_produtos);
        await _context.SaveChangesAsync();
        return _produtos;
    }

    public async Task<Produtos> Excluir(int id)
    {
        _produtos = await _context.Produto.FindAsync(id);
        if(_produtos == null)
        {
            return null;
        }
        _context.Produto.Remove(_produtos);
        await _context.SaveChangesAsync();
        return _produtos;
    }

    public async Task<Produtos> Incluir(ProdutosDto produto)
    {
        _produtos = new Produtos(produto);
        _context.Produto.Add(_produtos);
        await _context.SaveChangesAsync();
        return _produtos;
    }


    public async Task<ProdutosDto> Selecionar(int id)
    {
        _produtos = await _context.Produto.FindAsync(id);
        if(_produtos == null)
        {
            return null;
        }
        var produtoDto = new ProdutosDto(_produtos);
        return produtoDto;
    }

    public async Task<IEnumerable<ProdutosDto>> SelecionarTodos()
    {
        var produtos = await _context.Produto.AsNoTracking().ToListAsync();
        if(produtos == null)
        {
            return null;
        }

        /*** Codigo interessante de colocar na classe produtos Inicio ***/
        ProdutosDto produtoDto = null;
        var produtoDtoList = new List<ProdutosDto>();
        foreach(var produto in produtos)
        {
            produtoDto = new ProdutosDto(produto);
            produtoDtoList.Add(produtoDto);
        }
        /*** Codito interessante de colocar na classe produtos Fim ***/

        return produtoDtoList;
    }

}