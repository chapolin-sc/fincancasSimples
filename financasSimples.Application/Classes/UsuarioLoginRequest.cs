using System.ComponentModel.DataAnnotations;

namespace financasSimples.Application.Classes;

public class UsuarioLoginRequest
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [MaxLength(256)]
    [DataType(DataType.EmailAddress, ErrorMessage = "{0} em padrão de Email esperado.")]
    [Display(Name = "Login")]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DataType(DataType.Password)]
    [StringLength(30, ErrorMessage = "A {0} deve ter entre {2} a {1} caracteres!", MinimumLength = 6)]
    [Display(Name = "Senha")]
    public string Senha { get; set; }

    public bool Lembrar { get; set; } = false;
} 