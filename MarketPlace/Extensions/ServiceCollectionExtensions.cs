using MarketPlace.Infrastructure.Data;
using MarketPlace.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            return services;

        }

        public static IServiceCollection AddAplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>(options =>
                           options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddMvc();

            return services;
        }

        public static IServiceCollection AddAplicationIdentity(this IServiceCollection services, IConfiguration config)
        {
            services
           .AddIdentityCore<ApplicationUser>(options =>
           {
               options.SignIn.RequireConfirmedAccount = false;
               options.Password.RequireDigit = false;
               options.Password.RequireLowercase = false;
               options.Password.RequireNonAlphanumeric = false;
               options.Password.RequireUppercase = false;
           })
           .AddRoles<IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }
    }
}
