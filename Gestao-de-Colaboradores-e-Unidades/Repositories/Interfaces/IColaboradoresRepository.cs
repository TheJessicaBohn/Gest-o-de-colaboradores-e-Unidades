using Gestao_de_Colaboradores_e_Unidades.Models;

namespace Gestao_de_Colaboradores_e_Unidades.Repositories.Interfaces;

public interface IColaboradoresRepository
{
    void CriarColaborador(ColaboradoresModel colaborador);
    IEnumerable<ColaboradoresModel> Colaboradores { get; }
    ColaboradoresModel GetColaboradoresById(string colaboradoesId);
}