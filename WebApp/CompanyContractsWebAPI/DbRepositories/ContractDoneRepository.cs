using CompanyContractsWebAPI.Models.DB;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class ContractDoneRepository : BaseRepository<ContractDone>
    {
        public ContractDoneRepository(ApplicationContext applicationContext) :
            base(applicationContext)
        { }
    }
}
