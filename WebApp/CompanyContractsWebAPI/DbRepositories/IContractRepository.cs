using CompanyContractsWebAPI.Models.DB;

namespace CompanyContractsWebAPI.DbRepositories
{
    public interface IContractRepository
    {
        Contract Create(Contract item);

        IEnumerable<ContractWithNames> GetAll();

        ContractWithNames GetById(int id);

        Contract UpdateStatus(int id, string status);

        bool Delete(int id);
    }
}
