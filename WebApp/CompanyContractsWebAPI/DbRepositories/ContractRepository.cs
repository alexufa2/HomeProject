using CompanyContractsWebAPI.Models.DB;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class ContractRepository : IContractRepository
    {
        protected ApplicationContext _applicationContext { get; set; }

        public ContractRepository(ApplicationContext applicationContext)
        {
            if (applicationContext == null)
                throw new ArgumentNullException(nameof(applicationContext));

            _applicationContext = applicationContext;
        }

        public Contract Create(Contract item)
        {
            _applicationContext.Contracts.Add(item);
            _applicationContext.SaveChanges();
            return item;
        }

        public IEnumerable<ContractWithNames> GetAll()
        {
            return _applicationContext.Contracts
                   .Join(
                       _applicationContext.Goods,
                       contract => contract.Good_Id,
                       good => good.Id,
                       (contract, good) => new { contract, good })
                   .Join(
                       _applicationContext.Companyes,
                       comb => comb.contract.Company_Id,
                       company => company.Id,
                       (comb, company) => new ContractWithNames
                       {
                           Id = comb.contract.Id,
                           Number = comb.contract.Number,
                           Company_Id = comb.contract.Company_Id,
                           Company_Name = company.Name,
                           Good_Id = comb.contract.Good_Id,
                           Good_Name = comb.good.Name,
                           Good_Count = comb.contract.Good_Count,
                           Done_Sum = comb.contract.Done_Sum,
                           Total_Sum = comb.contract.Total_Sum,
                           Status = comb.contract.Status
                       });
        }

        public ContractWithNames GetById(int id)
        {
            return GetAll().FirstOrDefault(f => f.Id == id);
        }

        public Contract UpdateStatus(int id, string status)
        {
            var originalItem = _applicationContext.Contracts.FirstOrDefault(f => f.Id == id);
            if (originalItem == null)
                return null;

            originalItem.Status = status;
            _applicationContext.SaveChanges();
            return originalItem;
        }

        public bool Delete(int id)
        {
            var originalItem = _applicationContext.Contracts.FirstOrDefault(f => f.Id == id);
            if (originalItem == null)
                return false;

            _applicationContext.Contracts.Remove(originalItem);
            _applicationContext.SaveChanges();
            return true;
        }
    }
}
