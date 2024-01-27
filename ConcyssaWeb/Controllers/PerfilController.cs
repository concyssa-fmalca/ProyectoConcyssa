using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class PerfilController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerPerfiles()
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            PerfilDAO oPerfilDAO = new PerfilDAO();
            List<PerfilDTO> lstPerfilDTO = oPerfilDAO.ObtenerPerfiles(IdSociedad,BaseDatos,ref mensaje_error);
            if (lstPerfilDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstPerfilDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public int UpdateInsertPerfil(PerfilDTO perfilDTO)
        {
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            perfilDTO.IdSociedad = IdSociedad;
            PerfilDAO oPerfilDAO = new PerfilDAO();
            int resultado = oPerfilDAO.UpdateInsertPerfil(perfilDTO,BaseDatos);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdPerfil)
        {
            PerfilDAO oPerfilDAO = new PerfilDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<PerfilDTO> lstPerfilDTO = oPerfilDAO.ObtenerDatosxID(IdPerfil,BaseDatos);

            if (lstPerfilDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstPerfilDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarPerfil(int IdPerfil)
        {
            PerfilDAO oPerfilDAO = new PerfilDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int resultado = oPerfilDAO.Delete(IdPerfil,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
    }
}
