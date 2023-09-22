using financasSimples.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace financasSimples.Views.Pages.Produtos;

public class IndexModel : PageModel
{
    public IList<ProdutosDto> _produto { get; set; }

    public readonly string ENDPOINT = "http://localhost:9800/api/Produtos/";
    public readonly HttpClient httpClient = null;

    public IndexModel(){
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(ENDPOINT);         
    }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
             HttpResponseMessage response = await httpClient.GetAsync(ENDPOINT);
            if(!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Erro ao encontrar produtos cadastrados");
                return Page();
            }

            string produtos = await response.Content.ReadAsStringAsync();
            _produto = JsonConvert.DeserializeObject<List<ProdutosDto>>(produtos);

            return Page();
        }
        catch (Exception ex){
            throw ex;
        }
    }
}