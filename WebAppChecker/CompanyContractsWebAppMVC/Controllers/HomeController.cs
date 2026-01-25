using Microsoft.AspNetCore.Mvc;

namespace CompanyContractsWebAppMVC.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ILogger<BaseController> logger) : base(logger)
        {
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
