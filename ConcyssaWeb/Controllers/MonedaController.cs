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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<MonedaDTO> lstMonedaDTO = oMonedaDAO.ObtenerMonedas(IdSociedad.ToString(),BaseDatos);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int resultado = oMonedaDAO.UpdateInsertMoneda(MonedaDTO, IdSociedad.ToString(), Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")),BaseDatos);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdMoneda)
        {
            MonedaDAO oMonedaDAO = new MonedaDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<MonedaDTO> lstMonedaDTO = oMonedaDAO.ObtenerDatosxID(IdMoneda,BaseDatos);

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int resultado = oMonedaDAO.Delete(IdMoneda,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public string ValidarMonedaBase(int IdMoneda)
        {
            MonedaDAO oMonedaDAO = new MonedaDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<MonedaDTO> lstMonedaDTO = oMonedaDAO.ValidarMonedaBase(IdMoneda,BaseDatos);

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
