using Microsoft.AspNetCore.Mvc;

namespace ConcyssaWeb.Controllers
{
    public class GestionSolicitudCompraController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
    }
}
