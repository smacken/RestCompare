using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Repository.EntityFramework;
using RestCompare.Data;
using RestCompare.Data.Models;

namespace RestCompare.cli.Controllers
{
    public class ProductController : ApiController
    {
        private readonly EFRepository<Db, Product, int> _productRepository;

        public ProductController(EFRepository<Db, Product, int> productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> Get()
        {
            return _productRepository.Items.AsEnumerable();
        }

        public Product Get(int id)
        {
            return _productRepository.Find(id).Object;
        }

        public void Post(Product product)
        {
            _productRepository.Insert(product);
        }

        public void Put(Product product)
        {
            _productRepository.Update(product.Id, product);
        }

        public void Delete(int id)
        {
            _productRepository.RemoveByKey(id);
        }

    }
}
