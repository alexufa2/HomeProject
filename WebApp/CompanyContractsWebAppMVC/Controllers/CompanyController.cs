
using Microsoft.AspNetCore.Mvc;

namespace CompanyContractsWebAppMVC.Controllers
{
    public class CompanyController : BaseController
    {
        public CompanyController(ILogger<BaseController> logger) : base(logger)
        {
        }

        public IActionResult Goods()
        {
            return View();
        }
    }
}
