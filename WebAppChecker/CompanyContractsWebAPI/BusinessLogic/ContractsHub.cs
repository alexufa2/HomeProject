using Microsoft.AspNetCore.SignalR;

namespace CompanyContractsWebAPI.BusinessLogic
{
    public class ContractsHub : Hub
    {
        public async Task SendReloadContracts(string msg)
        {
            await this.Clients.All.SendAsync("ReloadContracts");
        }
    }
}
