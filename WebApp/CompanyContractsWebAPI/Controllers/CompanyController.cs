using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models.DB;
using CompanyContractsWebAPI.Models.DTO;

namespace CompanyContractsWebAPI.Controllers
{
    public class CompanyController : BaseApiController<Company, CompanyDto>
    {
        public CompanyController(IRepository<Company> repository) :
            base(repository)
        { }
    }
}
