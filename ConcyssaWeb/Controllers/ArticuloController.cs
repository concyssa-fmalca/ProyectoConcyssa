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



        public string ListarPrecioProductoProveedor(int IdArticulo)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloPrecioProveedorDTO> lstArticuloPrecioProveedorDTO = oArticuloDAO.ListarPrecioProductoProveedor(IdArticulo,ref mensaje_error);
            if (lstArticuloPrecioProveedorDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloPrecioProveedorDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ListarPrecioProductoProveedorNuevo(int IdArticulo)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            List<ArticuloPrecioProveedorDTO> lstArticuloPrecioProveedorDTO = oArticuloDAO.ListarPrecioProductoProveedorNuevo(IdArticulo, ref mensaje_error);
            if (lstArticuloPrecioProveedorDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloPrecioProveedorDTO);
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
        public string ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProductoConServicios(int TipoItem, int IdTipoProducto, int IdAlmacen, int estado = 3)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProductoConServicios(TipoItem,IdSociedad, IdAlmacen, IdTipoProducto, ref mensaje_error, estado);
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
        public string ListarArticulosxSociedadxAlmacenStockxProductoConServicios(int IdArticulo, int IdAlmacen, int estado = 3)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ListarArticulosxSociedadxAlmacenStockxProductoConServicios(IdSociedad, IdArticulo, IdAlmacen, ref mensaje_error, estado);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ListarArticulosxSociedadxAlmacenStockxProductoActivoFijo(int IdArticulo, int IdAlmacen, int estado = 3)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ListarArticulosxSociedadxAlmacenStockxProductoActivoFijo(IdSociedad, IdArticulo, IdAlmacen, ref mensaje_error, estado);
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

        public string SavePrecioProveedor(int IdArticulo,int IdProveedor, decimal PrecioSoles, decimal PrecioDolares, int IdCondicionPagoProveedor, int numeroentrega)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            bool respuesta = oArticuloDAO.SavePrecioProveedor(IdArticulo,IdProveedor, PrecioSoles, PrecioDolares, IdCondicionPagoProveedor, numeroentrega,IdSociedad, ref mensaje_error);
            if (respuesta)
            {
                return "1";
            }
            else
            {
                return mensaje_error;
            }
        }
        public string SavePrecioProveedorNuevo(int IdArticulo, int IdProveedor, decimal PrecioSoles, decimal PrecioDolares, int IdCondicionPagoProveedor, int numeroentrega,int IdObra)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            bool respuesta = oArticuloDAO.SavePrecioProveedorNuevo(IdArticulo, IdProveedor, PrecioSoles, PrecioDolares, IdCondicionPagoProveedor, numeroentrega, IdSociedad,IdObra, ref mensaje_error);
            if (respuesta)
            {
                return "1";
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

        public string EliminarProductoProveedor(int IdProductoProveedor)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            bool respuesta = oArticuloDAO.EliminarProductoProveedor(IdProductoProveedor, ref mensaje_error);
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
        public string ObtenerArticulosActivoFijo()
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerArticulosActivoFijo(IdSociedad, ref mensaje_error);
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
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            List<StockArticuloAlmacenDTO> oArticuloDTO = oArticuloDAO.ObtenerStockArticuloXAlmacen(IdUsuario,IdArticulo, ref mensaje_error);
            if (oArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(oArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerStockArticuloXPendiente(int IdObra,int IdArticulo)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            List<ArticuloDTO> oArticuloDTO = oArticuloDAO.ObtenerStockArticuloXAlmacenXPendiente(IdObra, IdArticulo, ref mensaje_error);
            if (oArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(oArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerArticulosConStockObras(int IdArticulo)
        {
            string mensaje_error = "";
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerArticulosStockObras(IdArticulo, ref mensaje_error);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

    }
}
