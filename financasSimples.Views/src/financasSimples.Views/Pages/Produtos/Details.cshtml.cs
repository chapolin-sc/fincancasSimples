using financasSimples.Views.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace financasSimples.Views.Pages.Produtos;

public class DetailsModel : PageModel
{
    /*//public readonly string ENDPOINT = "http://localhost:9800/api/Produtos/";
    public readonly string ENDPOINT = "https://zt0ailq0y9.execute-api.us-east-1.amazonaws.com/Prod/api/Produtos/";*/

    public IConfiguration _configuration;
    public ProdutosViewModel? _produtosDto;
    public readonly HttpClient httpClient = null;



    public DetailsModel(IConfiguration configuration)
    {
        _configuration = configuration;
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("ApiString"));
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        HttpResponseMessage response = await httpClient.GetAsync(id.ToString());
        if(!response.IsSuccessStatusCode)
        {
            return NotFound();
        }

        string produtos = await response.Content.ReadAsStringAsync();
        _produtosDto = JsonConvert.DeserializeObject<ProdutosViewModel>(produtos);
        return Page();
    }
}