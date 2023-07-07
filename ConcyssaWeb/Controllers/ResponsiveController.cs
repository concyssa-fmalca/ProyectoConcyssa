using Microsoft.AspNetCore.Mvc;

namespace ConcyssaWeb.Controllers
{
    public class ResponsiveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
