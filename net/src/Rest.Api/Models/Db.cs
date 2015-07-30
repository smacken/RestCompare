using Microsoft.Data.Entity;
using Rest.Api.Models;
using System;

namespace Rest.Api.Models
{
    public interface IDbContext : IDisposable
    {
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
        //int SqlCommand(string sql, params object[] parameters);
    }

    public class Db : DbContext, IDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var products = modelBuilder.Entity<Product>();
            products.Key(x => x.Id);
            products.Property(b => b.Name).Required();
        }
    }
}
