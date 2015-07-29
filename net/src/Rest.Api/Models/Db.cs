using Microsoft.Data.Entity;
using Rest.Api.Models;

namespace Rest.Api.Models
{
    public class Db : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(b => b.Name).Required();
        }
    }
}
