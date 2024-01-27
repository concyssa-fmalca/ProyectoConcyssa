using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class TipoDocumentoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerTipoDocumentos()
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            TipoDocumentoDAO oTipoDocumentoDAO = new TipoDocumentoDAO();
            List<TipoDocumentoDTO> lstTipoDocumentoDTO = oTipoDocumentoDAO.ObtenerTipoDocumentos(BaseDatos);
            if (lstTipoDocumentoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstTipoDocumentoDTO);
            }
            else
            {
                return "error";
            }
        }
    }
}
