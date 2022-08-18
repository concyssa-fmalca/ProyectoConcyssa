using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class TipoDocumentoOperacionController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerTipoDocumentoOperacion(int estado = 3)
        {
            string mensaje_error = "";
            TipoDocumentoOperacionDAO oTipoDocumentoOperacionDAO = new TipoDocumentoOperacionDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<TipoDocumentoOperacionDTO> lstTipoDocumentoOperacionDTO = oTipoDocumentoOperacionDAO.ObtenerTipoDocumentoOperacion(IdSociedad, ref mensaje_error, estado);
            if (lstTipoDocumentoOperacionDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstTipoDocumentoOperacionDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
    }
}
