using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class MonedaController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerMonedas()
        {
            MonedaDAO oMonedaDAO = new MonedaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<MonedaDTO> lstMonedaDTO = oMonedaDAO.ObtenerMonedas(IdSociedad.ToString());
            if (lstMonedaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstMonedaDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertMoneda(MonedaDTO MonedaDTO)
        {

            MonedaDAO oMonedaDAO = new MonedaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int resultado = oMonedaDAO.UpdateInsertMoneda(MonedaDTO, IdSociedad.ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdMoneda)
        {
            MonedaDAO oMonedaDAO = new MonedaDAO();
            List<MonedaDTO> lstMonedaDTO = oMonedaDAO.ObtenerDatosxID(IdMoneda);

            if (lstMonedaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstMonedaDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarMoneda(int IdMoneda)
        {
            MonedaDAO oMonedaDAO = new MonedaDAO();
            int resultado = oMonedaDAO.Delete(IdMoneda);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public string ValidarMonedaBase(int IdMoneda)
        {
            MonedaDAO oMonedaDAO = new MonedaDAO();
            List<MonedaDTO> lstMonedaDTO = oMonedaDAO.ValidarMonedaBase(IdMoneda);

            if (lstMonedaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstMonedaDTO);
            }
            else
            {
                return "error";
            }

        }
    }
}
