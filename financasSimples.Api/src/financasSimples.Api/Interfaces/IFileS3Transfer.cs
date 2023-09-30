using financasSimples.Infra.Classes;
using Microsoft.AspNetCore.Mvc;

namespace financasSimples.Api.interfaces;

public interface IFileS3Transfer
{
    Task<ResquestResponse> UploadFileS3Async(IFormFile file, string nomeArquivo, string nomeBucket);
    Task<byte[]> DownloadFileS3Async(string nomeArquivo, string nomeBucket);
}