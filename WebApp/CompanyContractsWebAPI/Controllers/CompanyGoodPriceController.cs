using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyContractsWebAPI.Controllers
{
    [ApiController]
    public class CompanyGoodPriceController: ControllerBase
    {
        protected ICompanyGoodPriceRepository _repository;

        public CompanyGoodPriceController(ICompanyGoodPriceRepository repository)
        {
            _repository = repository;
        }

        [HttpPost, Route("[controller]/Create")]
        public virtual IActionResult Create(CompanyGoodPrice item)
        {
            try
            {
                var result = _repository.Create(item);
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
                var result = _repository.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut, Route("[controller]/Update")]
        public virtual IActionResult Update(CompanyGoodPrice item)
        {
            try
            {
                var result = _repository.Update(item);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete, Route("[controller]/Delete")]
        public virtual IActionResult Delete(CompanyGoodPrice item)
        {
            try
            {
                var result = _repository.Delete(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
