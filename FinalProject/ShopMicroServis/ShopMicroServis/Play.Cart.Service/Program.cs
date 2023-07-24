using Microsoft.OpenApi.Models;
using Play.Cart.Service.Clients;
using Play.Cart.Service.Entities;
using Play.Common.MassTransit;
using Play.Common.MongoDB;
using Polly;
using Polly.Timeout;

namespace Play.Cart.Service
{
    public class Program
    {
        private const string AllowedOriginSetting = "AllowedOrigin";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = new ConfigurationBuilder()
             .SetBasePath(builder.Environment.ContentRootPath)
             .AddJsonFile("appsettings.json")
             .Build();

            // Add services to the container.
            builder.Services.AddMongo()
                 .AddMongoRepository<CartItem>("cartitems")
                 .AddMongoRepository<ShopItem>("shopitems")
                 .AddMassTransitWithRabbitMq();

            AddCatalogClient(builder.Services);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Play.Cart.Service", Version = "v1" });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
         

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Play.Cart.Service v1"));

                //app.UseCors(builder =>
                //{
                //    builder.WithOrigins(configuration[AllowedOriginSetting])
                //        .AllowAnyHeader()
                //        .AllowAnyMethod();
                //});
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        private static void AddCatalogClient(IServiceCollection services)
        {
            Random jitterer = new Random();

            services.AddHttpClient<ShopClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7098");
            })
            .AddTransientHttpErrorPolicy(builder => builder.Or<TimeoutRejectedException>().WaitAndRetryAsync(
                5,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                                + TimeSpan.FromMilliseconds(jitterer.Next(0, 1000)),
                onRetry: (outcome, timespan, retryAttempt) =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    serviceProvider.GetService<ILogger<ShopClient>>()?
                        .LogWarning($"Delaying for {timespan.TotalSeconds} seconds, then making retry {retryAttempt}");
                }
            ))
            .AddTransientHttpErrorPolicy(builder => builder.Or<TimeoutRejectedException>().CircuitBreakerAsync(
                3,
                TimeSpan.FromSeconds(15),
                onBreak: (outcome, timespan) =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    serviceProvider.GetService<ILogger<ShopClient>>()?
                        .LogWarning($"Opening the circuit for {timespan.TotalSeconds} seconds...");
                },
                onReset: () =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    serviceProvider.GetService<ILogger<ShopClient>>()?
                        .LogWarning($"Closing the circuit...");
                }
            ))
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));
        }
    }
}