using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using financasSimples.Views.ViewsModels.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace financasSimples.Views.Pages.Usuarios;

public class CreateModel : PageModel
{
    [BindProperty]
    public UsuarioCadastroRequestViewModel _Request { get; set; }
    public IConfiguration _configuration;
    public readonly HttpClient _httpClient;




    public CreateModel(IConfiguration configuration) 
    {
        _configuration = configuration;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("ApiString"));
    }



    public async Task<IActionResult> OnPostAsync()
    {
        if(ModelState.IsValid)
        {
            try
            {
                using (MultipartFormDataContent multipartForm = new MultipartFormDataContent()){

                    multipartForm.Add(new StringContent(_Request.Nome), "Nome");
                    multipartForm.Add(new StringContent(_Request.Email), "Email");
                    multipartForm.Add(new StringContent(_Request.Senha), "Senha");
                    multipartForm.Add(new StringContent(_Request.ConfirmaSenha), "ConfirmaSenha");

                    HttpResponseMessage httpResponse = await _httpClient.PostAsync("Usuario/Cadastro", multipartForm);

                    if(!httpResponse.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError(string.Empty, $"Erro ao tentar cadastrar({httpResponse.EnsureSuccessStatusCode()})");
                        return Page();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }

        TempData["UsuarioCadastro"] = true;
        return RedirectToPage("/Index");
    }
}