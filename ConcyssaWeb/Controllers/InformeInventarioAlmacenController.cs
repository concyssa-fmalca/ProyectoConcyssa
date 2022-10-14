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

            DataTableDTO oDataTableDTO = new DataTableDTO();
            if (lstArticuloStockDTO.Count > 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstArticuloStockDTO.Count;
                oDataTableDTO.iTotalRecords = lstArticuloStockDTO.Count;
                oDataTableDTO.aaData = (lstArticuloStockDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);
                //return JsonConvert.SerializeObject(lstArticuloStockDTO);
            }
            else
            {
                return mensaje_error;
            }
            return "error";
        }

    }
}
