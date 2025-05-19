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

    public IEnumerable<ColaboradoresModel> Colaboradores => _context.Colaboradores.Include(u => u.Unidade);

    public void CriarColaborador(ColaboradoresModel colaborador)
    {
        _context.Colaboradores.Add(colaborador);
        _context.SaveChanges();
    }


    public void AtualizarColaborador(ColaboradoresModel colaborador)
    {
        var colaboradorExistente = _context.Colaboradores.FirstOrDefault(c => c.ColaboradorId == colaborador.ColaboradorId);

        if (colaboradorExistente == null)
            throw new Exception("Colaborador não encontrado.");

        _context.SaveChanges();
    }

    public ColaboradoresModel BuscaColaboradorPorId(int id)
    {
        var colaborador = _context.Colaboradores.FirstOrDefault(u => u.UnidadeId == id);
        if (colaborador == null) throw new Exception("Colaborador não encontado");

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