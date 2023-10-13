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
    public readonly AwsCredenciais _credenciais = new AwsCredenciais();

    public readonly string nomeBucket = "";

    public ProdutosController(IProdutosRepository produtosRepository, IConfiguration configuration, IFileSaveService fileSaveService,
        IFileS3Transfer fileS3Transfer, IMetodosAuxiliares metodosAuxiliares)
    {
        _produtosRepository = produtosRepository;
        _Configuration = configuration;
        _fileSaveService = fileSaveService;
        _fileS3Transfer = fileS3Transfer;
        _metodosAuxiliares = metodosAuxiliares;

        //Configurações salvas no meu arquivo appsettings.Development, chaves não compartilhadas no github
        nomeBucket = _Configuration.GetSection("AwsConfiguration").GetValue<string>("AwsS3BucketImagens");
        _credenciais.AwsKey = _Configuration.GetSection("AwsConfiguration").GetValue<string>("AwsAccessKey");
        _credenciais.AwsSecretKey = _Configuration.GetSection("AwsConfiguration").GetValue<string>("AwsSecretKey");


        // ifs que acessam variaveis globais na aws, necessario para não haverem commits equivocados com chaves de acesso
        if(Environment.GetEnvironmentVariable("AwsS3BucketImagens") != null)
        {
            nomeBucket = Environment.GetEnvironmentVariable("AwsS3BucketImagens");
        }

        if(Environment.GetEnvironmentVariable("AwsAccessKey") != null && Environment.GetEnvironmentVariable("AwsSecretKey") != null)
        {
            _credenciais.AwsKey = Environment.GetEnvironmentVariable("AwsAccessKey");
            _credenciais.AwsSecretKey = Environment.GetEnvironmentVariable("AwsSecretKey");
        }
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
                byte[] imagemBytes = await _fileS3Transfer.DownloadFileS3Async(produto.ImagemProdutoNomeDto, nomeBucket, _credenciais);

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
            byte[] imagemBytes = await _fileS3Transfer.DownloadFileS3Async(produtos.ImagemProdutoNomeDto, nomeBucket, _credenciais);

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
            ResquestResponse response = await _fileS3Transfer.UploadFileS3Async(imagem, novoNomeImagem, nomeBucket, _credenciais);
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

            ResquestResponse response = await _fileS3Transfer.UploadFileS3Async(imagem, novoNomeImagem, nomeBucket, _credenciais);
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