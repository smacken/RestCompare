using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Rest.Api.Models
{
    public class Db : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public Db() : base("Db")
        {
            Database.SetInitializer<Db>(null);
        }

        protected override void OnConfiguring(DbContextOptions options)
        {
            options.UseSqlServer(Startup.Configuration.Get("Data:DefaultConnection:ConnectionString"));
        }
    }
}
