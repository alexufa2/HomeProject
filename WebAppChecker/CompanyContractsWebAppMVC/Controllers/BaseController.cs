using CompanyContractsWebAppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CompanyContractsWebAppMVC.Controllers
{
    public abstract class BaseController: Controller
    {
        protected readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
