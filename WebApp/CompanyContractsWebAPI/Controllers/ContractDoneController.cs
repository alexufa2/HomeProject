using CompanyContractsWebAPI.BusinessLogic;
using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Helpers;
using CompanyContractsWebAPI.Models.DB;
using CompanyContractsWebAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CompanyContractsWebAPI.Controllers
{
    public class ContractDoneController : ControllerBase
    {
        IContractDoneRepository _repository;
        RabbitMqWorker _rabbitMqWorker;

        public ContractDoneController(IContractDoneRepository repository, RabbitMqWorker rabbitMqWorker)
        {
            _repository = repository;
            _rabbitMqWorker = rabbitMqWorker;
        }

        [HttpGet, Route("[controller]/GetByContractId")]
        public virtual IActionResult GetByContractId(int contractId)
        {
            try
            {
                var dbResult = _repository.GetByContractId(contractId);
                if (dbResult == null)
                    return NotFound();

                var result = dbResult.Select(Helper.Convert<ContractDoneDto, ContractDone>);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost, Route("[controller]/Create")]
        public async Task<IActionResult> Create([FromBody]ContractDoneDto item)
        {
            try
            {
                var dbItem = Helper.Convert<ContractDone, ContractDoneDto>(item);
                dbItem.Id = 0;
                dbItem.IntegrationId = Guid.NewGuid();

                var dbResult = _repository.Create(dbItem);
                item.Id = dbResult.Id;

                await _rabbitMqWorker.SendContractDoneCreated(dbResult);

                return Ok(item);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut, Route("[controller]/Update")]
        public virtual async Task<IActionResult> Update([FromBody]ContractDoneDto item)
        {
            try
            {
                var dbItem = Helper.Convert<ContractDone, ContractDoneDto>(item);
                var dbResult = _repository.Update(dbItem);

                if (dbResult == null)
                    return NotFound();

                await _rabbitMqWorker.SendContractDoneUpdated(
                   dbResult.IntegrationId,
                   dbResult.Contract_Id,
                   dbResult.Done_Amount,
                   "Добавлено (изменено)");

                return Ok(item);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete, Route("[controller]/Delete")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            try
            {
                var dbItem = _repository.GetById(id);

                if (dbItem != null)
                {
                    Guid interationId = dbItem.IntegrationId;
                    int contractId = dbItem.Contract_Id;
                    decimal amount = dbItem.Done_Amount;

                    var result = _repository.Delete(id);

                    if (result)
                    {
                        await _rabbitMqWorker.SendContractDoneUpdated(
                            interationId,
                            contractId,
                            amount,
                            "Удалено");
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
