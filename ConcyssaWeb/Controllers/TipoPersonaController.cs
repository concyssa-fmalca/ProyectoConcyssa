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
            TipoPersonaDAO oTipoPersonaDAO = new TipoPersonaDAO();
            List<TipoPersonaDTO> lstTipoPersonaDTO = oTipoPersonaDAO.ObtenerTipoPersonas();
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
