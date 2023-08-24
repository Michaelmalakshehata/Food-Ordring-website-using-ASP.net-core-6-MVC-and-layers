using FoodOrdering.Application.Repositories;
using FoodOrdering.Persistence.Context;
using FoodOrdering.Persistence.Repositories;
using FoodOredering.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Persistence
{
    public static class ServiceExtension
    {
        public static void ConfigurePersistence(this IServiceCollection Services,IConfiguration configuration)
        {
            Services.AddDbContext<EntityContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Connection"),
            b => b.MigrationsAssembly(typeof(EntityContext).Assembly.FullName)));
            Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
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
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericService<>));
            Services.AddScoped(typeof(IServiceCategory), typeof(ServiceCategory));
            Services.AddScoped(typeof(IServiceMenu), typeof(ServiceMenu));
            Services.AddScoped(typeof(IServiceCart), typeof(ServiceCart));
            Services.AddScoped(typeof(IServiceShowMenu), typeof(ServiceShowMenu));
            Services.AddScoped(typeof(IServiceOrder), typeof(ServiceOrder));
            Services.AddScoped(typeof(IServiceFinishedOrder), typeof(ServiceFinishedOrder));

        }
    }
}
