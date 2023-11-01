using System.Net.Http.Headers;
using financasSimples.Views.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace financasSimples.Views.Pages.Produtos;

public class EditModel : PageModel
{
    public IConfiguration _configuration { get; }

    [BindProperty]
    public ProdutosViewModel? _produtosDto { get; set; }

    [BindProperty(Name="_produtosDto.ImagemProdutoDto")]
    public IFormFile? imagem { get; set; }



    public EditModel(IConfiguration configuration)
    {
        _configuration = configuration;
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("ApiString"));
    }

    

    public readonly HttpClient httpClient = null;


    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await httpClient.GetAsync("Produtos/" + id.ToString());
            string produtoString = await response.Content.ReadAsStringAsync();
            _produtosDto = JsonConvert.DeserializeObject<ProdutosViewModel>(produtoString);

            return Page();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
         //Necessário porque o campo ImagemProdutoDto na view é um tipo file e o DataAnnotations não funciona. 
        if(imagem != null && imagem.FileName.Length > 100)
        {
            ModelState.AddModelError("_produtosDto.ImagemProdutoDto", "O nome da imagem não pode ter mais de 100 caracteres!");
        }

        if(ModelState.IsValid)
        {
            try
            {
                using (MultipartFormDataContent multipartForm = new MultipartFormDataContent())
                {
                    string endPointIntermediario = "";

                    multipartForm.Add(new StringContent(_produtosDto.IdProdutoDto.ToString()), "IdProdutoDto");
                    multipartForm.Add(new StringContent(_produtosDto.ImagemProdutoNomeDto != null ? _produtosDto.ImagemProdutoNomeDto : ""), "ImagemProdutoNomeDto");
                    multipartForm.Add(new StringContent(_produtosDto.NomeProdutoDto), "NomeProdutoDto");
                    multipartForm.Add(new StringContent(_produtosDto.VolumeProdutoDto), "VolumeProdutoDto");
                    multipartForm.Add(new StringContent(_produtosDto.MarcaProdutoDto != null ? _produtosDto.MarcaProdutoDto : ""), "MarcaProdutoDto");
                    multipartForm.Add(new StringContent(_produtosDto.DescricaoProdutoDto != null ? _produtosDto.DescricaoProdutoDto : ""), "DescricaoProdutoDto");
                    
                    if(imagem != null && imagem.Length > 0)
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        await imagem.CopyToAsync(memoryStream);
                        ByteArrayContent byteArrayContent = new ByteArrayContent(memoryStream.ToArray());
                        byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("image/*");

                        multipartForm.Add(byteArrayContent, name: "imagem", fileName: imagem.FileName);
                        endPointIntermediario = "UpComImagem/";
                    }

                    HttpResponseMessage response = await httpClient.PutAsync("Produtos/" + endPointIntermediario + _produtosDto.IdProdutoDto.ToString(), multipartForm);
                    TempData["MensagemDeInteracaoComBanco"] = "Alteração realizada com sucesso";

                    if(!response.IsSuccessStatusCode)
                    {
                        TempData["MensagemDeInteracaoComBanco"] = "";
                        ModelState.AddModelError(null, "Erro ao tentar salvar um produto no banco");
                    }
                    
                    return RedirectToPage("/Produtos/Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Problemas ao atualizar o produto");
            }
        }
        return Page();
    }

} 