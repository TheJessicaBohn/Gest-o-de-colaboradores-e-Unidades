using Gestao_de_Colaboradores_e_Unidades.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestao_de_Colaboradores_e_Unidades.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ColaboradoresModel> Colaboradores { get; set; }
    public DbSet<UnidadesModel> Unidades { get; set; }
    public DbSet<UsuariosModel> Usuarios { get; set; }


}