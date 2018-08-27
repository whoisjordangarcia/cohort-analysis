using System;
using System.IO;
using Invitae.CohortAnalysis.Business;
using Invitae.CohortAnalysis.ConsoleApplication;
using Invitae.CohortAnalysis.Domain.Models;
using Invitae.CohortAnalysis.Interfaces;
using Invitae.CohortAnalysis.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Invitae.CohortAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var services = new ServiceCollection();
            ConfigureServices(services);

            IConfiguration config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
              .Build();

            services.Configure<Settings>(options => config.GetSection("Settings").Bind(options));

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<App>().Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // services
            serviceCollection.AddTransient<ICohortAnalysisService, CohortAnalysisService>();
            serviceCollection.AddTransient<ICustomerService, CustomerService>();
            serviceCollection.AddTransient<IOrderService, OrderService>();
            serviceCollection.AddTransient<ICsvService, CsvService>();

            // business logic layer
            serviceCollection.AddTransient<ICohortCalculationLogic, CohortCalculationLogic>();

            serviceCollection.AddTransient<App>();
        }
    }
}
