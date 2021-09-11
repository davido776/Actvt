using System.Text;
using Actvt.API.Services;
using Actvt.Domain;
using Actvt.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Actvt.API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
              services.AddIdentityCore<AppUser>(options=>
              {
                  options.Password.RequireNonAlphanumeric = false;
              })
              .AddEntityFrameworkStores<DataContext>()
              .AddSignInManager<SignInManager<AppUser>>();
            
              var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
              services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                      .AddJwtBearer(opt =>
                      {
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                           ValidateIssuerSigningKey = true,
                           IssuerSigningKey = key,
                           ValidateIssuer = false,
                           ValidateAudience = false
                        };
                      });
              services.AddScoped<TokenService>();

              return services;
        }
    }
}