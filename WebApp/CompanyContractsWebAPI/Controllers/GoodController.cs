using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models;

namespace CompanyContractsWebAPI.Controllers
{
    public class GoodController : BaseApiController<Good>
    {
        public GoodController(IRepository<Good> repository) :
            base(repository)
        { }
    }
}
