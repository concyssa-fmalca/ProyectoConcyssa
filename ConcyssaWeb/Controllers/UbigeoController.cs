using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class UbigeoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }


        public string ObtenerTodosUbigeo()
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            UbigeoDAO oUbigeoDAO = new UbigeoDAO();
            List<UbigeoDTO> lstUbigeoDTO = oUbigeoDAO.ObtenerTodosUbigeo(BaseDatos);
            if (lstUbigeoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUbigeoDTO);
            }
            else
            {
                return "error";
            }
        }

        public string ObtenerDepartamentos()
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            UbigeoDAO oUbigeoDAO = new UbigeoDAO();
            List<UbigeoDTO> lstUbigeoDTO = oUbigeoDAO.ObtenerDepartamentos(BaseDatos);
            if (lstUbigeoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUbigeoDTO);
            }
            else
            {
                return "error";
            }
        }

        public string ObtenerProvincias(string Departamento)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            UbigeoDAO oUbigeoDAO = new UbigeoDAO();
            List<UbigeoDTO> lstUbigeoDTO = oUbigeoDAO.ObtenerProvincias(Departamento,BaseDatos);
            if (lstUbigeoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUbigeoDTO);
            }
            else
            {
                return "error";
            }
        }

        public string ObtenerDistritos(string Provincia)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            UbigeoDAO oUbigeoDAO = new UbigeoDAO();
            List<UbigeoDTO> lstUbigeoDTO = oUbigeoDAO.ObtenerDistritos(Provincia,BaseDatos);
            if (lstUbigeoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUbigeoDTO);
            }
            else
            {
                return "error";
            }
        }
    }
}
