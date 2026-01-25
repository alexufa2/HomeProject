using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models.DB;

namespace CompanyContractsWebAPI.Controllers
{
    public class ContractController : BaseApiController<Contract>
    {

        public ContractController(IRepository<Contract> repository) :
            base(repository)
        { }
    }
}
