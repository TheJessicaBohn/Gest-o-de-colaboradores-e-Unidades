using Gestao.Context;
using Gestao.Models;
using Gestao.Repositories.Interfaces;

namespace Gestao.Repository;

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

    public UnidadesModel BuscaUnidadesPorId(int id)
    {
        var unidade = _context.Unidades.FirstOrDefault(u => u.UnidadeId == id);
        if (unidade == null) throw new Exception("Unidade não encontada");

        return unidade;
    }

     public void AtualizarUnidade(UnidadesModel unidade)
    {
        var existente = BuscaUnidadesPorId(unidade.UnidadeId);
        if (existente == null) return;

        existente.UnidadeNome = unidade.UnidadeNome;
        existente.UnidadeCodigo = unidade.UnidadeCodigo;
        existente.EstaUnidadeAtiva = unidade.EstaUnidadeAtiva;
    }

    public void InativarUnidade(int id)
    {
        var unidade = BuscaUnidadesPorId(id);
        if (unidade != null)
            unidade.EstaUnidadeAtiva = false;
    }
}