using Alachisoft.NCache.Caching.Distributed;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using WebApiRestful.Configuration;
using WebApiRestful.Infrastructure.Configuration;
using WebApiRestful.Middleware;

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
            services.AddLogging(logging =>
            {
                logging.AddNLog();
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            });

            //services.AddAuthorization(options =>
            //{
            //    //options.AddPolicy("OnlyAdmin", x => x.RequireRole("Admin"));

            //    options.FallbackPolicy = new AuthorizationPolicyBuilder()
            //                                    .RequireAuthenticatedUser()
            //                                    .Build();

            //});

            //Automapper

            services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);

            services.AddNCacheDistributedCache(configuration =>
            {
                configuration.CacheName = "WebApiRestfulCache";
                configuration.EnableLogs = true;
                configuration.ExceptionsEnabled = true;
            });

            //Register Datatabase
            services.RegisterContextDb(Configuration);

            //Register Dependency Injection
            services.RegisterDI(Configuration);

            // Register Authentication Token
            services.RegisterTokenBear(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sample.WebApiRestful",
                    Version = "v1",
                    Description = "This is Swagger WebAPI Restful",

                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    In = ParameterLocation.Header,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                    Description = "Please input your token"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });


                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "MyPolicy", builder =>
                    {
                        builder.AllowAnyOrigin().
                                AllowAnyMethod().
                                AllowAnyHeader();
                    });
            });

            services.AddValidatorsFromAssemblyContaining<Startup>();
        }
        public class AuthResponsesOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                    .Union(context.MethodInfo.GetCustomAttributes(true))
                    .OfType<AuthorizeAttribute>();

                if (authAttributes.Any())
                    operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            }
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEBAPI RESTFUL v1"));
            }

            app.UseHttpsRedirection();

            //Handle Global Error
            //app.UseExceptionHandler(error =>
            //{
            //    error.Run(async httpContext =>
            //    {
            //        var msg = httpContext.Features.Get<IExceptionHandlerFeature>();

            //        int statusCode = httpContext.Response.StatusCode;

            //        await httpContext.Response.WriteAsync($"{statusCode} - {msg.Error.Message}");

            //    });
            //});

            app.UseStaticFiles();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials()); // allow credentials

            app.UseRouting();
            app.UseStatusCodePages();

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
