using System.Text;
using Gestao_de_Colaboradores_e_Unidades.Context;
using Gestao_de_Colaboradores_e_Unidades.Repositories.Interfaces;
using Gestao_de_Colaboradores_e_Unidades.Repository;
using Gestao_de_Colaboradores_e_Unidades.Services;
using Gestao_de_Colaboradores_e_Unidades.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


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

        var jwtSettingsSection = Configuration.GetSection("JwtSettings");
        services.Configure<JwtSettings>(jwtSettingsSection);

        var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

        if (jwtSettings == null)
        {
            throw new InvalidOperationException("JwtSettings configuration is missing");
        }

        var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

        services.AddSingleton(jwtSettings);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience
            };
        });

        services.AddControllers();
        services.AddSwaggerGen();
        services.AddScoped<TokenService>();
    }

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