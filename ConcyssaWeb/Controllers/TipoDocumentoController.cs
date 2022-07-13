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
            TipoDocumentoDAO oTipoDocumentoDAO = new TipoDocumentoDAO();
            List<TipoDocumentoDTO> lstTipoDocumentoDTO = oTipoDocumentoDAO.ObtenerTipoDocumentos();
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
