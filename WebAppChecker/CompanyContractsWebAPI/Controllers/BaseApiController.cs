using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Helpers;
using CompanyContractsWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyContractsWebAPI.Controllers
{
    [ApiController]
    public abstract class BaseApiController<Tdb> : ControllerBase
        where Tdb : class, IEntityWithId, new()

    {
        protected IRepository<Tdb> _repository;

        public BaseApiController(IRepository<Tdb> repository)
        {
            _repository = repository;
        }

        [HttpPost, Route("[controller]/Create")]
        public virtual IActionResult Create(Tdb item)
        {
            try
            {
                item.Id = 0;
                var dbResult = _repository.Create(item);
                item.Id = dbResult.Id;
                return Ok(item);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet, Route("[controller]/GetById")]
        public virtual IActionResult GetById(int id)
        {
            try
            {
                var dbResult = _repository.GetById(id);
                if (dbResult == null)
                    return NotFound();

                return Ok(dbResult);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet, Route("[controller]/GetAll")]
        public virtual IActionResult GetAll()
        {
            try
            {
                var dbResult = _repository.GetAll();
                var result = dbResult.OrderBy(o => o.Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut, Route("[controller]/Update")]
        public virtual IActionResult Update(Tdb item)
        {
            try
            {
                var dbResult = _repository.Update(item);

                if (dbResult == null)
                    return NotFound();

                return Ok(item);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete, Route("[controller]/Delete")]
        public virtual IActionResult Delete(int id)
        {
            try
            {
                var result = _repository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
