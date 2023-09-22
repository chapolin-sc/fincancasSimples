using financasSimples.Api.interfaces;
using financasSimples.Domain.Dto;
using financasSimples.Domain.Entities;
using financasSimples.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace financasSimples.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    public readonly IProdutosRepository _produtosRepository;
    public readonly IConfiguration _Configuration;
    public readonly IFileSaveService _fileSaveService;

    

    public ProdutosController(IProdutosRepository produtosRepository, IConfiguration configuration, IFileSaveService fileSaveService)
    {
        _produtosRepository = produtosRepository;
        _Configuration = configuration;
        _fileSaveService = fileSaveService;
    }



    // GET api/values
    [HttpGet]
    public async Task<IEnumerable<ProdutosDto>> Get()
    {
        IEnumerable<ProdutosDto> produtos = new List<ProdutosDto>();
        produtos = await _produtosRepository.SelecionarTodos();

        byte[] imagemBytes;
        foreach(ProdutosDto produto in produtos)
        {
            imagemBytes = await _fileSaveService.GetArquivoAsync(produto.ImagemProdutoDto, "Imagens");

            if(imagemBytes != null)
            {
                produto.ImagemProdutoDto = Convert.ToBase64String(imagemBytes);
            }else
            {
                produto.ImagemProdutoDto = null;
            }
        }
        
        return produtos;
    }



    // GET api/values/5
    [HttpGet("{id}")]
    public async Task<ProdutosDto> Get(int id)
    {
        return await _produtosRepository.Selecionar(id);
    }



    [HttpPost]
    public async Task Post([FromForm]ProdutosDto produto)
    {
        await _produtosRepository.Incluir(produto);
    }



    // POST api/values/CreateComImagem
    [HttpPost("CreateComImagem")]
    public async Task Post([FromForm]ProdutosDto produto, IFormFile imagem)
    {
         if(imagem != null && imagem.Length > 0) 
        {
            List<IFormFile> files = new List<IFormFile>();
            List<string> imagensNomes = new List<string>();
            
            files.Add(imagem);
            imagensNomes = await _fileSaveService.SaveFiles(files, "Imagens");
            produto.ImagemProdutoDto = imagensNomes[0];
        }

        await _produtosRepository.Incluir(produto);
    }



    // PUT api/values/5
    [HttpPut("{id}")]
    public async Task<Produtos> Put(int id, [FromForm]ProdutosDto produto)
    {        
        return await _produtosRepository.Alterar(id, produto);
    }



    // PUT api/values/UpComImagem/5
    [HttpPut("UpComImagem/{id}")]
    public async Task<Produtos> Put(int id, [FromForm]ProdutosDto produto, IFormFile imagem)
    {
        if(imagem != null && imagem.Length > 0) 
        {
            List<IFormFile> files = new List<IFormFile>();
            List<string> imagensNomes = new List<string>();
            
            files.Add(imagem);
            imagensNomes = await _fileSaveService.SaveFiles(files, "Imagens");
            produto.ImagemProdutoDto = imagensNomes[0];
        }
        
        return await _produtosRepository.Alterar(id, produto);
    }



    // DELETE api/values/5
    [HttpDelete("{id}")]
    public async Task<Produtos> Delete(int id)
    {
        return await _produtosRepository.Excluir(id);
    }
}