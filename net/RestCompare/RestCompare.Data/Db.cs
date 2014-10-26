using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestCompare.Data.Models;

namespace RestCompare.Data
{
    public class Db : DbContext
    {
        public DbSet<Product> Products { get; set; }


    }
}
