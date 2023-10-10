using financasSimples.Api.interfaces;
using financasSimples.Domain.Dto;
using financasSimples.Domain.Entities;
using financasSimples.Domain.Interfaces;
using financasSimples.Infra.Classes;
using Microsoft.AspNetCore.Mvc;


namespace financasSimples.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    public readonly IProdutosRepository _produtosRepository;
    public readonly IConfiguration _Configuration;
    public readonly IFileSaveService _fileSaveService;
    public readonly IFileS3Transfer _fileS3Transfer;
    public readonly IMetodosAuxiliares _metodosAuxiliares;

    public readonly string nomeBucket = "";

    public ProdutosController(IProdutosRepository produtosRepository, IConfiguration configuration, IFileSaveService fileSaveService,
        IFileS3Transfer fileS3Transfer, IMetodosAuxiliares metodosAuxiliares)
    {
        _produtosRepository = produtosRepository;
        _Configuration = configuration;
        _fileSaveService = fileSaveService;
        _fileS3Transfer = fileS3Transfer;
        _metodosAuxiliares = metodosAuxiliares;
        nomeBucket = _Configuration.GetSection("AwsConfiguration").GetValue<string>("AwsS3BucketImagens");
    }



    // GET api/values
    [HttpGet]
    public async Task<IEnumerable<ProdutosDto>> Get()
    {
        IEnumerable<ProdutosDto> produtos = new List<ProdutosDto>();
        produtos = await _produtosRepository.SelecionarTodos();

        foreach(ProdutosDto produto in produtos)
        {
            if(produto.ImagemProdutoNomeDto != null && produto.ImagemProdutoNomeDto.Length > 0)
            {
                byte[] imagemBytes = await _fileS3Transfer.DownloadFileS3Async(produto.ImagemProdutoNomeDto, nomeBucket);

                //O codigo usa a mesma variavel que continha o nome da imagem para salvar a imagem em base64
                if(imagemBytes != null)
                {
                    produto.ImagemProdutoDto = Convert.ToBase64String(imagemBytes);
                }
            }   
        }
        return produtos;
    }



    // GET api/values/5
    [HttpGet("{id}")]
    public async Task<ProdutosDto> Get(int id)
    {
        ProdutosDto produtos = new ProdutosDto();
        produtos = await _produtosRepository.Selecionar(id);

        if(produtos.ImagemProdutoNomeDto != null && produtos.ImagemProdutoNomeDto.Length > 0)
        {
            byte[] imagemBytes = await _fileS3Transfer.DownloadFileS3Async(produtos.ImagemProdutoNomeDto, nomeBucket);

            //O codigo usa a mesma variavel que continha o nome da imagem para salvar a imagem em base64
            if(imagemBytes != null)
            {
                produtos.ImagemProdutoDto = Convert.ToBase64String(imagemBytes);
            }
        }   

        return produtos;
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
            string novoNomeImagem = _metodosAuxiliares.GeraNovoNomeComGuid(imagem.FileName);
            ResquestResponse response = await _fileS3Transfer.UploadFileS3Async(imagem, novoNomeImagem, nomeBucket);
            if(response.StatusCode != 200)
            {
                Console.WriteLine($"Erro: {response.Mensagem}");
            }

            produto.ImagemProdutoNomeDto = novoNomeImagem;
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
    public async Task Put(int id, [FromForm]ProdutosDto produto, IFormFile imagem)
    {
         if(imagem != null && imagem.Length > 0) 
        {
            string novoNomeImagem = _metodosAuxiliares.GeraNovoNomeComGuid(imagem.FileName);

            ResquestResponse response = await _fileS3Transfer.UploadFileS3Async(imagem, novoNomeImagem, nomeBucket);
            if(response.StatusCode != 200)
            {
                Console.WriteLine($"Erro: {response.Mensagem}");
            }
           
            produto.ImagemProdutoNomeDto = novoNomeImagem;
        }
        
        await _produtosRepository.Alterar(id, produto);
    }



    // DELETE api/values/5
    [HttpDelete("{id}")]
    public async Task<Produtos> Delete(int id)
    {
        return await _produtosRepository.Excluir(id);
    }
}