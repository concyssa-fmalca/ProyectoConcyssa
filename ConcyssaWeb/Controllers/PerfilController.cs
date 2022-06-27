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
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            PerfilDAO oPerfilDAO = new PerfilDAO();
            List<PerfilDTO> lstPerfilDTO = oPerfilDAO.ObtenerPerfiles(IdSociedad,ref mensaje_error);
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

            PerfilDAO oPerfilDAO = new PerfilDAO();
            int resultado = oPerfilDAO.UpdateInsertPerfil(perfilDTO);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdPerfil)
        {
            PerfilDAO oPerfilDAO = new PerfilDAO();
            List<PerfilDTO> lstPerfilDTO = oPerfilDAO.ObtenerDatosxID(IdPerfil);

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
            int resultado = oPerfilDAO.Delete(IdPerfil);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
    }
}
