namespace financasSimples.Application.Classes;

public class UsuarioCadastroResponse
{
    public bool Success { get; private set; }

    public List<string> Error { get; private set; }

    
    public UsuarioCadastroResponse() => 
        Error = new List<string>();

    public UsuarioCadastroResponse(bool success = true) : this() =>
        Success = success;

    public void AdicionarError(string error) =>
        Error.Add(error);

    public void AdicionarErrors(IEnumerable<string> errors) =>
        Error.AddRange(errors);
}