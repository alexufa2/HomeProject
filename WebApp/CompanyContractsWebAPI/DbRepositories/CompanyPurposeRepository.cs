using CompanyContractsWebAPI.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class CompanyPurposeRepository : BaseRepository<CompanyPurpose>
    {
        public CompanyPurposeRepository(ApplicationContext applicationContext) :
            base(applicationContext)
        { }
    }
}
