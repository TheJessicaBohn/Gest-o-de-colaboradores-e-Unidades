using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gestao_de_Colaboradores_e_Unidades.Models;

[Table("Unidades")]
[Index(nameof(UnidadeCodigo), IsUnique = true)]
public class UnidadesModel
{
    [Key]
    public string? UnidadeId { get; set; }

    [Required(ErrorMessage = "O código é obrigatório")]
    [StringLength(20, ErrorMessage = "O tamanho máximo é 20 caracteres")]
    public string UnidadeCodigo { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "O tamanho máximo é 50 caracteres")]
    public string UnidadeNome { get; set; } = string.Empty;

    [NotMapped]
    public List<ColaboradoresModel> Colaboradores { get; set; } = new();
}
