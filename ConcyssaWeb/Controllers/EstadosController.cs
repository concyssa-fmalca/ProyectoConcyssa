using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace ConcyssaWeb.Controllers
{
    public class EstadosController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerEstados(int Modulo=0 )
        {
            EstadosDAO oMonedaDAO = new EstadosDAO();
            string message = "";
            List<EstadosDTO> lstMonedaDTO = oMonedaDAO.ObtenerEstados(Modulo, ref message);
            if (lstMonedaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstMonedaDTO);
            }
            else
            {
                return "error";
            }
        }
        public string ObtenerEstadosUsuario(int IdGiro )
        {
            EstadosDAO oMonedaDAO = new EstadosDAO();
            string message = "";
            int idUsurio= Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<EstadosDTO> lstMonedaDTO = oMonedaDAO.ObtenerEstadosUsuario(IdGiro, idUsurio, ref message);
            if (lstMonedaDTO.Count > 0 || message=="" )
            {
                return JsonConvert.SerializeObject(lstMonedaDTO);
            }
            else
            {
                return "error";
            }
        }
        public string ObtenerEstadoUsuario()
        {
            EstadosDAO oMonedaDAO = new EstadosDAO();
            string message = "";
            int idUsurio = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<EstadosDTO> lstMonedaDTO = oMonedaDAO.ObtenerEstadoUsuario( idUsurio, ref message);
            if (lstMonedaDTO.Count > 0 )
            {
                return JsonConvert.SerializeObject(lstMonedaDTO[0]);
            }
            else
            {
                return "error";
            }
        }




    }
}
