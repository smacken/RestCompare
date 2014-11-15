using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestCompare.Api.Models;
using SharpRepository.Repository;

namespace RestCompare.Api.Controllers
{
    [RoutePrefix("api/v1/products")]
    public class ProductsController : RestController<Product>
    {
        public ProductsController(IRepository<Product, int> repository): base(repository)
        {   
        }
    }
}