using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace financasSimples.Views.Pages.Produtos;

public class DeleteModel : PageModel
{
    /*/public readonly string ENDPOINT = "http://localhost:9800/api/Produtos/";
    public readonly string ENDPOINT = "https://zt0ailq0y9.execute-api.us-east-1.amazonaws.com/Prod/api/Produtos/";*/

    public IConfiguration _configuration;
    public readonly HttpClient httpClient = null;

    

    // Fazer por injeção de dependência
    public DeleteModel(IConfiguration configuration)
    {
        _configuration = configuration;
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("ApiString"));
    }
    
    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(id.ToString());

            if(!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(null, "Erro ao tentar cadastrar o produto");
            }
            
            return RedirectToPage("/Produtos/Index");
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}