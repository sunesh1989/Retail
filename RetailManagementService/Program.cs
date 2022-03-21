using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RetailManagement.Shared.Product;
using RetailManagementService.DataContext;
using RetailManagementService.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RetailManagementService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder();
            BuildConfig(configuration);

            var builder = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<MongoSettings>(hostContext.Configuration.GetSection(nameof(MongoSettings)));
                    services.AddSingleton<IMongoSettings>(sp => sp.GetRequiredService<IOptions<MongoSettings>>().Value);
                    services.AddTransient<ProductService>();
                    services.AddSingleton<IBus>(RabbitHutch.CreateBus(GlobalInfo.RabbitMqConnectionString));
                    services.AddScoped<IMongoDBContext, MongoDBContext>();
                    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                }).UseConsoleLifetime();
            var host = builder.Build();
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var productService = services.GetRequiredService<ProductService>();
                    SetupResponse(productService);

                    Console.WriteLine("Success");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Occured");
                }
            }
            Console.ReadLine();
        }

        static void BuildConfig(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        }

        private static void SetupResponse(ProductService productService)
        {
            var bus = RabbitHutch.CreateBus(GlobalInfo.RabbitMqConnectionString);
            bus.Rpc.Respond<ProductRequest, ProductResponse>(request => new ProductResponse
            {
                Products = productService.Get(request.Records, request.PageNumber)
            });

            bus.Rpc.Respond<ProductItemRequest, ProductResponse>(request => new ProductResponse
            {
                Product = productService.GetProductById(request.Parameter)
            });
        }
    }

    public static class GlobalInfo
    {
        public static string RabbitMqConnectionString
        {
            get { return "host=localhost"; }
        }
    }
}
