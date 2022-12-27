using Microsoft.EntityFrameworkCore;
using WebApiRestful.Domain.Entities;

namespace WebApiRestful.Data
{
    public class WebApiRestfulContext : DbContext
    {
        public WebApiRestfulContext(DbContextOptions<WebApiRestfulContext> options): base(options)
        {
            
        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserToken> UserToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
