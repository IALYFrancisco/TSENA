using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Explicitement ajouter les fichiers de configuration en fonction de l'environnement
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory()) // Assurer que le répertoire de base est correctement défini
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Ajouter le DbContext avec la chaîne de connexion
builder.Services.AddDbContext<ShopManagementeContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Ajouter les services
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurer le pipeline de requêtes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ShopManagement}/{action=Index}/{id?}");

app.Run();
