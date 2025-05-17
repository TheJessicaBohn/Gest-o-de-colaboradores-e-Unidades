using Gestao_de_Colaboradores_e_Unidades.Context;
using Gestao_de_Colaboradores_e_Unidades.Models;
using Gestao_de_Colaboradores_e_Unidades.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gestao_de_Colaboradores_e_Unidades.Repository;

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

    public ColaboradoresModel GetColaboradoresById(string id)
    {
        var colaborador = _context.Colaboradores.FirstOrDefault(u => u.UnidadeId == id);
        if (colaborador == null) throw new Exception("Colaborador não encontado");

        return colaborador;
    }
}