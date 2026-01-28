using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Helpers;
using CompanyContractsWebAPI.Models.DB;
using CompanyContractsWebAPI.Models.DTO;
using Microsoft.AspNetCore.SignalR;

namespace CompanyContractsWebAPI.BusinessLogic
{
    public class ContractsHub : Hub
    {
        private IRepositoryFactory _repositoryFactory;
        public ContractsHub(IRepositoryFactory repositoryFactory) : base()
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task GetContracts()
        {
            Contract[] contracts = GetAllContracts();
            await this.Clients.Caller.SendAsync("RecieveContracts", contracts);
        }

        private Contract[] GetAllContracts()
        {
            var repository = _repositoryFactory.CreateRepository<IRepository<Contract>>();
            return repository.GetAll().ToArray();
        }

        public async Task SendReloadContracts()
        {
            Contract[] contracts = GetAllContracts();
            await this.Clients.All.SendAsync("ReloadContracts", contracts);
        }

        public async Task GetContractById(int id)
        {
            var repository = _repositoryFactory.CreateRepository<IRepository<Contract>>();
            var contract = repository.GetById(id);
            await this.Clients.Caller.SendAsync("RecieveContractById", contract);
        }

        public async Task GetContractDoneByContractId(int contractId)
        {
            var contractDoneDtoArr = GetContractDoneDtoArray(contractId);
            await this.Clients.Caller.SendAsync("ReciveContractDoneByContractId", contractDoneDtoArr);
        }

        private ContractDoneDto[] GetContractDoneDtoArray(int contractId)
        {
            var repository = _repositoryFactory.CreateRepository<IContractDoneRepository>();
            var dbResult = repository.GetByContractId(contractId);

            if (dbResult == null)
                return Array.Empty<ContractDoneDto>();

            return dbResult.Select(Helper.Convert<ContractDoneDto, ContractDone>).ToArray();
        }


        public async Task SendReloadContractDoneForContract(int contractId)
        {
            await this.Clients.All.SendAsync("ReloadContractDoneForContract", contractId);
        }
    }
}
