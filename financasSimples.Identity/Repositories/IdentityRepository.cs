using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using financasSimples.Application.Classes;
using financasSimples.Application.Interface;
using financasSimples.Identity.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace financasSimples.Identity.Repositories;

public class IdentityRepository : IIdentityRepository
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtOptions _jwtOptions;



    public IdentityRepository(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> 
        userManager, IOptions<JwtOptions> jwtOptions)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
    }


    
    public async Task<UsuarioCadastroResponse> CadastroAsync(UsuarioCadastroRequest usuario)
    {
        IdentityUser identityUser = new IdentityUser
        {
            UserName = usuario.Email,
            Email = usuario.Email,
            EmailConfirmed = true
        };

        IdentityResult result = await _userManager.CreateAsync(identityUser, usuario.Senha);
        if(result.Succeeded)
        {
            //Operação geralmente usada após confirmação por email
            await _userManager.SetLockoutEnabledAsync(identityUser, false);
        }

        UsuarioCadastroResponse usuarioCadastroResponse = new UsuarioCadastroResponse(result.Succeeded);
        if(!result.Succeeded && result.Errors.Count() > 0)
        {
            usuarioCadastroResponse.AdicionarErrors(result.Errors.Select(r => r.Description));
        }

        return usuarioCadastroResponse;
    }

    public async Task<UsuarioLoginResponse> LoginAsync(UsuarioLoginRequest usuario)
    {
        SignInResult result = await _signInManager.PasswordSignInAsync(usuario.Login, usuario.Senha, false, true);
        if(result.Succeeded)
            return await GerarToken(usuario.Login);

        UsuarioLoginResponse usuarioLoginResponse = new UsuarioLoginResponse(result.Succeeded);
        if(!result.Succeeded)
        {
            if(result.IsLockedOut)
                usuarioLoginResponse.AdicionarError("Esta conta está bloqueada.");
            else if(result.IsNotAllowed)
                usuarioLoginResponse.AdicionarError("Esta conta não tem permissão para fazer login.");
            else if(result.RequiresTwoFactor)
                usuarioLoginResponse.AdicionarError("É preciso confirmar o Login no seu segundo fator.");
            else
                usuarioLoginResponse.AdicionarError("Usuário ou senha incorretos");
        }  

        return usuarioLoginResponse;
    } 
    

    private async Task<UsuarioLoginResponse> GerarToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        IList<Claim> tokenClaims = await ObterClaimERole(user);

        DateTime dateExpiration =  DateTime.Now.AddSeconds(_jwtOptions.Expiration);

        JwtSecurityToken jwt = new JwtSecurityToken
        (
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: tokenClaims,
            notBefore: DateTime.Now,
            expires: dateExpiration,
            signingCredentials: _jwtOptions.SigningCredentials
        );

        string token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new UsuarioLoginResponse
        (
            success: true,
            token: token,
            expedicaoToken: dateExpiration
        );
    }


    private async Task<IList<Claim>> ObterClaimERole(IdentityUser user)
    {
        IList<Claim> claims = await _userManager.GetClaimsAsync(user);
        IList<string> roles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

        foreach(var role in roles)
        {
            claims.Add(new Claim("role", role));
        }

        return claims;
    }
}