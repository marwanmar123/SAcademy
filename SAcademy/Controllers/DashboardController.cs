using Microsoft.AspNetCore.Mvc;

namespace SAcademy.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
