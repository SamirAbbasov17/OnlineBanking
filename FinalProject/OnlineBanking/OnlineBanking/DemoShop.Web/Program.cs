using DemoShop.Data;
using DemoShop.Models;
using DemoShop.Services.Implementations;
using DemoShop.Services.Interfaces;
using DemoShop.Web.Configuration;
using DemoShop.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DemoShop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = new ConfigurationBuilder()
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json")
            .Build();


            builder.Services.AddDbContext<DemoShopDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<DemoShopUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
            })
                .AddDefaultUI()
                .AddEntityFrameworkStores<DemoShopDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options => { options.Cookie.Name = "DemoShopLogin"; });

            builder.Services
                .Configure<DestinationAccountConfiguration>(
                    configuration.GetSection(nameof(DestinationAccountConfiguration)))
                .Configure<DirectPaymentsConfiguration>(
                    configuration.GetSection(nameof(DirectPaymentsConfiguration)))
                .Configure<CardPaymentsConfiguration>(
                    configuration.GetSection(nameof(CardPaymentsConfiguration)));

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<IProductsService, ProductsService>();
            builder.Services.AddScoped<IOrdersService, OrdersService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.InitializeDatabaseAsync().GetAwaiter().GetResult();

            if (!app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRouting();
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "areas",
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
            app.Run();
        }
    }
}