using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using financasSimples.Views.ViewsModels;
using System.Net;

namespace financasSimples.Views.Pages.Produtos;

public class CreateModel : PageModel
{
    public IConfiguration _configuration;

    [BindProperty]
    public ProdutosViewModel? Produtos { get; set; }

    [BindProperty(Name="Produtos.ImagemProdutoDto")]
    public IFormFile? imagem { get; set; }
    public readonly HttpClient httpClient = null;



    // Fazer por injeção de dependência
    public CreateModel(IConfiguration configuration)
    {
        _configuration = configuration;
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("ApiString"));
    }

    
    
    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("IdProdutoDto");

        //Necessário porque o campo ImagemProdutoDto na view é um tipo file e o DataAnnotations não funciona. 
        if(imagem != null && imagem.FileName.Length > 100)
        {
            ModelState.AddModelError("Produtos.ImagemProdutoDto", "O nome da imagem não pode ter mais de 100 caracteres!");
        }
        if(ModelState.IsValid)
        {
            try
            {
                using (MultipartFormDataContent multipartForm = new MultipartFormDataContent()){

                    string endPointIntermediario = "";

                    if(imagem != null && imagem.Length > 0)
                    {
                        using MemoryStream memoryStream = new MemoryStream();
                        await imagem.CopyToAsync(memoryStream);
                        ByteArrayContent byteArrayContenFile = new ByteArrayContent(memoryStream.ToArray());
                        byteArrayContenFile.Headers.ContentType = new MediaTypeHeaderValue("image/*");
                        
                        multipartForm.Add(byteArrayContenFile, name: "imagem", fileName: imagem.FileName);
                        endPointIntermediario = "CreateComImagem/";
                    }
                   
                    multipartForm.Add(new StringContent(Produtos.NomeProdutoDto), "NomeProdutoDto");
                    multipartForm.Add(new StringContent(Produtos.VolumeProdutoDto), "VolumeProdutoDto");
                    multipartForm.Add(new StringContent(Produtos.MarcaProdutoDto != null ? Produtos.MarcaProdutoDto : ""), "MarcaProdutoDto");
                    multipartForm.Add(new StringContent(Produtos.DescricaoProdutoDto != null ? Produtos.DescricaoProdutoDto : ""), "DescricaoProdutoDto");

                    HttpResponseMessage response = await httpClient.PostAsync("Produtos/" + endPointIntermediario, multipartForm);
                    TempData["MensagemDeInteracaoComBanco"] = "Cadastro realizado com sucesso";

                    if(!response.IsSuccessStatusCode)
                    {
                        TempData["MensagemDeInteracaoComBanco"] = "";
                        
                        if(response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            ModelState.AddModelError(null, "Sem permição para realizar cadastros.");
                        }

                        ModelState.AddModelError(null, "Erro ao tentar cadastrar o produto");
                    }
                    
                    return RedirectToPage("/Produtos/Index");
                }
            }
            catch (Exception ex)
            {
                return Page();
            }
            
        }
        return Page();
    }
}