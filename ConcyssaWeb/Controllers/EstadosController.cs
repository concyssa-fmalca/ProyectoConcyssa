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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<EstadosDTO> lstMonedaDTO = oMonedaDAO.ObtenerEstados(Modulo,BaseDatos, ref message);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int idUsurio= Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<EstadosDTO> lstMonedaDTO = oMonedaDAO.ObtenerEstadosUsuario(IdGiro, idUsurio,BaseDatos, ref message);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int idUsurio = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<EstadosDTO> lstMonedaDTO = oMonedaDAO.ObtenerEstadoUsuario( idUsurio,BaseDatos, ref message);
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
