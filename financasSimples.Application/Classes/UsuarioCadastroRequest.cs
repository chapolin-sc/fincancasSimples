using System.ComponentModel.DataAnnotations;

namespace financasSimples.Application.Classes;

public class UsuarioCadastroRequest 
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(256, ErrorMessage = "Máximo de caractres excedido!")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DataType(DataType.EmailAddress)]
    [MaxLength(256, ErrorMessage = "Máximo de caractres excedido!")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(30, ErrorMessage = "A {0} deve ter entre {2} a {1} caracteres!", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Senha { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirma Senha")]
    [Compare("Senha", ErrorMessage = "A confirmação de senha não confere com a senha!")]
    public string ConfirmaSenha { get; set; }

}