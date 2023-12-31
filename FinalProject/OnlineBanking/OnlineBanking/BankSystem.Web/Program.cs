using BankSystem.Common.Configuration;
using BankSystem.Common.EmailSender;
using BankSystem.Common.Utils;
using BankSystem.Data;
using BankSystem.Models;
using BankSystem.Web.Infrastructure.Extensions;
using BankSystem.Web.Infrastructure.Middleware;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Configuration;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace BankSystem.Web
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


            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            builder.Services.AddDbContextPool<BankSystemDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            builder.Services
                .Configure<CookieTempDataProviderOptions>(options => { options.Cookie.IsEssential = true; });

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            })
                .AddEntityFrameworkStores<BankSystemDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.SlidingExpiration = true;
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
            });

            builder.Services
                .AddDomainServices()
                .AddApplicationServices()
                .AddCommonProjectServices()
                .AddAuthentication().AddGoogle(options =>
                {

                    options.ClientId = configuration["App:GoogleClientId"];
                    options.ClientSecret = configuration["App:GoogleClientSecret"];
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                }).AddFacebook(options =>
                {

                    options.AppId = configuration["Authentication:Facebook:AppId"];
                    options.AppSecret = configuration["Authentication:Facebook:AppSecret"];
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                });

            builder.Services.Configure<SecurityStampValidatorOptions>(
                options => { options.ValidationInterval = TimeSpan.Zero; });

            builder.Services
                .Configure<RouteOptions>(options => options.LowercaseUrls = true);

            builder.Services
                .Configure<BankConfiguration>(
                    configuration.GetSection(nameof(BankConfiguration)))
                .Configure<SendGridConfiguration>(
                    configuration.GetSection(nameof(SendGridConfiguration)));

            builder.Services
                .PostConfigure<BankConfiguration>(settings =>
                {
                    if (!ValidationUtil.IsObjectValid(settings))
                    {
                        throw new ApplicationException("BankConfiguration is invalid");
                    }
                })
                .PostConfigure<SendGridConfiguration>(settings =>
                {
                    if (!ValidationUtil.IsObjectValid(settings))
                    {
                        throw new ApplicationException("SendGridConfiguration is invalid");
                    }
                });

            builder.Services
                .AddResponseCompression(options => options.EnableForHttps = true);

            builder.Services
                .AddRazorPages()
                .AddRazorPagesOptions(options => options.Conventions.AuthorizePage("/MoneyTransfers"));

            builder.Services
                .AddControllers(options => options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>());
            
            builder.Services.AddControllers();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/error/500");
                app.UseHsts();
            }

            // We can add all recommended headers or add custom ones or choose between different ones
            app.AddDefaultSecurityHeaders(
                new SecurityHeadersBuilder()
                    .AddDefaultSecurePolicy());

            app.UseResponseCompression();
            app.UseStatusCodePages();

            app.UseHttpsRedirection();
            app.UseStaticFiles(
                new StaticFileOptions
                {
                    OnPrepareResponse = ctx =>
                    {
                        const int cacheDurationInSeconds = 60 * 60 * 24 * 365; // 1 year
                        ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                            $"public,max-age={cacheDurationInSeconds}";
                    }
                });
            app.UseCookiePolicy();

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseRouting();

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

            app.InitializeDatabase();
            app.Run();
        }
    }
}