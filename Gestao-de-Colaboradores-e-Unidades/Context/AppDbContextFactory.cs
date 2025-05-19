using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Gestao_de_Colaboradores_e_Unidades.Context;
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseMySql(
            "Server=localhost;Port=3306;Database=meubanco;User=root;Password=;",
            new MySqlServerVersion(new Version(8, 0, 32)),
            mySqlOptions =>
            {
                mySqlOptions.EnableRetryOnFailure();
            });

        return new AppDbContext(optionsBuilder.Options);
    }
}
