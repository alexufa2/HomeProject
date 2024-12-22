using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models.DB;
using CompanyContractsWebAPI.Models.DTO;

namespace CompanyContractsWebAPI.Controllers
{
    public class ContractDoneController : BaseApiController<ContractDone, ContractDoneDto>
    {
        public ContractDoneController(IRepository<ContractDone> repository) :
            base(repository)
        { }
    }
}
