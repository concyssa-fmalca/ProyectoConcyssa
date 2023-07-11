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
            UbigeoDAO oUbigeoDAO = new UbigeoDAO();
            List<UbigeoDTO> lstUbigeoDTO = oUbigeoDAO.ObtenerTodosUbigeo();
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
            UbigeoDAO oUbigeoDAO = new UbigeoDAO();
            List<UbigeoDTO> lstUbigeoDTO = oUbigeoDAO.ObtenerDepartamentos();
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
            UbigeoDAO oUbigeoDAO = new UbigeoDAO();
            List<UbigeoDTO> lstUbigeoDTO = oUbigeoDAO.ObtenerProvincias(Departamento);
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
            UbigeoDAO oUbigeoDAO = new UbigeoDAO();
            List<UbigeoDTO> lstUbigeoDTO = oUbigeoDAO.ObtenerDistritos(Provincia);
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
