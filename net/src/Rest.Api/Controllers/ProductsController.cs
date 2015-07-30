using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Rest.Api.Models;

namespace Rest.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private IDbContext Db { get; }
        public ProductsController(IDbContext db)
        {
            Db = db;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return Db.Set<Product>().AsEnumerable();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = Db.Set<Product>().FirstOrDefault(x => x.Id == id);
            if (product == null) return new HttpNotFoundResult();
            return new ObjectResult(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (!ModelState.IsValid) return new BadRequestObjectResult(ModelState);

            if (product.Id == default(int))
            {
                Db.Set<Product>().Add(product);
                Db.SaveChanges();
                return new ObjectResult(product);
            }
            else
            {
                var original = Db.Set<Product>().FirstOrDefault(x => x.Id == product.Id);
                if (original == null) return new HttpNotFoundResult();
                original.Name = product.Name;
                original.Description = product.Description;
                original.CategoryId = product.CategoryId;
                Db.SaveChanges();
                return new ObjectResult(original);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if (!ModelState.IsValid) return new BadRequestObjectResult(ModelState);
            var original = Db.Set<Product>().FirstOrDefault(x => x.Id == product.Id);
            if (original == null) return new HttpNotFoundResult();
            original.Name = product.Name;
            original.Description = product.Description;
            original.CategoryId = product.CategoryId;
            Db.SaveChanges();
            return new ObjectResult(original);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = Db.Set<Product>().FirstOrDefault(x => x.Id == id);
            if (product == null) return new HttpNotFoundResult();
            Db.Set<Product>().Remove(product);
            Db.SaveChanges();
            return new HttpStatusCodeResult(200);
        }
    }
}
