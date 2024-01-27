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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ConfiguracionDecimalesDAO oConfiguracionDecimalesDAO = new ConfiguracionDecimalesDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ConfiguracionDecimalesDTO> lstConfiguracionDecimalesDTO = oConfiguracionDecimalesDAO.ObtenerConfiguracionDecimales(IdSociedad,BaseDatos);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ConfiguracionDecimalesDAO oConfiguracionDecimalesDAO = new ConfiguracionDecimalesDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int resultado = oConfiguracionDecimalesDAO.UpdateInsertConfiguracionDecimales(configuracionDecimalesDTO, IdSociedad,BaseDatos);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }
    }
}
