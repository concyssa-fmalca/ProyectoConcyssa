using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class InformeInventarioAlmacenController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }


        public string ListarInventarioxAlmacen(int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino)
        {
            string mensaje_error = "";
            KardexDAO oKardexDAO = new KardexDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloStockDTO> lstArticuloStockDTO = oKardexDAO.ObtenerStockxAlmacen(IdAlmacen, ref mensaje_error);
            if (lstArticuloStockDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloStockDTO);
            }
            else
            {
                return mensaje_error;
            }
            return "error";
        }

    }
}
