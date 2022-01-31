using InformeClimaPortal.DataContext;
using InformeClimaPortal.Services.InformeClima;
using InformeClimaPortal.Services.OpenWeatherMap;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<InformeClimaDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<InformeClimaDbContext>(options => options.UseInMemoryDatabase("InformeClima"));

builder.Services.AddTransient<IOpenWeatherMapService, OpenWeatherMapService>();
builder.Services.AddTransient<IInformeService, InformeService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Informes}/{action=Index}/{id?}");

app.Run();
