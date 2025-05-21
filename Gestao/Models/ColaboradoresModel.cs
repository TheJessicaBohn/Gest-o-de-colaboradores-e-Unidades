using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao.Models;

[Table("Colaboradores")]
public class ColaboradoresModel
{
    [Key]
    public int ColaboradorId { get; set; }

    [Required(ErrorMessage = "O nome do colaborador é obrigatório")]
    [Display(Name = "Nome do Colaborador")]
    [StringLength(100, ErrorMessage = "O tamanho máximo é 100 caracteres")]
    public string ColaboradorNome { get; set; } = string.Empty;
    public virtual string UnidadeCodigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "O colaborador deve ser vinculado à uma unidade!")]
    [ForeignKey("UnidadeId")]
    public required int UnidadeId { get; set; }
}