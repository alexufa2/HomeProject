using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models.DB;
using CompanyContractsWebAPI.Models.DTO;

namespace CompanyContractsWebAPI.Controllers
{
    public class GoodController : BaseApiController<Good, GoodDto>
    {
        public GoodController(IRepository<Good> repository) :
            base(repository)
        { }
    }
}
