using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence;
using Persistence.Persistence;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
           var host  = CreateWebHostBuilder(args).Build();

            var scope = host.Services.CreateScope();

           var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<DataContext>();

                await context.Database.MigrateAsync();

                await Seed.SeedData(context);
            }
            catch (System.Exception ex)
            {
                
               var logger = services.GetRequiredService<ILogger<Program>>();

               logger.LogError(ex,"ERORRRORORR");
            }

             await host.RunAsync();
           
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
