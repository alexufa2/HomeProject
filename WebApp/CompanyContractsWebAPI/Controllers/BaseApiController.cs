using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyContractsWebAPI.Controllers
{
    [ApiController]
    public abstract class BaseApiController<Tdb, Udto> : ControllerBase
        where Tdb : class, IEntityWithId, new()
        where Udto : class, IEntityWithId, new()

    {
        protected IRepository<Tdb> _repository;

        public BaseApiController(IRepository<Tdb> repository)
        {
            _repository = repository;
        }

        [HttpPost, Route("[controller]/Create")]
        public virtual IActionResult Create(Udto item)
        {
            try
            {
                var dbItem = ConvertFromDto(item);
                dbItem.Id = 0;
                var dbResult = _repository.Create(dbItem);
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

                var result = ConvertToDto(dbResult);
                return Ok(result);
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
                var result = dbResult.Select(ConvertToDto).OrderBy(o => o.Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut, Route("[controller]/Update")]
        public virtual IActionResult Update(Udto item)
        {
            try
            {
                var dbItem = ConvertFromDto(item);
                var dbResult = _repository.Update(dbItem);

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

        protected virtual Tdb ConvertFromDto(Udto item)
        {
            return Helper.ConvertFromDto<Tdb, Udto>(item);
        }

        protected virtual Udto ConvertToDto(Tdb item)
        {
            return Helper.ConvertToDto<Tdb, Udto>(item);
        }
    }
}
