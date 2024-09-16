using DesafioBackEndProject.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DesafioBackEndProject.DependecyInjection
{
    public static class AuthConfigurationDI
    {
        public static void AddAuthConfiguration(this IServiceCollection services, ConfigurationManager configuration )
        {


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "xyz.com",
                        ValidAudience = "xyz.com",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("FWM4sX5KV8pko3rkhOxoeGPHqeL7z2niLCZCcrRmimdnz0tdA0vMxRnXkHGWEket")) // Chave secreta
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("admin"));
                options.AddPolicy("DriverPolicy", policy => policy.RequireRole("driver"));
            });

        }
    }
}
