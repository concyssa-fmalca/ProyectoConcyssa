using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class ConfiguracionSociedadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string ObtenerConfiguracionSociedad()
        {
            string mensaje_error = "";
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            List<ConfiguracionSociedadDTO> oConfiguracionSociedadDTO = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(IdSociedad, ref mensaje_error);

            if (mensaje_error.ToString().Length == 0)
            {
                return JsonConvert.SerializeObject(oConfiguracionSociedadDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string UpdateInsertConfiguracionSociedad(ConfiguracionSociedadDTO oConfiguracionSociedadDTO)
        {
            string mensaje_error = "";
            ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int respuesta = oConfiguracionSociedadDAO.UpdateInsertConfiguracionSociedad(oConfiguracionSociedadDTO, IdSociedad, ref mensaje_error);

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


    }
}
