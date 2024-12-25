using Microsoft.AspNetCore.Mvc;

namespace CompanyContractsWebAppMVC.Controllers
{
    public class ContractController : BaseController
    {
        public ContractController(ILogger<BaseController> logger) : base(logger)
        {
        }

        public IActionResult DoneList()
        {
            return View();
        }
    }
}
