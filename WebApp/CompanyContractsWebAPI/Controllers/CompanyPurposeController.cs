using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models;

namespace CompanyContractsWebAPI.Controllers
{
    public class CompanyPurposeController : BaseApiController<CompanyPurpose>
    {
        public CompanyPurposeController(IRepository<CompanyPurpose> repository) :
            base(repository)
        { }
    }
}
