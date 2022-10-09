using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.WebApiRestful.Data;
using Sample.WebApiRestful.Data.Abstract;
using Sample.WebApiRestful.Service;

namespace Sample.WebApiRestful.Infrastructure.Configuration
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
            service.AddScoped(typeof(IDapperHelper<>), typeof(DapperHelper<>));
            service.AddScoped<ICategoryService, CategoryService>();


        }
    }
}
