using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class TipoPersonaController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public string ObtenerTipoPersonas()
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            TipoPersonaDAO oTipoPersonaDAO = new TipoPersonaDAO();
            List<TipoPersonaDTO> lstTipoPersonaDTO = oTipoPersonaDAO.ObtenerTipoPersonas(BaseDatos);
            if (lstTipoPersonaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstTipoPersonaDTO);
            }
            else
            {
                return "error";
            }
        }
    }
}
