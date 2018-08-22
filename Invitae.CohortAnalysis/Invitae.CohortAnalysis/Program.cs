using System;
using System.IO;
using Invitae.CohortAnalysis.ConsoleApplication;
using Invitae.CohortAnalysis.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Invitae.CohortAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            IConfiguration config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            services.Configure<Settings>(options => config.GetSection("Settings").Bind(options));

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<App>().Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<App>();
        }
    }
}
