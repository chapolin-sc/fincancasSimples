using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace financasSimples.Views.Pages;

public class IndexModel : PageModel
{
    public IActionResult OnGet()
    {
        ViewData["UsuarioLogado"] = false;
        
        if(HttpContext.Request.Cookies.ContainsKey("JWTCookieFinancasUsuario"))
        {
            ViewData["UsuarioLogado"] = true;
        }

        return Page();
    }
}