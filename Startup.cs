using System;
using MeetingAppAPI.Data;
using MeetingAppAPI.Data.Interfaces;
using MeetingAppAPI.Data.Models.Services;
using MeetingAppAPI.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MeetingAppAPI.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using MeetingAppAPI.Security;
using MeetingAppAPI.Data.Interfaces.Services;
using MeetingAppAPI.Extensions;

namespace MeetingAppAPI
{
    public class Startup
    {
        public IConfiguration _config { get; }

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        

        public void ConfigureServices(IServiceCollection services)
        {

            //!
            // var TokenValidationParameters = new TokenValidationParameters
            // {
            //     ValidIssuer = "https://localhost:5001",
            //     ValidAudience = "https://localhost:5001",
            //     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ")),
            //     ClockSkew = TimeSpan.Zero // remove delay of token when expire
            // };
            //!

            //!
            // services
            //     .AddAuthentication(options =>
            //     {
            //         options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //     })
            //     .AddJwtBearer(cfg =>
            //     {
            //         cfg.TokenValidationParameters = TokenValidationParameters;
            //     });

            //!


            //!
            // services.AddAuthorization(cfg =>
            // {
            //     cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
            //     cfg.AddPolicy("User", policy => policy.RequireClaim("type", "User"));
            // });
            //!
            services.AddIdentityServices(_config);
            services.AddAplicationServices(_config);

            services.Configure<AppSettings>(_config.GetSection("AppSettings"));

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddCors();

            // services.AddCors(opts => 
            // {
            //     opts.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build());
            // });

            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(_config.GetConnectionString("ConnString"));
            });



            services.AddScoped<IEventRepo, EventRepo> ();
            services.AddScoped<ICategoryRepo, CategoryRepo> ();
            services.AddScoped<ICommentRepo, CommentRepo> ();
            services.AddScoped<IUserRepo, UserRepo> ();
            services.AddScoped<IUserService, UserService> ();
            services.AddScoped<ICommentEventService, CommentEventService>();
            services.AddScoped<ITokenService, TokenService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseCors(policy => policy.
            //     AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // app.UseCors(x => x
            //     .AllowAnyOrigin()
            //     .AllowAnyMethod()
            //     .AllowAnyHeader());

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
