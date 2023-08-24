using FoodOrdering.Application.Repositories;
using FoodOrdering.Persistence;
using FoodOrdering.Persistence.Context;
using FoodOrdering.Persistence.Repositories;
using FoodOredering.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.ConfigurePersistence(builder.Configuration);

var app = builder.Build();
var serviceScope = app.Services.CreateScope();
var dataContext = serviceScope.ServiceProvider.GetService<EntityContext>();
dataContext?.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
