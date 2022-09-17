using Microsoft.AspNetCore.Mvc;

namespace ConcyssaWeb.Controllers
{
    public class GestionSolicitudController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
    }
}
