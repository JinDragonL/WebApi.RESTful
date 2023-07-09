using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApiRestful.Domain.Entities;

namespace WebApiRestful.Data
{
    public class WebApiRestfulContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public WebApiRestfulContext(DbContextOptions<WebApiRestfulContext> options, 
            IConfiguration configuration,
            IServiceProvider serviceProvider) : base(options)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<UserToken> UserToken { get; set; }
        public DbSet<DBLog> DBLog { get; set; }

        //AspNetUser
        //AspNetRole

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");

            SeedData(modelBuilder);
        }


        private void SeedData(ModelBuilder modelBuilder)
        {
            var passwordHasherService = _serviceProvider.CreateScope().ServiceProvider.GetService<PasswordHasher<ApplicationUser>>();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                   Id = Guid.NewGuid().ToString(),
                   UserName = "admin",
                   PasswordHash = passwordHasherService.HashPassword(new ApplicationUser(), "1"),
                   Email = "admin@ymail.com",
                   AccessFailedCount = 0
                });

            var defaltRoles = _configuration.GetSection("DefaultRole");

            if(defaltRoles.Exists()) {

                foreach (var role in defaltRoles.GetChildren())
                {
                    modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = role.Value,
                        NormalizedName = role.Value.ToString()
                    });
                }
            }

        }

    }
}
