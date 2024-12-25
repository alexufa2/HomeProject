using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models;
using CompanyContractsWebAPI.Models.DB;
using CompanyContractsWebAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyContractsWebAPI.Controllers
{
    public class ContractController : ControllerBase
    {
        protected IContractRepository _repository;

        public ContractController(IContractRepository repository)
        {
            _repository = repository;
        }

        [HttpPost, Route("[controller]/Create")]
        public virtual IActionResult Create([FromBody]CreateContractDto item)
        {
            try
            {
                var dbItem = Helper.ConvertFromDto<Contract, CreateContractDto>(item);
                dbItem.Id = 0;
                dbItem.Done_Sum = 0;
                dbItem.Status = ContractStatuses.New;
                var dbResult = _repository.Create(dbItem);

                return Ok(item);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet, Route("[controller]/Getall")]
        public virtual IActionResult Getall()
        {
            try
            {
                var dbItems = _repository.GetAll();
                var result = dbItems.Select(ConvertToDto);
                return Ok(result);
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
                var dbItem = _repository.GetById(id);
                var dto = ConvertToDto(dbItem);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut, Route("[controller]/Cancel")]
        public virtual IActionResult Cancel(int id)
        {
            try
            {
                var item  = _repository.UpdateStatus(id, ContractStatuses.Canceled);
                if (item == null)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        private ContractDto ConvertToDto(ContractWithNames item)
        {
            var dto = Helper.ConvertToDto<ContractWithNames, ContractDto>(item);
            dto.StatusName = ContractStatuses.GetStatusName(dto.Status);
            return dto;
        }
    }
}
