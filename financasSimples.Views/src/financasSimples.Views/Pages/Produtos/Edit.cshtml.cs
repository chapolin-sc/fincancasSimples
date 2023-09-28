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

    

    //public readonly string ENDPOINT = "http://localhost:9800/api/Produtos/";
    //public string ENDPOINT = new string(_configuration.GetValue<string>("ApiString")); //"https://zt0ailq0y9.execute-api.us-east-1.amazonaws.com/Prod/api/Produtos/";
    public readonly HttpClient httpClient = null;


    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await httpClient.GetAsync(id.ToString());
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
                    
                    if(imagem !=  null && imagem.Length > 0)
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        await imagem.CopyToAsync(memoryStream);
                        ByteArrayContent byteArrayContent = new ByteArrayContent(memoryStream.ToArray());
                        byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("image/*");

                        multipartForm.Add(byteArrayContent, name: "imagem", fileName: imagem.FileName);
                        endPointIntermediario = "UpComImagem/";
                    }

                    multipartForm.Add(new StringContent(_produtosDto.IdProdutoDto.ToString()), "IdProdutoDto");
                    multipartForm.Add(new StringContent(_produtosDto.NomeProdutoDto), "NomeProdutoDto");
                    multipartForm.Add(new StringContent(_produtosDto.VolumeProdutoDto), "VolumeProdutoDto");
                    multipartForm.Add(new StringContent(_produtosDto.MarcaProdutoDto != null ? _produtosDto.MarcaProdutoDto : ""), "MarcaProdutoDto");
                    multipartForm.Add(new StringContent(_produtosDto.DescricaoProdutoDto != null ? _produtosDto.DescricaoProdutoDto : ""), "DescricaoProdutoDto");

                    HttpResponseMessage response = await httpClient.PutAsync(endPointIntermediario + _produtosDto.IdProdutoDto, multipartForm);

                    if(!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError(null, "Erro ao tentar salvar um produto no banco");
                    }
                    
                    return RedirectToPage("/Produtos/Index");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        return Page();
    }

} 