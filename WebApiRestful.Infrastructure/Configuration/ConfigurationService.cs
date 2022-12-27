using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiRestful.Authentication.Service;
using WebApiRestful.Data;
using WebApiRestful.Data.Abstract;
using WebApiRestful.Service;
using WebApiRestful.Service.Abstract;

namespace WebApiRestful.Infrastructure.Configuration
{
    public static class ConfigurationService
    {
        public static void RegisterContextDb(this IServiceCollection service, IConfiguration configuration )
        {
            service.AddDbContext<WebApiRestfulContext>(options => options
                            .UseSqlServer(configuration.GetConnectionString("SampleWebApiConnection"),
                            options => options.MigrationsAssembly(typeof(WebApiRestfulContext).Assembly.FullName)));

        }

        public static void RegisterDI(this IServiceCollection service)
        {
            service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            service.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            service.AddScoped(typeof(IDapperHelper<>), typeof(DapperHelper<>));
            service.AddScoped<ICategoryService, CategoryService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<ITokenHandler, TokenHandler>();
            service.AddScoped<IUserTokenService, UserTokenService>();

        }
    }
}
