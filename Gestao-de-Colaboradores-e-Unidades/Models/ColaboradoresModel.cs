namespace Gestao_de_Colaboradores_e_Unidades.Models;

    public class ColaboradoresModel
    {

        public string? ColaboradorId { get; set; }
        public string? ColaboradorNome { get; set; }

        public string? UnidadeId { get; set; }
        public virtual UnidadesModel Unidade { get; set; }

    }
