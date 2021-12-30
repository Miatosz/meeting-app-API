using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MeetingAppAPI.Data;
using MeetingAppAPI.Data.Interfaces;
using MeetingAppAPI.Data.Interfaces.Services;
using MeetingAppAPI.Data.Repositories;
using MeetingAppAPI.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingAppAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(config.GetConnectionString("ConnString")));

            services.AddScoped<ITokenService, TokenService>();
            
            // services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        
            return services;
        }
    }
}