using System;
using System.Threading.Tasks;
using ConsoleApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp.UI
{
    class Program
    {
        private static void ConfigureServices(HostBuilderContext _, IServiceCollection services)
        {
            services.AddDbContext<ConsoleContext>(options =>
                options.UseSqlite("Data Source=nagato.db;"));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddHostedService<Startup>();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(ConfigureServices);
        }

        static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    ConsoleContext context = services.GetService<ConsoleContext>();
                    await context.Database.MigrateAsync();
                    await ConsoleSeed.SeedAsync(context);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            await host.RunAsync();
        }
    }
}