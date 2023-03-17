using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using SisArrendamiento.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ArrendamientoWebContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Renta}/{action=Index}/{id?}");
    //pattern: "{controller=Renta}/{action=Detalles}/{id=1}");

IWebHostEnvironment env = app.Environment;
RotativaConfiguration.Setup(env.WebRootPath);
app.Run();
