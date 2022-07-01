using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class ArticuloController:Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerArticulosxSociedad()
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerticulosxSociedad(IdSociedad,ref mensaje_error);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }

        }

        public string UpdateInsertArticulo( ArticuloDTO oArticuloDTO)
        {
            
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            bool respuesta = oArticuloDAO.UpdateInsertArticulo(oArticuloDTO, ref mensaje_error);
            if (respuesta)
            {
                return "Se guardo Correctamente el articulo";
            }
            else
            {
                return mensaje_error;
            }

        }

    }
}
