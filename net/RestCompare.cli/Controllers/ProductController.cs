using System.Web.Http;
using Repository.EntityFramework;
using RestCompare.Data;
using RestCompare.Data.Models;

namespace RestCompare.cli.Controllers
{
    [RoutePrefix("api/v1/products")]
    public class ProductController : RestController<Product>
    {
        public ProductController(EFRepository<Db, Product, int> repository) : base(repository)
        {
            
        }
    }
}
