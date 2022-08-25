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

        public string ObtenerArticulosxSociedad(int estado = 3)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerticulosxSociedad(IdSociedad,ref mensaje_error,estado);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ListarArticulosxSociedadxAlmacenStock(int IdAlmacen,int estado = 3)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ListarArticulosxSociedadxAlmacenStock(IdSociedad,IdAlmacen, ref mensaje_error, estado);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        
        public string ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto(int IdTipoProducto,int IdAlmacen, int estado = 3)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto(IdSociedad, IdAlmacen, IdTipoProducto, ref mensaje_error, estado);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ListarArticulosxSociedadxAlmacenStockxProducto(int IdArticulo,int IdAlmacen, int estado = 3)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ListarArticulosxSociedadxAlmacenStockxProducto(IdSociedad, IdArticulo, IdAlmacen, ref mensaje_error, estado);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string ObtenerDatosxID(int IdArticulo)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerDatosxID(IdArticulo, ref mensaje_error);

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
            oArticuloDTO.IdSociedad = IdSociedad;
            bool respuesta = oArticuloDAO.UpdateInsertArticulo(oArticuloDTO, ref mensaje_error);
            if (respuesta)
            {
                return "1";
            }
            else
            {
                return mensaje_error;
            }
        }
        
        public string EliminarArticulo(ArticuloDTO oArticuloDTO)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oArticuloDTO.IdSociedad = IdSociedad;
            bool respuesta = oArticuloDAO.ELiminarArticulo(oArticuloDTO, ref mensaje_error);
            if (respuesta)
            {
                return "1";
            }
            else
            {
                return mensaje_error;
            }
        }
    }
}
