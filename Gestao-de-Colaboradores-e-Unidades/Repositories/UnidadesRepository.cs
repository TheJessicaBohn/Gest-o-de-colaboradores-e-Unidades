using Gestao_de_Colaboradores_e_Unidades.Context;
using Gestao_de_Colaboradores_e_Unidades.Models;
using Gestao_de_Colaboradores_e_Unidades.Repositories.Interfaces;

namespace Gestao_de_Colaboradores_e_Unidades.Repository;

public class UnidadesRepository : IUnidadesRepository
{
    private readonly AppDbContext _context;
    public UnidadesRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<UnidadesModel> Unidades => _context.Unidades;

    public void CriarUnidade(UnidadesModel unidade)
    {
        _context.Unidades.Add(unidade);
        _context.SaveChanges();
    }

    public UnidadesModel GetUnidadesById(string id)
    {
        var unidade = _context.Unidades.FirstOrDefault(u => u.UnidadeId == id);
        if (unidade == null) throw new Exception("Unidade não encontada");

        return unidade;
    }
}