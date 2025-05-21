using Gestao.Context;
using Gestao.Models;
using Gestao.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Repository;

public class ColaboradoresRepository : IColaboradoresRepository
{
    private readonly AppDbContext _context;

    public ColaboradoresRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ColaboradoresModel>> BuscarTodosOsColaboradores()
    {
        return await _context.Colaboradores.ToListAsync();
    }

     public async Task<bool> CriarColaborador(ColaboradoresModel colaborador)
    {
        await _context.Colaboradores.AddAsync(colaborador);
        return await _context.SaveChangesAsync() > 0;
    }

    public void AtualizarColaborador(ColaboradoresModel colaborador)
    {
        var existente = _context.Colaboradores.FirstOrDefault(c => c.ColaboradorId == colaborador.ColaboradorId);

        if (existente == null)
            throw new Exception("Colaborador não encontrado.");

        existente.ColaboradorNome = colaborador.ColaboradorNome;
        existente.UnidadeId = colaborador.UnidadeId;

        _context.SaveChanges();
    }

    public ColaboradoresModel BuscaColaboradorPorId(int id)
    {
        var colaborador = _context.Colaboradores.FirstOrDefault(c => c.ColaboradorId == id);

        if (colaborador == null)
            throw new Exception("Colaborador não encontrado");

        return colaborador;
    }

    public void RemoverColaborador(int id)
    {
        var colaborador = _context.Colaboradores.FirstOrDefault(c => c.ColaboradorId == id);
        if (colaborador != null)
        {
            _context.Colaboradores.Remove(colaborador);
            _context.SaveChanges();
        }
    }
}
