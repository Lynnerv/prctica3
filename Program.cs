using Microsoft.EntityFrameworkCore; // EF Core
using prctica3.Data; // DbContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Usar SQLite y almacenar DB en carpeta /data (Render)
builder.Services.AddDbContext<FeedbackDbContext>(options =>
    options.UseSqlite("Data Source=/data/feedback.db"));

// Servicio HTTP para JSONPlaceholder
builder.Services.AddHttpClient<prctica3.Services.PostService>();

var app = builder.Build();

// Configurar middleware según entorno
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Rutas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Noticias}/{action=Index}/{id?}");

// Ejecutar migración automáticamente en producción
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FeedbackDbContext>();
    db.Database.Migrate();
}

app.Run();
