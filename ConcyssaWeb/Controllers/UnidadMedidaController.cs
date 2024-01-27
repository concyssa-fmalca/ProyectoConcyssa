using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class UnidadMedidaController : Controller
    {

        public IActionResult Listado()
        {
            return View();
        }


        public string ObtenerUnidadMedidas()
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            UnidadMedidaDAO oUnidadMedidaDAO = new UnidadMedidaDAO();
            List<UnidadMedidaDTO> lstUnidadMedidaDTO = oUnidadMedidaDAO.ObtenerUnidadMedidas(IdSociedad,BaseDatos,ref mensaje_error);
            if (lstUnidadMedidaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUnidadMedidaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ObtenerUnidadMedidasxEstado(int estado)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            UnidadMedidaDAO oUnidadMedidaDAO = new UnidadMedidaDAO();
            List<UnidadMedidaDTO> lstUnidadMedidaDTO = oUnidadMedidaDAO.ObtenerUnidadMedidasxEstado(IdSociedad,estado,BaseDatos,ref mensaje_error);
            if (lstUnidadMedidaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUnidadMedidaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        

        public string UpdateInsertUnidadMedida(UnidadMedidaDTO UnidadMedidaDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            UnidadMedidaDAO oUnidadMedidaDAO = new UnidadMedidaDAO();
            UnidadMedidaDTO.IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            string resultado = oUnidadMedidaDAO.UpdateInsertUnidadMedida(UnidadMedidaDTO,BaseDatos,ref mensaje_error, Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")));
            if (mensaje_error.Length>0)
            {
                return mensaje_error;
            }
            else
            {
                if (resultado != "0")
                {
                    resultado = "1";
                }

            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdUnidadMedida)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            UnidadMedidaDAO oUnidadMedidaDAO = new UnidadMedidaDAO();
            List<UnidadMedidaDTO> lstUnidadMedidaDTO = oUnidadMedidaDAO.ObtenerDatosxID(IdUnidadMedida,BaseDatos,ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }

            if (lstUnidadMedidaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUnidadMedidaDTO);
            }
            else
            {
                return "error";
            }

        }

        public string EliminarUnidadMedida(int IdUnidadMedida)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            UnidadMedidaDAO oUnidadMedidaDAO = new UnidadMedidaDAO();
            string resultado = oUnidadMedidaDAO.Delete(IdUnidadMedida,BaseDatos,ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }

            if (resultado == "0")
            {
                resultado = "1";
            }

            return resultado;
        }

    }
}
