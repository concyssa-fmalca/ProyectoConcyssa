

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
            SociedadDAO oSociedadDAO = new SociedadDAO();
            List<SociedadDTO> lstSociedadDTO = oSociedadDAO.ObtenerSociedades(ref mensajeError);
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
            SociedadDAO SociedadDAO = new SociedadDAO();
            int resultado = SociedadDAO.UpdateInsertSociedad(sociedadDTO,ref error_mensaje);
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
            List<SociedadDTO> lstSociedadDTO = oSociedadDAO.ObtenerDatosxID(IdSociedad);

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
