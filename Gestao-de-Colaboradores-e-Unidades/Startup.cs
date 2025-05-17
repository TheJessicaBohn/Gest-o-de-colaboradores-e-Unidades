using Gestao_de_Colaboradores_e_Unidades.Context;
using Gestao_de_Colaboradores_e_Unidades.Repositories.Interfaces;
using Gestao_de_Colaboradores_e_Unidades.Repository;
using Microsoft.EntityFrameworkCore;

namespace Gestao_de_Colaboradores_e_Unidades;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(9, 0, 0))
            ));

        services.AddTransient<IColaboradoresRepository, ColaboradoresRepository>();
        services.AddTransient<IUnidadesRepository, UnidadesRepository>();
        services.AddTransient<IUsuariosRepository, UsuariosRepository>();

        services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}