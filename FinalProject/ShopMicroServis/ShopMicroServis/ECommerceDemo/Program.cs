using ECommerceDemo.Configuration;
using ECommerceDemo.Extensions;
using ECommerceDemo.Data;
using ECommerceDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ECommerceDemo.Services;
using System.Net;

namespace ECommerceDemo
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


            builder.Services.AddDbContext<ECommerceDataDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ECommerceDemoUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
            })
                .AddDefaultUI()
                .AddEntityFrameworkStores<ECommerceDataDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "auth_cookie";
                options.Cookie.SameSite = SameSiteMode.None;
                options.LoginPath = new PathString("/Identity/Unauthorized/Index");
                options.AccessDeniedPath = new PathString("/Identity/Unauthorized/Index");

                // Not creating a new object since ASP.NET Identity has created
                // one already and hooked to the OnValidatePrincipal event.
                // See https://github.com/aspnet/AspNetCore/blob/5a64688d8e192cacffda9440e8725c1ed41a30cf/src/Identity/src/Identity/IdentityServiceCollectionExtensions.cs#L56
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            });

            //builder.Services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/Identity/Unauthorized/Index"; // Login sayfas?n?n yolunu belirleyin
            //    options.AccessDeniedPath = "/Identity/Unauthorized/Index"; // Yetkisiz eri?im durumunda yönlendirilecek sayfan?n yolu
            //});

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
            builder.Services.AddScoped<IGetCurrentUser, GetCurrentUser>();
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
            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                        response.StatusCode == (int)HttpStatusCode.Forbidden)
                    response.Redirect("/Identity/Unauthorized/Index");
            });
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