using Application.Activities;
using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Persistence;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
      
             services.AddDbContext<DataContext>(opt => {
                    opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            services.AddCors(opt=>{
               opt.AddPolicy("CorsPolicy",policy=>{
                   policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
               });
            });

            services.AddMediatR(typeof(List.Query).Assembly);

            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}