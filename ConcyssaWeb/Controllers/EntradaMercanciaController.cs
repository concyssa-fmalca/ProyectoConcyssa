using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class EntradaMercanciaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Listado()
        {
            return View();
        }
        public IActionResult ListadoLogistica()
        {
            return View();
        }
        public string ListarOPDNDT(string EstadoOPDN = "ABIERTO")
        {
            string mensaje_error = "";
            OpdnDAO oOpdnDAO = new OpdnDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OpdnDTO> lstOpdnDTO = oOpdnDAO.ObtenerOPDNxEstado(IdSociedad, ref mensaje_error, EstadoOPDN);
            if (lstOpdnDTO.Count >= 0 && mensaje_error.Length==0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstOpdnDTO.Count;
                oDataTableDTO.iTotalRecords = lstOpdnDTO.Count;
                oDataTableDTO.aaData = (lstOpdnDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
                        
            return mensaje_error;
        }

        public string ListarOPDNDTModalOPCH(string EstadoOPDN = "ABIERTO")
        {
            string mensaje_error = "";
            OpdnDAO oOpdnDAO = new OpdnDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OpdnDTO> lstOpdnDTO = oOpdnDAO.ListarOPDNDTModalOPCH(IdSociedad, ref mensaje_error, EstadoOPDN);
            if (lstOpdnDTO.Count >= 0 && mensaje_error.Length == 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstOpdnDTO.Count;
                oDataTableDTO.iTotalRecords = lstOpdnDTO.Count;
                oDataTableDTO.aaData = (lstOpdnDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }

            return mensaje_error;
        }

        public string ObtenerOPDNDetalle(int IdOPDN)
        {
            string mensaje_error = "";
            OpdnDAO oOpdnDAO = new OpdnDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OPDNDetalle> lstOPDNDetalle = oOpdnDAO.ObtenerDetalleOpdn(IdOPDN, ref mensaje_error);
            if (lstOPDNDetalle.Count > 0)
            {
                //oDataTableDTO.sEcho = 1;
                //oDataTableDTO.iTotalDisplayRecords = lstOPDNDetalle.Count;
                //oDataTableDTO.iTotalRecords = lstOPDNDetalle.Count;
                //oDataTableDTO.aaData = (lstOPDNDetalle);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(lstOPDNDetalle);

            }
            else
            {
                return mensaje_error;

            }
        }

        







        public string UpdateInsertMovimientoEMLogistica(OpdnDTO oOpdnDTO)
        {
            string mensaje_error = "";
            int IdSociedad = Convert.ToInt32((String.IsNullOrEmpty(oOpdnDTO.IdSociedad.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oOpdnDTO.IdSociedad);
            int IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(oOpdnDTO.IdUsuario.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")) : oOpdnDTO.IdUsuario);
            if (IdSociedad==0)
            {
                IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            }
            if (IdUsuario == 0)
            {
                IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            }
            //int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            //int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            oOpdnDTO.IdSociedad = IdSociedad;
            oOpdnDTO.IdUsuario = IdUsuario;
            MovimientoDAO oMovimimientoDAO = new MovimientoDAO();
            OpdnDAO oOpdnDAO = new OpdnDAO();
            int respuesta = oMovimimientoDAO.InsertUpdateMovimientoOPDN(oOpdnDTO, ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {
                for (int i = 0; i < oOpdnDTO.detalles.Count; i++)
                {
                    oOpdnDTO.detalles[i].IdOPDN = respuesta;
                    int respuesta1 = oMovimimientoDAO.InsertUpdateOPDNDetalle(oOpdnDTO.detalles[i], ref mensaje_error);
                }
                oOpdnDAO.UpdateTotalesOPDN(respuesta, ref mensaje_error);
                

            }

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta > 0)
                {
                    return "1";
                }
                else
                {
                    return mensaje_error;
                }
            }
        }


        public string UpdateInsertMovimiento(MovimientoDTO oMovimientoDTO)
        {
            string mensaje_error = "";
            int IdSociedad = Convert.ToInt32((String.IsNullOrEmpty(oMovimientoDTO.IdSociedad.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oMovimientoDTO.IdSociedad);
            int IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(oMovimientoDTO.IdUsuario.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oMovimientoDTO.IdUsuario);

            if(IdSociedad == 0)
            {
                IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            }

            if (IdUsuario == 0)
            {
                IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            }


            //int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            //int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            oMovimientoDTO.IdSociedad = IdSociedad;
            oMovimientoDTO.IdUsuario = IdUsuario;
            MovimientoDAO oMovimimientoDAO = new MovimientoDAO();
            int respuesta = oMovimimientoDAO.InsertUpdateMovimiento(oMovimientoDTO, ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {
                for (int i = 0; i < oMovimientoDTO.detalles.Count; i++)
                {
                    oMovimientoDTO.detalles[i].IdMovimiento = respuesta;
                    int respuesta1 = oMovimimientoDAO.InsertUpdateMovimientoDetalle(oMovimientoDTO.detalles[i], ref mensaje_error);
                }

            }

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta > 0)
                {
                    return "1";
                }
                else
                {
                    return mensaje_error;
                }
            }
        }


        public string GenerarIngresoExtorno(int IdMovimiento)
        {
            string mensaje_error = "";
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            KardexDAO oKardexDAO = new KardexDAO();
            MovimientoDTO oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosDetallexIdMovimiento(IdMovimiento, ref mensaje_error);
            ArticuloStockDTO oArticuloStockDTO = new ArticuloStockDTO();

            if (mensaje_error.ToString().Length == 0)
            {
                int validadStock = 0;
                for (int i = 0; i < oMovimientoDTO.detalles.Count(); i++)
                {
                    oArticuloStockDTO = oKardexDAO.ObtenerArticuloxIdArticuloxIdAlm(oMovimientoDTO.detalles[i].IdArticulo, oMovimientoDTO.detalles[i].IdAlmacen, ref mensaje_error);
                    if (oArticuloStockDTO.Stock < oMovimientoDTO.detalles[i].CantidadBase)
                    {
                        validadStock = 1;
                    }
                }
                if (validadStock == 1)
                {
                    return "No hay suficiente Stock";
                }
                SalidaMercanciaController oSalidaMercanciaController = new SalidaMercanciaController();
                oMovimientoDTO.IdTipoDocumento = 334;
                oMovimientoDTO.Comentario = "EXTORNO DEL INGRESO " + oMovimientoDTO.NombSerie + "-" + +oMovimientoDTO.Correlativo;
                oMovimientoDTO.IdMovimiento = 0;
                oSalidaMercanciaController.UpdateInsertMovimiento(oMovimientoDTO);


                return "ddd";

            }
            else
            {
                return mensaje_error;

            }

        }


        public string ObtenerDatosxIDOPDN(int IdOPDN)
        {
            string mensaje_error = "";
            OpdnDAO oOpdnDAO = new OpdnDAO();
            OpdnDTO oOpdnDTO = oOpdnDAO.ObtenerDatosxIDOPDN(IdOPDN, ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {
                List<OPDNDetalle> lstOPDNDetalle = new List<OPDNDetalle>();
                lstOPDNDetalle = oOpdnDAO.ObtenerDetalleOpdn(IdOPDN, ref mensaje_error);
                oOpdnDTO.detalles = new OPDNDetalle[lstOPDNDetalle.Count()];
                for (int i = 0; i < lstOPDNDetalle.Count; i++)
                {
                    oOpdnDTO.detalles[i] = lstOPDNDetalle[i];


                }


                return JsonConvert.SerializeObject(oOpdnDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
    }
}
