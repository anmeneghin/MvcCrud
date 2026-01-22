using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using MvcCrud.Data;
using Microsoft.EntityFrameworkCore.InMemory;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços MVC (controllers + views).
builder.Services.AddControllersWithViews();

// Registra o DbContext do Entity Framework usando um banco InMemory (bom para estudos, nao precisa do banco).
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("StudyCrudDb"));

var app = builder.Build();

// Configura a cultura padrão para pt-BR.
// Isso faz com que o model binding aceite vírgula como separador decimal (ex: 2,50).
var defaultCulture = new CultureInfo("pt-BR");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(defaultCulture),
    SupportedCultures = new[] { defaultCulture },
    SupportedUICultures = new[] { defaultCulture }
};
CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;
app.UseRequestLocalization(localizationOptions);

// Seed: popula alguns produtos iniciais para testar a aplicação.
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!ctx.Products.Any())
    {
        ctx.Products.AddRange(
            new MvcCrud.Models.Product { Name = "Caneta", Price = 2.5m, Description = "Caneta azul" },
            new MvcCrud.Models.Product { Name = "Caderno", Price = 12.0m, Description = "Caderno A4" },
            new MvcCrud.Models.Product { Name = "Borracha", Price = 1.2m, Description = "Borracha branca" }
        );
        ctx.SaveChanges();
    }
}

// Pipeline HTTP básico (middlewares)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Define rota padrão para controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();