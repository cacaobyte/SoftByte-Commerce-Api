using CommerceCore.DAL.Commerce;
using CommerceCore.EL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// Registrar el contexto de base de datos
builder.Services.AddDbContext<SoftByte>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("cadenaSQL"))
);

// Configurar CORS para aceptar solicitudes de cualquier origen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// 📌 Agregar el middleware para capturar `HttpResponseException`
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (HttpResponseException ex)
    {
        context.Response.StatusCode = ex.StatusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync($"{{\"error\": \"{ex.Message}\"}}");
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync($"{{\"error\": \"Error interno del servidor: {ex.Message}\"}}");
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Aplicar CORS con la política "AllowAll"
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
