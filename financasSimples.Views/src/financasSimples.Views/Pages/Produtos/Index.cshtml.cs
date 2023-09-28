using financasSimples.Views.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace financasSimples.Views.Pages.Produtos;

public class IndexModel : PageModel
{
    private IConfiguration _configuration;
    public IList<ProdutosViewModel> _produto { get; set; }

    /*//public readonly string ENDPOINT = "http://localhost:9800/api/Produtos/";
    public readonly string ENDPOINT = "https://zt0ailq0y9.execute-api.us-east-1.amazonaws.com/Prod/api/Produtos/";*/


    public readonly HttpClient httpClient = null;

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
             HttpResponseMessage response = await httpClient.GetAsync("");
            if(!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Erro ao encontrar produtos cadastrados");
                return Page();
            }

            string produtos = await response.Content.ReadAsStringAsync();
            _produto = JsonConvert.DeserializeObject<List<ProdutosViewModel>>(produtos);

            return Page();
        }
        catch (Exception ex){
            throw ex;
        }
    }
}