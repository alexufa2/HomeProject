using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models.DB;
using CompanyContractsWebAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CompanyContractsWebAPI.Controllers
{
    public class ContractDoneController : BaseApiController<ContractDone, ContractDoneDto>
    {

        IContractDoneRepository _currentRepository;
        public ContractDoneController(IContractDoneRepository repository) :
            base(repository)
        {
            _currentRepository = repository;
        }

        [HttpGet, Route("[controller]/GetByContractId")]
        public virtual IActionResult GetByContractId(int contractId)
        {
            try
            {
                var dbResult = _currentRepository.GetByContractId(contractId);
                if (dbResult == null)
                    return NotFound();

                var result = dbResult.Select(ConvertToDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
