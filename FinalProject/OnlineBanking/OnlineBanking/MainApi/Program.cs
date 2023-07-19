using BankSystem.Common.AutoMapping.Profiles;
using BankSystem.Common.Utils;
using MainApi.Data;
using MainApi.Infrastructure;
using MainApi.Services.Bank;
using Microsoft.EntityFrameworkCore;

namespace MainApi
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

            
            builder.Services.AddDbContext<MainApiDbContext>(options =>
                options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IBanksService, BanksService>();
            builder.Services.Configure<MainApiConfiguration>(configuration.GetSection(nameof(MainApiConfiguration)));
            builder.Services.Configure<MainApiDbContext>(
               configuration.GetSection(nameof(MainApiDbContext)));

            builder.Services.PostConfigure<MainApiDbContext>(settings =>
            {
                if (!ValidationUtil.IsObjectValid(settings))
                {
                    throw new ApplicationException("CentralApiConfiguration is invalid");
                }
            });
            builder.Services.AddAutoMapper(typeof(DefaultProfile));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            //app.Run();



            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers();});

            app.SeedData();
            app.Run();
        }
    }
}