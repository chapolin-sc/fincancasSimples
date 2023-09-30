using financasSimples.Api.interfaces;
using financasSimples.Infra.Classes;
using financasSimples.Infra.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace financasSimples.Api.Services;

public class FileS3Transfer : IFileS3Transfer
{
    public IConfiguration _configuration = null!;
    public IStoregeService _storegeService = null!;

    public FileS3Transfer(IConfiguration configuration, IStoregeService storegeService)
    {
        _configuration = configuration;
        _storegeService = storegeService;
    }



    public async Task<byte[]> DownloadFileS3Async(string nomeArquivo, string nomeBucket)
    {
        var objeto = new S3Objecto
        {
            Nome = nomeArquivo,
            BucketNome = nomeBucket,
        };

        var credenciais = new AwsCredenciais
        {
            AwsKey = _configuration.GetSection("AwsConfiguration").GetValue<string>("AwsAccessKey"),
            AwsSecretKey = _configuration.GetSection("AwsConfiguration").GetValue<string>("AwsSecretKey")
        };

        byte[] arquivo = await _storegeService.DownloadFileS3Async(objeto, credenciais);
        
        return arquivo;
    }



    public async Task<ResquestResponse> UploadFileS3Async(IFormFile file, string nomeArquivo, string nameBucket)
    {
        await using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        var obj = new S3Objecto
        {
            Nome = nomeArquivo,
            BucketNome = nameBucket,
            InputStream = memoryStream
        };

        var credenciais = new AwsCredenciais
        {
            AwsKey = _configuration.GetSection("AwsConfiguration").GetValue<string>("AwsAccessKey"),
            AwsSecretKey = _configuration.GetSection("AwsConfiguration").GetValue<string>("AwsSecretKey")
        };

        ResquestResponse response = await _storegeService.UploadFileS3Async(obj, credenciais);

        return response;
    }
}