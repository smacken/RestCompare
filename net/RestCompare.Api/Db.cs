using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RestCompare.Api.Models;

namespace RestCompare.Api
{
    public class Db : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public Db() : base("Db")
        {
            Database.SetInitializer<Db>(null);
        }
    }
}