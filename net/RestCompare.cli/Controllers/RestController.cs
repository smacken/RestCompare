﻿using System;
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

        [HttpPost]
        [ValidationResponseFilter]
        public HttpResponseMessage Post(T item)
        {
            _repository.Insert(item);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        [ValidationResponseFilter]
        public HttpResponseMessage Put(int key, T item)
        {
            _repository.Update(key, item);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _repository.RemoveByKey(id);
        }
    }
}
