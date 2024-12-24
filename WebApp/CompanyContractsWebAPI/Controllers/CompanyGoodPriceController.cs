using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models.DB;
using CompanyContractsWebAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CompanyContractsWebAPI.Controllers
{
    [ApiController]
    public class CompanyGoodPriceController : ControllerBase
    {
        protected ICompanyGoodPriceRepository _repository;

        public CompanyGoodPriceController(ICompanyGoodPriceRepository repository)
        {
            _repository = repository;
        }

        [HttpPost, Route("[controller]/Create")]
        public virtual IActionResult Create(CompanyGoodPriceDto item)
        {
            try
            {
                var dbItem = Helper.ConvertFromDto<CompanyGoodPrice, CompanyGoodPriceDto>(item);
                var dbResult = _repository.Create(dbItem);
                return Ok(item);
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
                var result = 
                    dbResult
                    .Select(Helper.ConvertToDto<CompanyGoodPrice, CompanyGoodPriceDto>)
                    .OrderBy(o=>o.Company_Id).ThenBy(o=>o.Good_Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet, Route("[controller]/GetByCompanyId")]
        public virtual IActionResult GetByCompanyId(int companyId)
        {
            try
            {
                var dbResult = _repository.GetByCompanyId(companyId);
                var result = 
                    dbResult.Select(Helper.ConvertToDto<CompanyGoodPrice, CompanyGoodPriceDto>)
                    .OrderBy(o=>o.Good_Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet, Route("[controller]/GetByGoodId")]
        public virtual IActionResult GetByGoodId(int goodId)
        {
            try
            {
                var dbResult = _repository.GetByGoodId(goodId);
                var result = dbResult.Select(Helper.ConvertToDto<CompanyGoodPrice, CompanyGoodPriceDto>)
                    .OrderBy(o=>o.Company_Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut, Route("[controller]/Update")]
        public virtual IActionResult Update(CompanyGoodPriceDto item)
        {
            try
            {
                var dbItem = Helper.ConvertFromDto<CompanyGoodPrice, CompanyGoodPriceDto>(item);
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
        public virtual IActionResult Delete(int companyId, int goodId)
        {
            try
            {
                var result = _repository.Delete(companyId, goodId);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
