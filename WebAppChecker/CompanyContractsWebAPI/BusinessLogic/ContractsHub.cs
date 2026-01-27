using Microsoft.AspNetCore.SignalR;

namespace CompanyContractsWebAPI.BusinessLogic
{
    public class ContractsHub : Hub
    {
        public async Task SendReloadContracts()
        {
            await this.Clients.All.SendAsync("ReloadContracts");
        }

        public async Task SendReloadContractDoneForContract(int contractId)
        {
            await this.Clients.All.SendAsync("ReloadContractDoneForContract", contractId);
        }
    }
}
