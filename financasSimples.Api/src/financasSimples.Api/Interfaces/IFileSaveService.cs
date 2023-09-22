using Microsoft.AspNetCore.Mvc;

namespace financasSimples.Api.interfaces;

public interface IFileSaveService
{
    Task<List<string>> SaveFiles(List<IFormFile> files, string _pasta);
    Task<byte[]> GetArquivoAsync(string nomeArquivo, string pasta);
}