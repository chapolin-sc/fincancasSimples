using financasSimples.Application.Classes;

namespace financasSimples.Application.Interface;

public interface IIdentityRepository 
{
    Task<UsuarioCadastroResponse> CadastroAsync(UsuarioCadastroRequest usuario);

    Task<UsuarioLoginResponse> LoginAsync(UsuarioLoginRequest usuario);
}