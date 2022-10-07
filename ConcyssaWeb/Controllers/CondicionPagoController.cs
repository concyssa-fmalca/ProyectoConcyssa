using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class CondicionPagoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerCondicionPagos()
        {
            CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<CondicionPagoDTO> lstCondicionPagoDTO = oCondicionPagoDAO.ObtenerCondicionPagos(IdSociedad.ToString());
            if (lstCondicionPagoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCondicionPagoDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertCondicionPago(CondicionPagoDTO CondicionPagoDTO)
        {

            CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int resultado = oCondicionPagoDAO.UpdateInsertCondicionPago(CondicionPagoDTO, IdSociedad.ToString(), Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")));
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdCondicionPago)
        {
            CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
            List<CondicionPagoDTO> lstCondicionPagoDTO = oCondicionPagoDAO.ObtenerDatosxID(IdCondicionPago);

            if (lstCondicionPagoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCondicionPagoDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarCondicionPago(int IdCondicionPago)
        {
            CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
            int resultado = oCondicionPagoDAO.Delete(IdCondicionPago);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

    }
}
