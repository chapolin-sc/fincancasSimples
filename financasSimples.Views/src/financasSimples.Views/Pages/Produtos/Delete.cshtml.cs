using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace financasSimples.Views.Pages.Produtos;

public class DeleteModel : PageModel
{
    public readonly string ENDPOINT = "http://localhost:9800/api/Produtos/";
    public readonly HttpClient httpClient = null;

    // Fazer por injeção de dependência
    public DeleteModel()
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(ENDPOINT);
    }
    
    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(ENDPOINT + id);

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