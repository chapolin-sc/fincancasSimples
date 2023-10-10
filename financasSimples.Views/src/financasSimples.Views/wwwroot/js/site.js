// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


/*** Adiciona a Modal para exclusão de Produtos ***/
function PreenchePartialExclusao(id, nome, caminho)
{
    if(document.getElementById("Item") && document.getElementById("Excluir"))
    {
        var item = document.getElementById("Item")
        var excluir = document.getElementById("Excluir")

        item.innerText = nome 
        excluir.href = caminho + id
    }
}
/*** Final de: Adiciona a Modal para exclusão de Produtos ***/

/*** Carrega a imagem no cadastro de Produtos ***/
function defineMiniaturaImagem(file)
{
    if(file.files[0] && document.getElementById("ImgCadProduto"))
    {
        var reader = new FileReader();
        reader.onload = function (e) {
            document.getElementById("ImgCadProduto").src = e.target.result
        };
        reader.readAsDataURL(file.files[0]);
    }
}
/*** Final de: Carrega a imagem no cadastro de Produtos ***/
