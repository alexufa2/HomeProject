using CompanyContractsWebAPI.Models.DB;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data.Common;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class ContractDoneRepository : BaseRepository<ContractDone>, IContractDoneRepository
    {
        public ContractDoneRepository(ApplicationContext applicationContext) :
            base(applicationContext)
        { }

        public IEnumerable<ContractDone> GetByContractId(int contractId)
        {
            return _applicationContext.ContractDones.Where(w => w.Contract_Id == contractId);
        }
    }
}
