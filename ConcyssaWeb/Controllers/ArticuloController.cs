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
        
        public string ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProductoDT(int IdTipoProducto, int IdAlmacen, int estado = 3)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto(IdSociedad, IdAlmacen, IdTipoProducto, ref mensaje_error, estado);
            DataTableDTO oDataTableDTO = new DataTableDTO();
            if (lstArticuloDTO.Count > 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstArticuloDTO.Count;
                oDataTableDTO.iTotalRecords = lstArticuloDTO.Count;
                oDataTableDTO.aaData = (lstArticuloDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);
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

        public string ObtenerArticulosConStock(int Almacen,int Stock,int TipoItem,int TipoProducto )
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerArticulosRequerimientos(Almacen,Stock, TipoItem, TipoProducto, IdSociedad, ref mensaje_error);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerArticulosConStockSolicitud(int Almacen, int Stock, int TipoItem, int TipoProducto)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerArticulosRequerimientosSolicitud(Almacen, Stock, TipoItem, TipoProducto, IdSociedad, ref mensaje_error);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        

        public string ObtenerArticuloxIdArticuloRequerimiento(int IdArticulo, int IdAlmacen, int TipoItem)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            ArticuloDTO oArticuloDTO = oArticuloDAO.ObtenerArticuloxIdArticuloRequerimiento(IdArticulo,IdAlmacen, ref mensaje_error);
            if (oArticuloDTO!=null)
            {
                return JsonConvert.SerializeObject(oArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string InsertStockArticuloAlmacen(int IdProducto, int IdAlmacen, decimal StockMinimo, decimal StockMaximo, decimal StockAlerta)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            bool respuesta = oArticuloDAO.InsertStockArticuloAlmacen(IdProducto, IdAlmacen, StockMinimo, StockMaximo, StockAlerta, ref mensaje_error);
            if (respuesta)
            {
                return "1";
            }
            else
            {
                return mensaje_error;
            }
        }



        public string ObtenerStockArticuloXAlmacen(int IdArticulo)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            List<StockArticuloAlmacenDTO> oArticuloDTO = oArticuloDAO.ObtenerStockArticuloXAlmacen(IdArticulo, ref mensaje_error);
            if (oArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(oArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

    }
}
