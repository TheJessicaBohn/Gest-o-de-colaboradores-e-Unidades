namespace Gestao.ViewModels;

public class LoginViewModel
{
    public required string NomeUsuario { get; set; }
    public string Senha { get; set; } = String.Empty;
    public string RetornaURL { get; set; } = String.Empty;
}