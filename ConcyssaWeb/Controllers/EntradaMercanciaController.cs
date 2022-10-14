using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System;

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

            if (oOpdnDTO.IdMoneda == 1)
            {
                oOpdnDTO.TipoCambio = 1;
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
            if (oMovimientoDTO.IdMoneda==1)
            {
                oMovimientoDTO.TipoCambio = 1;
            }
            //oMovimientoDTO.TipoCambio=0
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

        public string ObtenerTipoCambio(string Moneda,string Fecha)
        {
            string mensaje_error;
            string valida = "";
            string Resultado = "1";

            if (Moneda=="1")
            {
                Resultado= "1";
            }
            else
            {
                WebResponse webResponse;
                HttpWebRequest request;
                Uri uri;
                string response;
                try
                {
                   
                    string cadenaUri = "https://api.apis.net.pe/v1/tipo-cambio-sunat?fecha="+ Fecha;
                    uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                    request = (HttpWebRequest)WebRequest.Create(uri);
                    request.ContentType = "application/json";
                    webResponse = request.GetResponse();
                    Stream webStream = webResponse.GetResponseStream();
                    StreamReader responseReader = new StreamReader(webStream);
                    response = responseReader.ReadToEnd();
                    Resultado = response;
                    var ff = JsonConvert.DeserializeObject(response);
                    var ddd = "ee";
                }
                catch (WebException e)
                {
                    using (WebResponse responses = e.Response)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)responses;
                        using (Stream data = responses.GetResponseStream())
                        using (var reader = new StreamReader(data))
                        {
                            mensaje_error = reader.ReadToEnd();

                        }
                    }

                    string err = e.ToString();
                }
            }
        
            return Resultado;

        }
    }
}
