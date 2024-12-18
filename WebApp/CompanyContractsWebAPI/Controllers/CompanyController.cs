using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models;

namespace CompanyContractsWebAPI.Controllers
{
    public class CompanyController : BaseApiController<Company, CreateCompany>
    {
        public CompanyController(IRepository<Company> repository) :
            base(repository)
        { }
    }
}
