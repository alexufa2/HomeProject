using CompanyContractsWebAPI.DbRepositories;
using Microsoft.AspNetCore.Mvc;

namespace CompanyContractsWebAPI.Controllers
{
    [ApiController]
    public abstract class BaseApiController<T> : ControllerBase where T : class
    {
        protected IRepository<T> _repository;

        public BaseApiController(IRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpPost, Route("[controller]/Create")]
        public virtual int Create(T item)
        {
            return _repository.Create(item);
        }

        [HttpGet, Route("[controller]/GetById")]
        public virtual T GetById(int id)
        {
            return _repository.GetById(id);
        }

        [HttpGet, Route("[controller]/GetAll")]
        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        [HttpPut, Route("[controller]/Update")]
        public virtual bool Update(T item)
        {
            return _repository.Update(item);
        }

        [HttpDelete, Route("[controller]/Delete")]
        public virtual bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
