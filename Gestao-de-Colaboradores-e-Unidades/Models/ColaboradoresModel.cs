using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao_de_Colaboradores_e_Unidades.Models;

[Table("Colaboradores")]
public class ColaboradoresModel
{
    [Key]
    public string? ColaboradorId { get; set; }

    [Required(ErrorMessage ="O nome do colaborador é obrigatório")]
    [Display(Name = "Nome do Colaborador")]
    [StringLength(100, ErrorMessage = "O tamanho máximo é 100 caracteres")]
    public string ColaboradorNome { get; set; } = string.Empty;

    [ForeignKey("Unidades")]
    [Required(ErrorMessage = "O colaborador deve ser vinculado a uma unidade!")]
    public string? UnidadeId { get; set; }

    [NotMapped]
    public virtual UnidadesModel Unidades { get; set; } = new();

}
