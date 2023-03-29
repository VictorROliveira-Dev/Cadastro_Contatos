using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AppWebMVC.Data;
using AppWebMVC.Services;

namespace AppWebMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppWebMVCContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AppWebMVCContext") ?? throw new InvalidOperationException("Connection string 'AppWebMVCContext' not found.")));

            //builder.Services.AddScoped<SeedingService>();
            builder.Services.AddScoped<SellerService>();
            builder.Services.AddScoped<DepartmentService>();
            builder.Services.AddScoped<SalesRecordService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            SeedDatabase();

            void SeedDatabase()
            {
                using (var Scope = app.Services.CreateScope())
                {
                    try
                    {
                        var ScopedContext = Scope.ServiceProvider.GetRequiredService<AppWebMVCContext>();
                        SeedingService.Initialize(ScopedContext);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}