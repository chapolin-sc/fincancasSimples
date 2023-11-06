using System.Net;
using System.Net.Http.Headers;
using financasSimples.Views.ViewsModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace financasSimples.Views.Pages.Produtos;

public class IndexModel : PageModel
{
    private IConfiguration _configuration;
    public IList<ProdutosViewModel> _produto { get; set; }

    public readonly HttpClient httpClient = null;
    public readonly IHttpContextAccessor _httpContext;
    



    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("ApiString"));    
    }




    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if(Request.Cookies.ContainsKey("JWTCookieFinancasUsuario"))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Request.Cookies["JWTCookieFinancasUsuario"]);
            }  
            
            HttpResponseMessage response = await httpClient.GetAsync("Produtos");
            if(!response.IsSuccessStatusCode)
            {
                if(response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    TempData["StatusPermissao"] = HttpStatusCode.Unauthorized;
                    return RedirectToPage("../Usuarios/Login");
                }
                
                ModelState.AddModelError(string.Empty, "Erro ao encontrar produtos cadastrados");
                return Page();
            }

            string produtos = await response.Content.ReadAsStringAsync();
            _produto = JsonConvert.DeserializeObject<List<ProdutosViewModel>>(produtos);

            //Url atual do sistema.
            ViewData["caminhoAtual"] = Request.GetDisplayUrl();    

            return Page();
        }
        catch (Exception ex){
            throw ex;
        }
    }
}