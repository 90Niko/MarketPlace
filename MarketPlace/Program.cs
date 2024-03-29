using MarketPlace.Extensions.DependencyInjection;

namespace MarketPlace
{
    public class Program
    {
        public static async void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAplicationDbContext(builder.Configuration);
            builder.Services.AddAplicationIdentity(builder.Configuration);

            builder.Services.AddControllersWithViews();

            builder.Services.AddAplicationServices();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapDefaultControllerRoute();

            app.MapRazorPages();

            await app.RunAsync();
        }
    }
}