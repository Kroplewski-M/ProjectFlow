using Microsoft.AspNetCore.Mvc;

namespace ProjectFlow.Web.Controllers
{
    public class Dashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
