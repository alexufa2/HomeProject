using CompanyContractsWebAPI.DbRepositories;
using Microsoft.AspNetCore.Mvc;

namespace CompanyContractsWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public abstract class BaseApiController<T> : ControllerBase where T : class
    {
        protected IRepository<T> _repository;

        public BaseApiController(IRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpPost(Name = "Create")]
        public virtual int Create(T item)
        {
            return _repository.Create(item);
        }

        [HttpGet(Name = "GetById")]
        public virtual T GetById(int id)
        {
            return _repository.GetById(id);
        }

        [HttpGet(Name = "GetAll")]
        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        [HttpPut(Name = "Update")]
        public virtual bool Update(T item)
        {
            return _repository.Update(item);
        }

        [HttpDelete(Name = "Delete")]
        public virtual bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
