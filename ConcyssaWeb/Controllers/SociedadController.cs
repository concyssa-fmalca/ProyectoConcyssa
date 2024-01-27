

using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class SociedadController:Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerSociedades()
        {
            string mensajeError = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            SociedadDAO oSociedadDAO = new SociedadDAO();
            List<SociedadDTO> lstSociedadDTO = oSociedadDAO.ObtenerSociedades(BaseDatos,ref mensajeError);
            if (lstSociedadDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSociedadDTO);
            }
            else
            {
                return mensajeError;
            }

        }

        public string UpdateInserSociedad(SociedadDTO sociedadDTO)
        {
            string error_mensaje = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            SociedadDAO SociedadDAO = new SociedadDAO();
            int resultado = SociedadDAO.UpdateInsertSociedad(sociedadDTO,BaseDatos,ref error_mensaje,Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")));
            if (error_mensaje.Length>0)
            {
                return error_mensaje;
            }
            else
            {
                if (resultado != 0)
                {
                    resultado = 1;
                }
            }
            

            return resultado.ToString();
        }

        public string ObtenerDatosxID(int IdSociedad)
        {
            SociedadDAO oSociedadDAO = new SociedadDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<SociedadDTO> lstSociedadDTO = oSociedadDAO.ObtenerDatosxID(IdSociedad,BaseDatos);

            if (lstSociedadDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSociedadDTO);
            }
            else
            {
                return "error";
            }

        }
    }
}
