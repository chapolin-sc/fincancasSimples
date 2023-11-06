using System.ComponentModel.DataAnnotations;

namespace financasSimples.Views.ViewsModels.Usuarios;

public class UsuarioLoginRequestViewModel
{
    [Required(ErrorMessage = "É necessário informar o {0}.")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Você precisa informar um email válido.")]
    public string Login { get; set; }

    [Required(ErrorMessage = "É necesário informar a {0}.")]
    [DataType(DataType.Password)]
    [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 6)]
    public string Senha { get; set; }
}