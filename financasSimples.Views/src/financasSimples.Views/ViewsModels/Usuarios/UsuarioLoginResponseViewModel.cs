namespace financasSimples.Views.ViewsModels.Usuarios;

public class UsuarioLoginResponseViewModel
{
    public bool Success { get; set; }

    public string Token { get; set; }

    public DateTime ExpiracaoToken { get; set; }

    public List<string> Error { get; set; }




    /*public UsuarioLoginResponseViewModel()
    {
        Error = new List<string>();
    }*/
}