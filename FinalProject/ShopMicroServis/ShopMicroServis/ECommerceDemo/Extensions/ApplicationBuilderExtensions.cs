namespace ECommerceDemo.Extensions
{
    using System.Threading.Tasks;
    using ECommerceDemo.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static async Task InitializeDatabaseAsync(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ECommerceDataDbContext>();

                await dbContext.Database.MigrateAsync();
            }
        }
    }
}