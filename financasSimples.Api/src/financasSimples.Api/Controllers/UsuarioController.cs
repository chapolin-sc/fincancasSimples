using System.Security.Principal;
using financasSimples.Application.Classes;
using financasSimples.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace financasSimples.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private IIdentityRepository _identity;



    public UsuarioController(IIdentityRepository identityRepository)
    {
        _identity = identityRepository;
    }




    [HttpPost("Cadastro")]
    public async Task<UsuarioCadastroResponse> CadastroAsync([FromForm]UsuarioCadastroRequest usuarioCadastroRequest)
    {
        UsuarioCadastroResponse usuarioCadastroResponse = await _identity.CadastroAsync(usuarioCadastroRequest);

        return usuarioCadastroResponse;
    }


    [HttpPost("Login")]
    public async Task<UsuarioLoginResponse> LoginAsync([FromForm]UsuarioLoginRequest usuarioLoginRequest)
    {
        UsuarioLoginResponse usuarioLoginResponse = await _identity.LoginAsync(usuarioLoginRequest);

        return usuarioLoginResponse;
    }
}