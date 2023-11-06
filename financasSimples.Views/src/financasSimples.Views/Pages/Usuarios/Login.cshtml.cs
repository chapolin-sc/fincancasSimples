using System.Net.Http.Headers;
using System.Text.Json;
using financasSimples.Views.ViewsModels.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NuGet.Common;

namespace financasSimples.Views.Pages.Usuarios;

public class LoginModel : PageModel
{
    [BindProperty]
    public UsuarioLoginRequestViewModel _Request { get; set; }
    private IConfiguration _configuration;
    private HttpClient _httpClient;




    public LoginModel(IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("ApiString"));
    }




    [HttpPost]
    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            using(MultipartFormDataContent multipartForm = new MultipartFormDataContent())
            {
                multipartForm.Add(new StringContent(_Request.Login), "Login");
                multipartForm.Add(new StringContent(_Request.Senha), "Senha");

                HttpResponseMessage _Response = await _httpClient.PostAsync("Usuario/Login", multipartForm);

                string responseString = await _Response.Content.ReadAsStringAsync();
                UsuarioLoginResponseViewModel responseObject = JsonConvert.DeserializeObject<UsuarioLoginResponseViewModel>(responseString);

                if(!_Response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Problemas ao tentar fazer login");
                    return Page();
                }

                if(!responseObject.Success)
                {
                    foreach(var erro in responseObject.Error)
                    {
                        ModelState.AddModelError(string.Empty, erro);
                    }
                    return Page();
                }

                CookieOptions cookie =  new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddHours(3),
                    Path = "/",
                    IsEssential = true
                };

                HttpContext.Response.Cookies.Append("JWTCookieFinancasUsuario", responseObject.Token, cookie);
            }
            
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Problemas ao tentar fazer login");
            return Page();
        }

        return Page();
    }
}