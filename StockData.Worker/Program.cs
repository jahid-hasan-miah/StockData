using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using StockData.StockManaging.Contexts;
using StockData.StockManaging.Modules;
using System;

namespace StockData.Worker
{
    public class Program
    {
        private static string _connectionString;
        private static string _migrationAssemblyName;
        private static IConfiguration _configuration;
        public static void Main(string[] args)
        {

            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(_configuration)
                .CreateLogger();
            try
            {
                Log.Information("Application Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseSerilog()
                .ConfigureContainer<ContainerBuilder>(builder => {
                    builder.RegisterModule(new WorkerModule());
                    builder.RegisterModule(new StockingModule(_connectionString, _migrationAssemblyName));
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var config = hostContext.Configuration;
                    _connectionString = config.GetConnectionString("DefaultConnection");
                    _migrationAssemblyName = typeof(Worker).Assembly.FullName;

                    services.AddDbContext<StockContext>(options =>
                        options.UseSqlServer(_connectionString,
                            m => m.MigrationsAssembly(_migrationAssemblyName)));

                    services.AddHostedService<Worker>();
                    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                });
    }
}
