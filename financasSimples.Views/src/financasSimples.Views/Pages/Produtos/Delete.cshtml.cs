using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace financasSimples.Views.Pages.Produtos;

public class DeleteModel : PageModel
{
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
            TempData["MensagemDeInteracaoComBanco"] = "Exclusão realizada com sucesso";

            if(!response.IsSuccessStatusCode)
            {
                TempData["MensagemDeInteracaoComBanco"] = "";
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