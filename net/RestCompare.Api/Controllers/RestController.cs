using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using RestCompare.Api.Validation;
using SharpRepository.EfRepository;
using SharpRepository.Repository;
using SharpRepository.Repository.Queries;

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

        // GET: api/Entity/pageSize/pageNumber/orderBy(optional) 
        [Route("{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public IHttpActionResult GetPaged(int pageSize = 10, int pageNumber = 1, string orderBy = "")
        {
            var totalCount = _repository.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);
            var items = _repository.GetAll(new PagingOptions<T>(pageNumber, pageSize, orderBy));
            return Ok(new
            {
                TotalCount = totalCount,
                Pages = totalPages,
                Items = items
            });
        }

        // GET: api/Entity/list?ids=1,4,8
        [Route("list")]
        public IEnumerable<T> List([FromUri] List<int> ids)
        {
            var repo = _repository as EfRepository<T, int>;
            if(repo == null) return new List<T>();
            // db.Entity.Where(x => ids.Contains(x.Key))
            var matchingId = ContainsPredicate<T>(ids, repo.Conventions.GetPrimaryKeyName(repo.EntityType));
            return repo.FindAll(matchingId);
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

        private Expression<Func<TEntity, bool>> ContainsPredicate<TEntity>(IEnumerable arr, string fieldname) where TEntity : class
        {
            ParameterExpression entity = Expression.Parameter(typeof(TEntity), "entity");
            MemberExpression member = Expression.Property(entity, fieldname);

            var containsMethods = typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public).Where(m => m.Name == "Contains");
            MethodInfo method = containsMethods.FirstOrDefault(m => m.GetParameters().Count() == 2);
            method = method.MakeGenericMethod(member.Type);
            var exprContains = Expression.Call(method, new Expression[] { Expression.Constant(arr), member });
            return Expression.Lambda<Func<TEntity, bool>>(exprContains, entity);
        }
    }
}