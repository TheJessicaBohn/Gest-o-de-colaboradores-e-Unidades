using System.ComponentModel.DataAnnotations;

namespace Gestao.ViewModels;

public class LoginViewModel
{
    [Display(Name = "E-mail")]
    public required string NomeUsuario { get; set; } = String.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public required string Senha { get; set; } = String.Empty;
    public string RetornaURL { get; set; } = String.Empty;
}