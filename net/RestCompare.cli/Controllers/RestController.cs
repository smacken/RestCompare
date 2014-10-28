using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Repository.EntityFramework;
using RestCompare.Data;

namespace RestCompare.cli.Controllers
{
    public class RestController<T> : ApiController where T: class
    {
        private readonly EFRepository<Db, T, int> _repository;

        public RestController(EFRepository<Db, T, int> repository)
        {
            _repository = repository;
        }

        [Route("")]
        public IEnumerable<T> Get()
        {
            return _repository.Items.AsEnumerable();
        }

        [Route("{id:int}")]
        public T Get(int id)
        {
            return _repository.Find(id).Object;
        }

        [Route("")]
        [HttpPost]
        [ValidationResponseFilter]
        public HttpResponseMessage Post([FromBody]T item)
        {
            _repository.Insert(item);
            _repository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("")]
        [HttpPut]
        [ValidationResponseFilter]
        public HttpResponseMessage Put(int key, T item)
        {
            _repository.Update(key, item);
            _repository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("")]
        [HttpDelete]
        public void Delete(int id)
        {
            _repository.RemoveByKey(id);
            _repository.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
