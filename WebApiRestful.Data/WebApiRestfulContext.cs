using Microsoft.EntityFrameworkCore;
using Sample.WebApiRestful.Domain.Entities;

namespace Sample.WebApiRestful.Data
{
    public class WebApiRestfulContext : DbContext
    {
        public WebApiRestfulContext(DbContextOptions<WebApiRestfulContext> options): base(options)
        {
            
        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
