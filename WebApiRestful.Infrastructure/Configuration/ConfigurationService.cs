using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApi.Restful.Core.Abstract;
using WebApi.Restful.Core.Cache;
using WebApi.Restful.Core.Configuration;
using WebApi.Restful.Core.EmailHelper;
using WebApiRestful.Authentication.Service;
using WebApiRestful.Data;
using WebApiRestful.Data.Abstract;
using WebApiRestful.Domain.Entities;
using WebApiRestful.Infrastructure.CommonService;
using WebApiRestful.Service;
using WebApiRestful.Service.Abstract;

namespace WebApiRestful.Infrastructure.Configuration
{
    public static class ConfigurationService
    {
        public static void RegisterContextDb(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<WebApiRestfulContext>(options => options
                            .UseSqlServer(configuration.GetConnectionString("SampleWebApiConnection"),
                            options => options.MigrationsAssembly(typeof(WebApiRestfulContext).Assembly.FullName)));

            service.AddIdentity<ApplicationUser, IdentityRole>(option =>
                    {
                        option.SignIn.RequireConfirmedEmail = true;
                    })
                    .AddEntityFrameworkStores<WebApiRestfulContext>()
                    .AddDefaultTokenProviders();

            service.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(10);
            });
        }

        public static void RegisterDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<EmailConfig>(configuration.GetSection("MailSettings"));

            service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            service.AddScoped(typeof(IDapperHelper<>), typeof(DapperHelper<>));
            service.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            service.AddSingleton<IDistributedCacheService, DistributedCacheService>();

            service.AddScoped<PasswordHasher<ApplicationUser>>();
            service.AddScoped<PasswordValidator<ApplicationUser>>();
            service.AddScoped<IEmailHelper, EmailHelper>();
            service.AddScoped<IEmailTemplateReader, EmailTemplateReader>();
            service.AddScoped<ICategoryService, CategoryService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IUserTokenService, UserTokenService>();
            service.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}
