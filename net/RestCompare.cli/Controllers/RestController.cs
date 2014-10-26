using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<T> Get()
        {
            return _repository.Items.AsEnumerable();
        }

        public T Get(int id)
        {
            return _repository.Find(id).Object;
        }

        public void Post(T item)
        {
            _repository.Insert(item);
        }

        public void Put(int key, T item)
        {
            _repository.Update(key, item);
        }

        public void Delete(int id)
        {
            _repository.RemoveByKey(id);
        }
    }
}
