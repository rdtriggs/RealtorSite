using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Realtor.Core.Exceptions;
using Realtor.Core.Extensions;

namespace Realtor.Api.Infrastructure
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            //TODO: Test or Remove
            //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
            .AddEnvironmentVariables()
            .Build();

        public static async Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();

            try
            {
                Log.Information("Starting API, hold on to your hats!");
                IWebHost host = BuildWebHost(args);
                await host.RunAsync().AnyContext();

                return 0;
            }
            catch (Exception e)
            {
                if (!(e is LoggedException))
                {
                    Log.Fatal(e, "{Class} -> {Method} -> API terminated unexpectedly", nameof(Program), nameof(Main));
                }

                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseConfiguration(Configuration).UseStartup<Startup>().UseSerilog().Build();
        }
    }
}
