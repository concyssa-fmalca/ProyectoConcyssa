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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<CondicionPagoDTO> lstCondicionPagoDTO = oCondicionPagoDAO.ObtenerCondicionPagos(IdSociedad.ToString(),BaseDatos);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int resultado = oCondicionPagoDAO.UpdateInsertCondicionPago(CondicionPagoDTO, IdSociedad.ToString(), Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")),BaseDatos);

            return resultado;

        }

        public string ObtenerDatosxID(int IdCondicionPago)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
            List<CondicionPagoDTO> lstCondicionPagoDTO = oCondicionPagoDAO.ObtenerDatosxID(IdCondicionPago,BaseDatos);

            if (lstCondicionPagoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCondicionPagoDTO);
            }
            else
            {
                return "error";
            }

        }


        public string ObtenerCondicionxProveedor(int IdProveedor)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
            List<CondicionPagoDTO> lstCondicionPagoDTO = oCondicionPagoDAO.ObtenerCondicionxProveedor(IdProveedor,BaseDatos);

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
            int resultado = oCondicionPagoDAO.Delete(IdCondicionPago,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

    }
}
