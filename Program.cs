using Microsoft.EntityFrameworkCore; // Necesario para EF Core
using prctica3.Data; // Importa tu DbContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Agrega el DbContext para guardar feedback usando SQLite
builder.Services.AddDbContext<FeedbackDbContext>(options =>
    options.UseSqlite("Data Source=feedback.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configura las rutas del controlador MVC por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
