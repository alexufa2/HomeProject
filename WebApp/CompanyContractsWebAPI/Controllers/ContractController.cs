using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models.DB;
using CompanyContractsWebAPI.Models.DTO;

namespace CompanyContractsWebAPI.Controllers
{
    public class ContractController : BaseApiController<Contract, ContractDto>
    {
        public ContractController(IRepository<Contract> repository) :
            base(repository)
        { }
    }
}
