namespace Gestao_de_Colaboradores_e_Unidades.Models;

    public class UnidadesModel
    {

        public string? UnidadeId { get; set; }
        public string? UnidadeCodigo { get; set; }
        public string? UnidadeNome { get; set; }

        public List<ColaboradoresModel> Colaboradores { get; set; }
    }
