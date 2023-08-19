using FoodOrdering.Application.Repositories;
using FoodOrdering.Persistence.Context;
using FoodOrdering.Persistence.Repositories;
using FoodOredering.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EntityContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"),
            b => b.MigrationsAssembly(typeof(EntityContext).Assembly.FullName)));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Default Password settings.
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
}).AddEntityFrameworkStores<EntityContext>()
.AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
// Add services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericService<>));
builder.Services.AddScoped(typeof(IServiceCategory), typeof(ServiceCategory));
builder.Services.AddScoped(typeof(IServiceMenu), typeof(ServiceMenu));
builder.Services.AddScoped(typeof(IServiceCart), typeof(ServiceCart));
builder.Services.AddScoped(typeof(IServiceShowMenu), typeof(ServiceShowMenu));
builder.Services.AddScoped(typeof(IServiceOrder), typeof(ServiceOrder));
builder.Services.AddScoped(typeof(IServiceFinishedOrder), typeof(ServiceFinishedOrder));
        

var app = builder.Build();

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
