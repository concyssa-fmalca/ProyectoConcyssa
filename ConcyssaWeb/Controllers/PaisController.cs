using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class PaisController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public string ObtenerPaises()
        {
            PaisDAO oPaisDAO = new PaisDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<PaisDTO> lstPaisDTO = oPaisDAO.ObtenerPaises(BaseDatos);
            if (lstPaisDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstPaisDTO);
            }
            else
            {
                return "error";
            }
        }
    }
}
