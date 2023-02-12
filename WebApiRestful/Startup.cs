using Alachisoft.NCache.Caching.Distributed;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Configuration;
using WebApiRestful.Infrastructure.Configuration;

namespace Sample.WebApiRestful
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddNCacheDistributedCache(configuration => {
                configuration.CacheName = "WebApiRestfulCache";
                configuration.EnableLogs = true;
                configuration.ExceptionsEnabled = true;
            });

            //Register Datatabase
            services.RegisterContextDb(Configuration);

            //Register Dependency Injection
            services.RegisterDI();

            // Register Authentication Token
            services.RegisterTokenBear(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample.WebApiRestful", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "MyPolicy", builder =>
                    {
                        builder.WithOrigins("http://localhost:4200").
                                AllowAnyMethod().
                                AllowAnyHeader();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi RESTful v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials()); // allow credentials

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute("pet-delete", "/mypet/{petId}", defaults: new
                {
                    controller = "MyPetApi",
                    action = "DeletePet"
                });
            });
        }
    }
}
