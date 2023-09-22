using financasSimples.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace financasSimples.Views.Pages.Produtos;

public class DetailsModel : PageModel
{

    public ProdutosDto? _produtosDto;

    public readonly string ENDPOINT = "http://localhost:9800/api/Produtos/";
    public readonly HttpClient httpClient = null;

    public DetailsModel()
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(ENDPOINT);
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        HttpResponseMessage response = await httpClient.GetAsync(ENDPOINT + id);
        if(!response.IsSuccessStatusCode)
        {
            return NotFound();
        }

        string produtos = await response.Content.ReadAsStringAsync();
        _produtosDto = JsonConvert.DeserializeObject<ProdutosDto>(produtos);
        return Page();
    }
}