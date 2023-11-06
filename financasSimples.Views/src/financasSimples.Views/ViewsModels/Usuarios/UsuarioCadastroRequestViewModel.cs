using System.ComponentModel.DataAnnotations;

namespace financasSimples.Views.ViewsModels.Usuarios;

public class UsuarioCadastroRequestViewModel 
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MaxLength(150)]
    [Display(Name = "Nome")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Necessário ter formato de Email")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DataType(DataType.Password)]
    [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 6)]
    [Display(Name = "Senha")]
    public string Senha { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DataType(DataType.Password)]
    [Compare("Senha", ErrorMessage = "O campo {0} precisa ser igual ao campo senha.")]
    [Display(Name = "Confirma Senha")]
    public string ConfirmaSenha { get; set; }
}