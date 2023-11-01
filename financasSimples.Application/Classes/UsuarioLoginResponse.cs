namespace financasSimples.Application.Classes;

public class UsuarioLoginResponse
{
    
    public bool Success { get; private set; }

    public string Token { get; private set; }

    public DateTime ExpiracaoToken { get; private set; }

    public List<string> Error { get; private set; }


    
    public UsuarioLoginResponse() => 
        Error = new List<string>();

    public UsuarioLoginResponse(bool success = true) : this() =>
        Success = success;

    public UsuarioLoginResponse(bool success, string token, DateTime expedicaoToken) : this(success)
    {
        Token = token;
        ExpiracaoToken = expedicaoToken;
    }
    


    public void AdicionarError(string error) =>
        Error.Add(error);

    public void AdicionarErrors(IEnumerable<string> errors) =>
        Error.AddRange(errors);

}