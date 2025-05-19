using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao.Models;

[Table("Usuarios")]
public class UsuariosModel
{
    [Key]
    public Guid UsuarioId { get; set; }

    [Required(ErrorMessage ="O nome de usuário é obrigatório")]
    [Display(Name = "Nome do Usuário")]
    [StringLength(100, ErrorMessage = "O tamanho máximo é 100 caracteres")]
    public string UsuarioNome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [StringLength(50, ErrorMessage = "O tamanho máximo é 50 caracteres")]
    [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
    public string UsuarioEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória")]
    [MinLength(8, ErrorMessage = "O tamanho mínimo da senha é 8 caracteres")]
    [MaxLength(50, ErrorMessage = "O tamanho máximo é 50 caracteres")]
    public string UsuarioSenha { get; set; } = string.Empty;
    public bool UsuarioStatus { get; set; }
}
