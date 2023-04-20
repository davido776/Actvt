using Actvt.Application.Activities;
using Actvt.Application.Core;
using Actvt.Application.Interfaces;
using Actvt.Persistence;
using Infrastructure.Photos;
using Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Actvt.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Actvt.API", Version = "v1" });
                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                //      Enter 'Bearer' [space] and then your token in the text input below.
                //      \r\n\r\nExample: 'Bearer 12345abcdef'",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //});
            });
            //services.AddDbContext<DataContext>(opt => 
            //{
            //  opt.UseSqlite(config.GetConnectionString("PostgresConnection"));
            //});

            //services.AddDbContext<DataContext>(opt =>
            //{
            //    opt.UseNpgsql(config.GetConnectionString("PostgresConnection"), x => x.MigrationsAssembly("Actvt.test.migration"));
            //});

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("SqlConnection"), x => x.MigrationsAssembly("Actvt.test.migration"));
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("WWW-Authenticate", "Pagination")
                    .WithOrigins("http://localhost:3000");
                    //.AllowAnyOrigin();
                });
            });
            services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));
            services.AddSignalR();

            return services;
        }
    }
}