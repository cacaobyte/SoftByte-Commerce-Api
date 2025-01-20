using CommerceCore.DAL.Commerce;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// Registrar el contexto de base de datos
builder.Services.AddDbContext<SoftByte>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("cadenaSQL"))
);

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
