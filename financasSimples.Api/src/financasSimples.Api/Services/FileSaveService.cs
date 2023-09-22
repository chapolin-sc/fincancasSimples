using financasSimples.Api.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace financasSimples.Api.Services;

public class FileSaveService : IFileSaveService
{

    public async Task<List<string>> SaveFiles(List<IFormFile> files, string _pasta)
    {
        try
        {
            List<string> fileNames = new List<string>();

            foreach(var file in files)
            {
                string fileName = GeraNovoNome(file.FileName);
                byte[] fileBytes = await GetFileParaArrayDeBytesAsync(file);
                string diretorio = CriaPathDeFile(fileName, _pasta);
                await File.WriteAllBytesAsync(diretorio, fileBytes);

                fileNames.Add(fileName);
            }
            return fileNames;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<byte[]> GetArquivoAsync(string nomeArquivo, string pasta)
    {
        if(nomeArquivo == null)
        {
            return null;
        }
        
        string path = Path.Combine(Directory.GetCurrentDirectory(), pasta, nomeArquivo);

        if(!File.Exists(path))
        {
            return null;
        }

        byte[] arquivosBytes = await File.ReadAllBytesAsync(path);
        return arquivosBytes;
    }

    private async Task<byte[]> GetFileParaArrayDeBytesAsync(IFormFile file)
    {
        using(var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }

    private string GeraNovoNome(string fileName)
    {
        var novoFileName = (Guid.NewGuid().ToString() + "_" + fileName).ToLower();
        novoFileName = novoFileName.Replace("-", "");

        return novoFileName;
    }

    private string CriaPathDeFile(string fileName, string pasta)
    {
        return Path.Combine(Directory.GetCurrentDirectory(), pasta, fileName);
    }
}
