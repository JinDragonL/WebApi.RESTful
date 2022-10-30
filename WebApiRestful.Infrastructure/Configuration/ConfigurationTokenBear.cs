using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiRestful.Infrastructure.Configuration
{
    public static class ConfigurationTokenBear
    {
        public static void RegisterTokenBear(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication()
                    .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = configuration["TokenBear:Issuer"],
                            ValidateIssuer = false,
                            ValidAudience = configuration["TokenBear:Audience"],
                            ValidateAudience = false,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenBear:SignatureKey"])),
                            ValidateIssuerSigningKey = true,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero
                        };
                        options.Events = new JwtBearerEvents()
                        {
                            OnTokenValidated = context =>
                            {
                                return Task.CompletedTask;
                            },
                            OnAuthenticationFailed = context =>
                            {
                                return Task.CompletedTask;
                            },
                            OnMessageReceived = context =>
                            {
                                return Task.CompletedTask;
                            },
                            OnChallenge = context =>
                            {
                                return Task.CompletedTask;
                            }
                        };
                    });



        }
    }
}
