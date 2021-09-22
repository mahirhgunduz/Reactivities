using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence;

namespace API
{
    // public interface InterfaceDeneme {  
    //     void Metod();
    // }

    // public class InterfaceDenemeClass : InterfaceDeneme
    // {
    //     public void Metod()
    //     {
    //         Console.WriteLine("HOOPPPP");
    //     }
    // }

    public class Startup
    {

        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        
            // services.AddDbContext<DataContext>(opt => {
            //     opt.UseSqlite(_config.GetConnectionString("DefaultConnection"));
            // });

                services.AddDbContext<DataContext>(opt => {
                opt.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });

            services.AddCors(opt=>{
               opt.AddPolicy("CorsPolicy",policy=>{
                   policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
               });
            });

            // services.AddTransient<InterfaceDeneme,InterfaceDenemeClass>();

            // var serviceProvider = services.BuildServiceProvider();

            // var inte = serviceProvider.GetService<InterfaceDeneme>();

            // inte.Metod();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");

            //app.UseHttpsRedirection();
            app.UseMvc();
        }

    }
}
