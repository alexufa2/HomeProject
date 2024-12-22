using CompanyContractsWebAPI.Models.DB;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class ContractRepository: BaseRepository<Contract>
    {
        public ContractRepository(ApplicationContext applicationContext) :
            base(applicationContext)
        { }
    }
}
