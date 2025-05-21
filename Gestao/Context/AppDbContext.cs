using Gestao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Gestao.Context;

public class AppDbContext : IdentityDbContext<UsuariosModel, IdentityRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ColaboradoresModel> Colaboradores { get; set; }
    public DbSet<UnidadesModel> Unidades { get; set; }
    public DbSet<UsuariosModel> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
   
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ColaboradoresModel>()
            .Property(c => c.ColaboradorId)
            .IsRequired();

        modelBuilder.Entity<UnidadesModel>()
            .Property(u => u.UnidadeId)
            .IsRequired();

        modelBuilder.Entity<UsuariosModel>()
            .Property(u => u.Id)
            .IsRequired();
    }
}