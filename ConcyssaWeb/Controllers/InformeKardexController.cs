using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace ConcyssaWeb.Controllers
{
    public class InformeKardexController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ListarKardex(int IdArticulo, int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino)
        {
            string mensaje_error = "";
            KardexDAO oKardexDAO = new KardexDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<KardexDTO> lstKardexDTO = oKardexDAO.ObtenerKardex(IdSociedad, IdArticulo, IdAlmacen, FechaInicio, FechaTermino, ref mensaje_error);
            if (lstKardexDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstKardexDTO);
            }
            else
            {
                return mensaje_error;
            }
            return "error";
        }


        public string ListarKardexDT(int IdArticulo, int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino)
        
        {
            string mensaje_error = "";
            KardexDAO oKardexDAO = new KardexDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<KardexDTO> lstKardexDTO = oKardexDAO.ObtenerKardex(IdSociedad, IdArticulo, IdAlmacen, FechaInicio, FechaTermino, ref mensaje_error);
            if (lstKardexDTO.Count > 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstKardexDTO.Count;
                oDataTableDTO.iTotalRecords = lstKardexDTO.Count;
                oDataTableDTO.aaData = (lstKardexDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);
                //return JsonConvert.SerializeObject(lstKardexDTO);
            }
            else
            {
                return mensaje_error;
            }
            return "error";
        }


    }
}
