using CompanyContractsWebAPI.Models.DB;

namespace CompanyContractsWebAPI.DbRepositories
{
    public interface IContractDoneRepository : IRepository<ContractDone>
    {
        public IEnumerable<ContractDone> GetByContractId(int contractId);
    }
}
