using System.Text;
using Gestao_de_Colaboradores_e_Unidades.Context;
using Gestao_de_Colaboradores_e_Unidades.Repositories;
using Gestao_de_Colaboradores_e_Unidades.Repositories.Interfaces;
using Gestao_de_Colaboradores_e_Unidades.Repository;
using Gestao_de_Colaboradores_e_Unidades.Services;
using Gestao_de_Colaboradores_e_Unidades.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
    ));


builder.Services.AddTransient<IColaboradoresRepository, ColaboradoresRepository>();
builder.Services.AddTransient<IUnidadesRepository, UnidadesRepository>();
builder.Services.AddTransient<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var jwtSettingsSection = configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);

var jwtSettings = jwtSettingsSection.Get<JwtSettings>() 
    ?? throw new InvalidOperationException("JwtSettings configuration is missing");

var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience
    };
});

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<TokenService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
