using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class ConfiguracionDecimalesController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerConfiguracionDecimales()
        {
            ConfiguracionDecimalesDAO oConfiguracionDecimalesDAO = new ConfiguracionDecimalesDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ConfiguracionDecimalesDTO> lstConfiguracionDecimalesDTO = oConfiguracionDecimalesDAO.ObtenerConfiguracionDecimales(IdSociedad);
            if (lstConfiguracionDecimalesDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstConfiguracionDecimalesDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertConfiguracionDecimales(ConfiguracionDecimalesDTO configuracionDecimalesDTO)
        {

            ConfiguracionDecimalesDAO oConfiguracionDecimalesDAO = new ConfiguracionDecimalesDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int resultado = oConfiguracionDecimalesDAO.UpdateInsertConfiguracionDecimales(configuracionDecimalesDTO, IdSociedad);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }
    }
}
