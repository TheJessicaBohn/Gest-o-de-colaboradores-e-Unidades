using Gestao.Context;
using Gestao.Models;
using Gestao.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Repository;

public class UnidadesRepository : IUnidadesRepository
{
    private readonly AppDbContext _context;

    public UnidadesRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<UnidadesModel> Unidades => _context.Unidades.ToList();

    public void CriarUnidade(UnidadesModel unidade)
    {
        _context.Unidades.Add(unidade);
        _context.SaveChanges();
    }

    public UnidadesModel BuscaUnidadesPorId(int id)
    {
        return _context.Unidades.FirstOrDefault(u => u.UnidadeId == id);
    }

    public void AtualizarUnidade(UnidadesModel unidade)
    {
        var existente = _context.Unidades.FirstOrDefault(u => u.UnidadeId == unidade.UnidadeId);
        if (existente != null)
        {
            existente.UnidadeCodigo = unidade.UnidadeCodigo;
            existente.UnidadeNome = unidade.UnidadeNome;
            existente.EstaUnidadeAtiva = unidade.EstaUnidadeAtiva;

            _context.SaveChanges();
        }
    }

    public void InativarUnidade(int id)
    {
        var unidade = _context.Unidades.FirstOrDefault(u => u.UnidadeId == id);
        if (unidade != null)
        {
            unidade.EstaUnidadeAtiva = false;
            _context.SaveChanges();
        }
    }

    public async Task<IEnumerable<UnidadesModel>> BuscaTodasUnidades()
    {
        return await _context.Unidades
            .OrderBy(u => u.UnidadeCodigo)
            .ToListAsync();
    }
}
