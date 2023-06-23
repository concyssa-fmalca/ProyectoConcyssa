using Microsoft.AspNetCore.Mvc;

namespace ConcyssaWeb.Controllers
{
    public class InformeRendicionController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
    }
}
