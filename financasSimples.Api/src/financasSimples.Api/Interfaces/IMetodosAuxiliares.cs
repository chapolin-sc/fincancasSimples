namespace financasSimples.Api.interfaces;

public interface IMetodosAuxiliares
{
    Task<byte[]> GetFileComoArrayDeBytesAsync(IFormFile file);
    string GeraNovoNomeComGuid(string fileName);
}