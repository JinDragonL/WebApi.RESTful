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
        //public DbSet<UserToken> UserToken { get; set; }
        public DbSet<DBLog> DBLog { get; set; }

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

            string defaultName = "Admin";
            string defaultEmail = "admin@ymail.com";

            var passwordHasherService = _serviceProvider.CreateScope().ServiceProvider.GetService<PasswordHasher<ApplicationUser>>();

            string roleId = string.Empty;

            var roles = _configuration.GetSection("DefaultRole");

            if (roles.Exists())
            {
                foreach (var role in roles.GetChildren())
                {
                    string id = Guid.NewGuid().ToString();

                    modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
                    {
                        Id = id,
                        Name = role.Value,
                        NormalizedName = role.Value.ToUpper(),
                    });

                    if(role.Value == defaultName)
                    {
                        roleId = id;
                    }
                }
            }

            string userId = Guid.NewGuid().ToString();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = userId,
                    UserName = defaultName.ToLower(),
                    NormalizedUserName = defaultName.ToUpper(),
                    Email = defaultEmail,
                    NormalizedEmail = defaultEmail.ToUpper(),
                    AccessFailedCount = 0,
                    PasswordHash = passwordHasherService.HashPassword(new ApplicationUser
                    {
                        UserName = defaultName.ToLower(),
                        NormalizedUserName = defaultName.ToUpper(),
                        Email = defaultEmail,
                        NormalizedEmail = defaultEmail.ToUpper(),
                    }, "1")
                });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = userId,
            });
        }
    }
}
