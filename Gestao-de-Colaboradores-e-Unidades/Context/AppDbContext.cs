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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<ColaboradoresModel>()
            .Property(c => c.ColaboradorId)
            .IsRequired();

        modelBuilder.Entity<UnidadesModel>()
            .Property(u => u.UnidadeId)
            .IsRequired();

        modelBuilder.Entity<UsuariosModel>()
            .Property(c => c.UsuarioId)
            .IsRequired();
        
        var usuarioHostId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        
        modelBuilder.Entity<UsuariosModel>().HasData(new UsuariosModel
        {
            UsuarioId = usuarioHostId,
            UsuarioNome = "Host",
            UsuarioEmail = "host@host.com",
            UsuarioSenha = "123qwe",
            UsuarioStatus = true,
        });
    }
}