using BankSystem.Common;
using BankSystem.Data;
using BankSystem.Models;
using BankSystem.Web.Infrastructure.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
        {
            InitializeDatabaseAsync(app).GetAwaiter().GetResult();

            return app;
        }

        private static async Task InitializeDatabaseAsync(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<BankSystemDbContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                await dbContext.Database.MigrateAsync();
                await SeedUser(userManager, dbContext);

                if (!await roleManager.RoleExistsAsync(GlobalConstants.AdministratorRoleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(GlobalConstants.AdministratorRoleName));
                }
            }
        }

        private static async Task SeedUser(UserManager<User> userManager, BankSystemDbContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                var user = new User
                {
                    Email = "test@test.com",
                    FullName = "Test Testov",
                    UserName = "test@test.com",
                    EmailConfirmed = true,
                    BankAccounts = new List<Account>
                    {
                        new Account
                        {
                            Balance = 10000,
                            CreatedOn = DateTime.UtcNow,
                            Name = "Main account",
                            UniqueId = "ABCJ98131785"
                        }
                    }
                };

                await userManager.CreateAsync(user, "Test123$");
            }
        }

        public static IApplicationBuilder AddDefaultSecurityHeaders(
            this IApplicationBuilder app,
            SecurityHeadersBuilder builder)
            => app.UseMiddleware<SecurityHeadersMiddleware>(builder.Policy());
    }
}
