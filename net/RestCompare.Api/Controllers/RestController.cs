using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestCompare.Api.Validation;
using SharpRepository.Repository;

namespace RestCompare.Api.Controllers
{
    public abstract class RestController<T> : ApiController where T : class, new()
    {
        private readonly IRepository<T, int> _repository;

        protected RestController(IRepository<T, int> repository)
        {
            _repository = repository;
        }

        [Route("")]
        public IEnumerable<T> Get()
        {
            return _repository.GetAll();
        }

        [Route("{id:int}")]
        public T Get(int id)
        {
            return _repository.Get(id);
        }

        [Route("")]
        [HttpPost]
        [ValidationResponseFilter]
        public HttpResponseMessage Post([FromBody]T item)
        {
            _repository.Add(item);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("")]
        [HttpPut]
        [ValidationResponseFilter]
        public HttpResponseMessage Put(int key, T item)
        {
            _repository.Update(item);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("")]
        [HttpDelete]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}