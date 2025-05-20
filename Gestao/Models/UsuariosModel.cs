using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Gestao.Models;

[Table("Usuarios")]
public class UsuariosModel : IdentityUser
{
    public bool UsuarioStatus { get; set; }
}
