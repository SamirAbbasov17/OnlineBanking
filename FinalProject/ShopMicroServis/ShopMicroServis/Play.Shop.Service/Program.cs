using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Play.Common.MassTransit;
using Play.Common.MongoDB;
using Play.Common.Settings;
using Play.Shop.Service.Entities;

namespace Play.Shop.Service
{
    public class Program
    {
        private const string AllowedOriginSetting = "AllowedOrigin";
        private ServiceSettings serviceSettings = new();

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = new ConfigurationBuilder()
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json")
            .Build();
            
            //serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

            builder.Services.AddMongo()
                    .AddMongoRepository<Item>("items")
                    .AddMassTransitWithRabbitMq();

            builder.Services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Play.Shop.Service", Version = "v1" });
            });

            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
          

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Play.Shop.Service v1"));

                //app.UseCors(builder =>
                //{
                //    builder.WithOrigins(configuration[AllowedOriginSetting])
                //        .AllowAnyHeader()
                //        .AllowAnyMethod();
                //});
            }

            app.UseHttpsRedirection();


            app.UseAuthorization();

            app.MapControllers();
           

            app.Run();
        }
    }
}