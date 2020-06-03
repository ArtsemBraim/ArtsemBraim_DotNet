using AutoMapper;
using Clinic.BLL.Infrastructure;
using Clinic.ConsoleUI.MenuItems;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Clinic.ConsoleUI
{
    class Program
    {
        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var services = serviceCollection.BuildServiceProvider();
            AppStart(services.GetService<MainMenu>());
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var config = CreateConfigurationConfig();
            services.AddSingleton<IConfiguration>(config);

            var connectionString = config.GetConnectionString("ClinicConnection");

            services.AddAutoMapper(typeof(MapperProfile));
            services.ConfigureBllServices(connectionString);
            services.AddScoped<MainMenu>();
            services.AddScoped<DoctorMenu>();
            services.AddScoped<PatientMenu>();
        }

        private static void AppStart(MainMenu menu)
        {
            menu.Start();
        }

        private static IConfigurationRoot CreateConfigurationConfig()
        {
            var configBuilder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true);
            var config = configBuilder.Build();
            return config;
        }
    }
}
