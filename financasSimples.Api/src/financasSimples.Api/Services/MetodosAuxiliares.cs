using financasSimples.Api.interfaces;

namespace financasSimples.Api.Services;

public class MetodosAuxiliares : IMetodosAuxiliares
{
    public string GeraNovoNomeComGuid(string fileName)
    {
        var novoFileName = (Guid.NewGuid().ToString() + "_" + fileName).ToLower();
        novoFileName = novoFileName.Replace("-", "");

        return novoFileName;
    }

    public async Task<byte[]> GetFileComoArrayDeBytesAsync(IFormFile file)
    {
        using(var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}