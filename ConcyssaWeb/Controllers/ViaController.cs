using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class ViaController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerVia(int estado = 3)
        {
            string mensaje_error = "";
            ViaDAO oViaDAO = new ViaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ViaDTO> lstViaDTO = oViaDAO.ObtenerVia(IdSociedad, ref mensaje_error, estado);
            if (lstViaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstViaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdVia)
        {
            string mensaje_error = "";
            ViaDAO oViaDAO = new ViaDAO();
            List<ViaDTO> lstCodigoUbsoDTO = oViaDAO.ObtenerDatosxID(IdVia, ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }

        }



        public string UpdateInsertVia(ViaDTO oViaDTO)
        {

            string mensaje_error = "";
            ViaDAO oViaDAO = new ViaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oViaDTO.IdSociedad = IdSociedad;
            int respuesta = oViaDAO.UpdateInsertVia(oViaDTO, ref mensaje_error, Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")));

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta == 1)
                {
                    return "1";
                }
                else
                {
                    return "error";
                }
            }

        }

        public int EliminarVia(int IdVia)
        {
            string mensaje_error = "";
            ViaDAO oViaDAO = new ViaDAO();
            int resultado = oViaDAO.Delete(IdVia, ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
    }
}
