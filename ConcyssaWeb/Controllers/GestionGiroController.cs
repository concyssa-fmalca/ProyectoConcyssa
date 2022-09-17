using Microsoft.AspNetCore.Mvc;

namespace ConcyssaWeb.Controllers
{
    public class GestionGiroController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
    }
}
