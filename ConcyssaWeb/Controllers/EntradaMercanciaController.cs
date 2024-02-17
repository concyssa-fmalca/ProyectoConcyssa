using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System;
using FE;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Helpers;
using System.Runtime.Intrinsics.X86;
using ISAP;
using System.ComponentModel;
using OfficeOpenXml;
using System.Linq;

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
        public string ListarOPDNDT(int IdBase,DateTime FechaInicio,DateTime FechaFin, string EstadoOPDN = "ABIERTO")
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpdnDAO oOpdnDAO = new OpdnDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OpdnDTO> lstOpdnDTO = oOpdnDAO.ObtenerOPDNxEstado(IdBase, IdSociedad,FechaInicio,FechaFin,BaseDatos,ref mensaje_error, EstadoOPDN,IdUsuario);
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

        public string ListarOPDNDTModalOPCH(int IdProveedor,string EstadoOPDN = "ABIERTO")
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpdnDAO oOpdnDAO = new OpdnDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OpdnDTO> lstOpdnDTO = oOpdnDAO.ListarOPDNDTModalOPCH(IdSociedad,BaseDatos,ref mensaje_error, EstadoOPDN, IdUsuario);
            List<OpdnDTO> newListOpdnDTO = new List<OpdnDTO>();


            if (IdProveedor > 0)
            {
                for (int i = 0; i < lstOpdnDTO.Count; i++)
                {
                    if (lstOpdnDTO[i].IdProveedor == IdProveedor)
                    {
                        newListOpdnDTO.Add(lstOpdnDTO[i]);
                    }                
                }
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = newListOpdnDTO.Count;
                oDataTableDTO.iTotalRecords = newListOpdnDTO.Count;
                oDataTableDTO.aaData = (newListOpdnDTO);
                return JsonConvert.SerializeObject(oDataTableDTO);
            }


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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpdnDAO oOpdnDAO = new OpdnDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OPDNDetalle> lstOPDNDetalle = oOpdnDAO.ObtenerDetalleOpdn(IdOPDN,BaseDatos,ref mensaje_error);
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


        //public string ObtenerOPDNDetalleModal(int IdOPDN)
        //{
        //    string mensaje_error = "";
        //    OpdnDAO oOpdnDAO = new OpdnDAO();
        //    int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
        //    DataTableDTO oDataTableDTO = new DataTableDTO();
        //    List<OPDNDetalle> lstOPDNDetalle = oOpdnDAO.ObtenerDetalleOpdnModal(IdOPDN,BaseDatos,ref mensaje_error);
        //    if (lstOPDNDetalle.Count > 0)
        //    {
        //        //oDataTableDTO.sEcho = 1;
        //        //oDataTableDTO.iTotalDisplayRecords = lstOPDNDetalle.Count;
        //        //oDataTableDTO.iTotalRecords = lstOPDNDetalle.Count;
        //        //oDataTableDTO.aaData = (lstOPDNDetalle);
        //        //return oDataTableDTO;
        //        return JsonConvert.SerializeObject(lstOPDNDetalle);

        //    }
        //    else
        //    {
        //        return mensaje_error;

        //    }
        //}







        public string UpdateInsertMovimientoEMLogistica(OpdnDTO oOpdnDTO)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32((String.IsNullOrEmpty(oOpdnDTO.IdSociedad.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oOpdnDTO.IdSociedad);
            int IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(oOpdnDTO.IdUsuario.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")) : oOpdnDTO.IdUsuario);
            if (IdSociedad == 0)
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
            int respuesta = oMovimimientoDAO.InsertUpdateMovimientoOPDN(oOpdnDTO,BaseDatos,ref mensaje_error);
            int respuesta1 = 0;
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {
                for (int i = 0; i < oOpdnDTO.detalles.Count; i++)
                {
                    oOpdnDTO.detalles[i].IdOPDN = respuesta;
                    respuesta1 = oMovimimientoDAO.InsertUpdateOPDNDetalle(oOpdnDTO.detalles[i],BaseDatos,ref mensaje_error);
                    int respuesta2 = oMovimimientoDAO.InsertUpdateOPDNDetalleCuadrilla(respuesta1, oOpdnDTO.detalles[i],BaseDatos,ref mensaje_error);
                }

                if (oOpdnDTO.AnexoDetalle != null)
                {
                    for (int i = 0; i < oOpdnDTO.AnexoDetalle.Count; i++)
                    {
                        oOpdnDTO.AnexoDetalle[i].ruta = "/Anexos/" + oOpdnDTO.AnexoDetalle[i].NombreArchivo;
                        oOpdnDTO.AnexoDetalle[i].IdSociedad = oOpdnDTO.IdSociedad;
                        oOpdnDTO.AnexoDetalle[i].Tabla = "Opdn";
                        oOpdnDTO.AnexoDetalle[i].IdTabla = respuesta;

                        oMovimimientoDAO.InsertAnexoMovimiento(oOpdnDTO.AnexoDetalle[i],BaseDatos,ref mensaje_error);
                    }
                }





                oOpdnDAO.UpdateTotalesOPDN(respuesta,BaseDatos,ref mensaje_error);


            }

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta > 0)
                {
                    return respuesta.ToString();
                }
                else
                {
                    return mensaje_error;
                }
            }
        }

        public string UpdateInsertMovimientoEMLogisticaString(string JsonDatosEnviar)
        {
            JsonDatosEnviar = JsonDatosEnviar.Remove(JsonDatosEnviar.Length - 1, 1);
            JsonDatosEnviar = JsonDatosEnviar.Remove(0, 1);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            OpdnDTO oOpdnDTO = JsonConvert.DeserializeObject<OpdnDTO>(JsonDatosEnviar, settings);
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32((String.IsNullOrEmpty(oOpdnDTO.IdSociedad.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oOpdnDTO.IdSociedad);
            int IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(oOpdnDTO.IdUsuario.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")) : oOpdnDTO.IdUsuario);
            if (IdSociedad == 0)
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
            int respuesta = oMovimimientoDAO.InsertUpdateMovimientoOPDN(oOpdnDTO, BaseDatos, ref mensaje_error);
            int respuesta1 = 0;
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {
                for (int i = 0; i < oOpdnDTO.detalles.Count; i++)
                {
                    oOpdnDTO.detalles[i].IdOPDN = respuesta;
                    respuesta1 = oMovimimientoDAO.InsertUpdateOPDNDetalle(oOpdnDTO.detalles[i], BaseDatos, ref mensaje_error);
                    int respuesta2 = oMovimimientoDAO.InsertUpdateOPDNDetalleCuadrilla(respuesta1, oOpdnDTO.detalles[i], BaseDatos, ref mensaje_error);
                }

                if (oOpdnDTO.AnexoDetalle != null)
                {
                    for (int i = 0; i < oOpdnDTO.AnexoDetalle.Count; i++)
                    {
                        oOpdnDTO.AnexoDetalle[i].ruta = "/Anexos/" + oOpdnDTO.AnexoDetalle[i].NombreArchivo;
                        oOpdnDTO.AnexoDetalle[i].IdSociedad = oOpdnDTO.IdSociedad;
                        oOpdnDTO.AnexoDetalle[i].Tabla = "Opdn";
                        oOpdnDTO.AnexoDetalle[i].IdTabla = respuesta;

                        oMovimimientoDAO.InsertAnexoMovimiento(oOpdnDTO.AnexoDetalle[i], BaseDatos, ref mensaje_error);
                    }
                }





                oOpdnDAO.UpdateTotalesOPDN(respuesta, BaseDatos, ref mensaje_error);


            }

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta > 0)
                {
                    return respuesta.ToString();
                }
                else
                {
                    return mensaje_error;
                }
            }
        }


        public string UpdateInsertMovimiento(MovimientoDTO oMovimientoDTO,string BaseDatos = "")
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
            if(BaseDatos == "")
            {
              BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            }
            int respuesta = oMovimimientoDAO.InsertUpdateMovimiento(oMovimientoDTO,BaseDatos,ref mensaje_error);
            int respuesta1 = 0;
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {
                for (int i = 0; i < oMovimientoDTO.detalles.Count; i++)
                {
                    oMovimientoDTO.detalles[i].IdMovimiento = respuesta;
                    oMovimientoDTO.detalles[i].IdMovimientoDetalle = 0;
                    respuesta1 = oMovimimientoDAO.InsertUpdateMovimientoDetalle(oMovimientoDTO.detalles[i],0 ,BaseDatos,ref mensaje_error);
                    int respuesta2 = oMovimimientoDAO.InsertUpdateMovimientoDetalleCuadrilla(respuesta1, oMovimientoDTO.detalles[i],BaseDatos,ref mensaje_error);


                }
                if (oMovimientoDTO.AnexoDetalle!=null)
                {
                    for (int i = 0; i < oMovimientoDTO.AnexoDetalle.Count; i++)
                    {
                        oMovimientoDTO.AnexoDetalle[i].ruta = "/Anexos/" + oMovimientoDTO.AnexoDetalle[i].NombreArchivo;
                        oMovimientoDTO.AnexoDetalle[i].IdSociedad = oMovimientoDTO.IdSociedad;
                        oMovimientoDTO.AnexoDetalle[i].Tabla = "Movimiento";
                        oMovimientoDTO.AnexoDetalle[i].IdTabla = respuesta;

                        oMovimimientoDAO.InsertAnexoMovimiento(oMovimientoDTO.AnexoDetalle[i],BaseDatos,ref mensaje_error);
                    }
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
                    return respuesta.ToString();
                }
                else
                {
                    return mensaje_error;
                }
            }
        }

        public string UpdateInsertMovimientoDesdeString(string JsonDatosEnviar)
        {
            JsonDatosEnviar = JsonDatosEnviar.Remove(JsonDatosEnviar.Length - 1, 1);
            JsonDatosEnviar = JsonDatosEnviar.Remove(0, 1);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            MovimientoDTO oMovimientoDTO = JsonConvert.DeserializeObject<MovimientoDTO>(JsonDatosEnviar,settings);

            string mensaje_error = "";
            if (oMovimientoDTO.IdMoneda == 1)
            {
                oMovimientoDTO.TipoCambio = 1;
            }
            //oMovimientoDTO.TipoCambio=0
            int IdSociedad = Convert.ToInt32((String.IsNullOrEmpty(oMovimientoDTO.IdSociedad.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oMovimientoDTO.IdSociedad);
            int IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(oMovimientoDTO.IdUsuario.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oMovimientoDTO.IdUsuario);

            if (IdSociedad == 0)
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int respuesta = oMovimimientoDAO.InsertUpdateMovimiento(oMovimientoDTO,BaseDatos,ref mensaje_error);
            int respuesta1 = 0;
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {
                for (int i = 0; i < oMovimientoDTO.detalles.Count; i++)
                {
                    oMovimientoDTO.detalles[i].IdMovimiento = respuesta;
                    oMovimientoDTO.detalles[i].IdMovimientoDetalle = 0;
                    respuesta1 = oMovimimientoDAO.InsertUpdateMovimientoDetalle(oMovimientoDTO.detalles[i], 0,BaseDatos,ref mensaje_error);
                    int respuesta2 = oMovimimientoDAO.InsertUpdateMovimientoDetalleCuadrilla(respuesta1, oMovimientoDTO.detalles[i],BaseDatos,ref mensaje_error);

                }

                if (oMovimientoDTO.AnexoDetalle != null)
                {
                    for (int i = 0; i < oMovimientoDTO.AnexoDetalle.Count; i++)
                    {
                        oMovimientoDTO.AnexoDetalle[i].ruta = "/Anexos/" + oMovimientoDTO.AnexoDetalle[i].NombreArchivo;
                        oMovimientoDTO.AnexoDetalle[i].IdSociedad = oMovimientoDTO.IdSociedad;
                        oMovimientoDTO.AnexoDetalle[i].Tabla = "Movimiento";
                        oMovimientoDTO.AnexoDetalle[i].IdTabla = respuesta;

                        oMovimimientoDAO.InsertAnexoMovimiento(oMovimientoDTO.AnexoDetalle[i],BaseDatos,ref mensaje_error);
                    }
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
                    return respuesta.ToString();
                }
                else
                {
                    return mensaje_error;
                }
            }
        }


        public string GenerarIngresoExtorno(int IdMovimiento, int Serie, DateTime FechaDoc, DateTime FechaCont)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            KardexDAO oKardexDAO = new KardexDAO();
            MovimientoDTO oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosDetallexIdMovimiento(IdMovimiento,BaseDatos,ref mensaje_error);
            ArticuloStockDTO oArticuloStockDTO = new ArticuloStockDTO();

            string MensajeNoStock = "";

            if (mensaje_error.ToString().Length == 0)
            {
                int validadStock = 0;
                for (int i = 0; i < oMovimientoDTO.detalles.Count(); i++)
                {
                    oArticuloStockDTO = oKardexDAO.ObtenerArticuloxIdArticuloxIdAlm(oMovimientoDTO.detalles[i].IdArticulo, oMovimientoDTO.detalles[i].IdAlmacen,BaseDatos,ref mensaje_error);
                    if (oArticuloStockDTO.Stock < oMovimientoDTO.detalles[i].CantidadBase)
                    {
                        validadStock = 1;
                        MensajeNoStock += oMovimientoDTO.detalles[i].CodigoArticulo + "-" + oMovimientoDTO.detalles[i].DescripcionArticulo+" </br> ";
                    }
                }
                if (validadStock == 1)
                {
                    return "No hay suficiente Stock </br> "+ MensajeNoStock;
                }
                SalidaMercanciaController oSalidaMercanciaController = new SalidaMercanciaController();
                oMovimientoDTO.IdTipoDocumento = 334;
                oMovimientoDTO.Comentario = "EXTORNO DEL INGRESO " + oMovimientoDTO.NombSerie + "-" + +oMovimientoDTO.Correlativo;
                oMovimientoDTO.IdMovimiento = 0;
                oMovimientoDTO.IdSerie = Serie;
                oMovimientoDTO.FechaDocumento = FechaDoc;
                oMovimientoDTO.FechaContabilizacion = FechaCont;
               
                oSalidaMercanciaController.UpdateInsertMovimiento(oMovimientoDTO,BaseDatos);


                return "1";

            }
            else
            {
                return mensaje_error;

            }

        }


        public string ObtenerDatosxIDOPDN(int IdOPDN)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpdnDAO oOpdnDAO = new OpdnDAO();
            OpdnDTO oOpdnDTO = oOpdnDAO.ObtenerDatosxIDOPDN(IdOPDN,BaseDatos,ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {
                List<OPDNDetalle> lstOPDNDetalle = new List<OPDNDetalle>();
                lstOPDNDetalle = oOpdnDAO.ObtenerDetalleOpdn(IdOPDN,BaseDatos,ref mensaje_error);
                oOpdnDTO.detalles = new OPDNDetalle[lstOPDNDetalle.Count()];
                for (int i = 0; i < lstOPDNDetalle.Count; i++)
                {
                    oOpdnDTO.detalles[i] = lstOPDNDetalle[i];
                }


                List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
                lstAnexoDTO = oOpdnDAO.ObtenerAnexoOpdn(IdOPDN,BaseDatos,ref mensaje_error);
                oOpdnDTO.AnexoDetalle = new AnexoDTO[lstAnexoDTO.Count()];
                for (int i = 0; i < lstAnexoDTO.Count; i++)
                {
                    oOpdnDTO.AnexoDetalle[i] = lstAnexoDTO[i];
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

        public string GenerarGuiaElectronica()
        {
            //APIGuiaRemisionSunat oAPIGuiaRemisionSunat = new APIGuiaRemisionSunat();
            //GRSunatDTO oGRSunatDTO = new GRSunatDTO();
            //oAPIGuiaRemisionSunat.SendGuiaRemision(oGRSunatDTO);

            return "";
        }

        public string ObtenerMotivoTraslado()
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MotivoTrasladoDAO oMotivoTrasladoDAO = new MotivoTrasladoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<MotivoTrasladoDTO> lstMotivoTrasladoDTO = oMotivoTrasladoDAO.ObtenerMotivoTraslado(IdSociedad,BaseDatos,ref mensaje_error);
            if (lstMotivoTrasladoDTO.Count >= 0 && mensaje_error.Length == 0)
            {
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(lstMotivoTrasladoDTO);

            }
            return mensaje_error;
        }


        public string GuardarFile(IFormFile file)
        {
            List<string> Archivos = new List<string>();
            if (file != null && file.Length > 0)
            {
                try
                {
                    string dir = "wwwroot/Anexos/" + file.FileName;
                    if (Directory.Exists(dir))
                    {
                        ViewBag.Message = "Archivo ya existe";
                    }
                    else
                    {
                        string filePath = Path.Combine(dir, Path.GetFileName(file.FileName));
                        using (Stream fileStream = new FileStream(dir, FileMode.Create, FileAccess.Write))
                        {
                            file.CopyTo(fileStream);
                            Archivos.Add(file.FileName);
                        }

                        ViewBag.Message = "Anexo guardado correctamente";
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error:" + ex.Message.ToString();
                    throw;
                }
            }
            return JsonConvert.SerializeObject(Archivos);
        }


        public string GenerarReporte(string NombreReporte, string Formato, int Id,string BaseDatos)
        {
            if (BaseDatos == "" || BaseDatos == null)
            {
                BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            }

            RespuestaDTO oRespuestaDTO = new RespuestaDTO();
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string requestData;
            string response;
            string mensaje_error;
            WebServiceDTO oWebServiceDTO = new WebServiceDTO();
            oWebServiceDTO.Formato = Formato;
            oWebServiceDTO.NombreReporte = NombreReporte;
            oWebServiceDTO.Id = Id;
            requestData = JsonConvert.SerializeObject(oWebServiceDTO);


            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&Id=" + Id + "&BaseDatos=" + BaseDatos;
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReportCrystal";
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReportCrystal";
                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);

                request.Method = "POST";
                //request.ContentType = "application/json;charset=utf-8";
                request.ContentType = "application/x-www-form-urlencoded";


                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);

                requestWriter.Write(strNew);


                requestWriter.Close();



                webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                response = responseReader.ReadToEnd();

                //var Resultado = response;
                //XmlSerializer xmlSerializer = new XmlSerializer(response);
                var rr = 33;
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(response);
                var dd = "";

                oRespuestaDTO.Result = xDoc.ChildNodes[1].ChildNodes[0].InnerText;
                oRespuestaDTO.Mensaje = xDoc.ChildNodes[1].ChildNodes[1].InnerText;
                oRespuestaDTO.Base64ArchivoPDF = xDoc.ChildNodes[1].ChildNodes[2].InnerText;

                return JsonConvert.SerializeObject(oRespuestaDTO);
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



            //WebResponse webResponse;
            //HttpWebRequest request;
            //Uri uri;
            //string response;
            //try
            //{

            //    string cadenaUri = "https://api.apis.net.pe/v1/tipo-cambio-sunat?fecha=" + Fecha;
            //    uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
            //    request = (HttpWebRequest)WebRequest.Create(uri);
            //    request.ContentType = "application/json";
            //    webResponse = request.GetResponse();
            //    Stream webStream = webResponse.GetResponseStream();
            //    StreamReader responseReader = new StreamReader(webStream);
            //    response = responseReader.ReadToEnd();
            //    Resultado = response;
            //    var ff = JsonConvert.DeserializeObject(response);
            //    var ddd = "ee";
            //}
            //catch (WebException e)
            //{
            //    using (WebResponse responses = e.Response)
            //    {
            //        HttpWebResponse httpResponse = (HttpWebResponse)responses;
            //        using (Stream data = responses.GetResponseStream())
            //        using (var reader = new StreamReader(data))
            //        {
            //            mensaje_error = reader.ReadToEnd();

            //        }
            //    }

            //    string err = e.ToString();
            //}

            return "";
        }

        public string UpdateOPDN(OpdnDTO oOpdnDTO)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpdnDAO oOpdnDAO = new OpdnDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int respuesta = oOpdnDAO.UpdateOPDN(IdUsuario, oOpdnDTO,BaseDatos,ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta == 1)
                {
                    return "1";
                }
                else
                {
                    return "error";
                }
            }

        }
        public string ValidarExtorno(int IdOPDN)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string Valida = "0";
            string mensaje_error = "";
            OpdnDAO oOpdnDAO = new OpdnDAO();
            Valida = oOpdnDAO.ValidaExtorno(IdOPDN,BaseDatos,ref mensaje_error);

            return Valida;

        }

        public int ValidarFacturaOPDN(int IdOPDN)
        {
            int Valida = 1;
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpdnDAO oOpdnDAO = new OpdnDAO();
            Valida = oOpdnDAO.ValidarOPDNTieneFactura(IdOPDN,BaseDatos,ref mensaje_error);

            return Valida;

        }
        public string GenerarOPDNExtorno(int IdOPDN)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpdnDAO oOpdnDAO = new OpdnDAO();
            List<string> SinStock = new List<string>();
           List<OPDNDetalle> lstoOpdnDTO = oOpdnDAO.ObtenerStockParaExtornoOPDN(IdOPDN,BaseDatos,ref mensaje_error);
            for (int i = 0; i < lstoOpdnDTO.Count; i++)
            {
                if( lstoOpdnDTO[i].Resta == -1) SinStock.Add( "No hay Stock para " + lstoOpdnDTO[i].DescripcionArticulo);
            }
            if(SinStock.Count != 0) {
                return JsonConvert.SerializeObject(SinStock);
            }
            else
            {
                return "bien";
            }

        }
        public string ExtornoConfirmado(int IdOPDn, string EsServicio)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpdnDAO oOpdnDAO = new OpdnDAO();
            int respuesta = oOpdnDAO.ExtornoConfirmado(IdOPDn, EsServicio,BaseDatos,ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta == 1)
                {
                    return "1";
                }
                else
                {
                    return "error";
                }
            }

        }

        public string ValidaTipoProductoOPDN(int ArticuloMuestra)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpdnDAO oOpdnDAO = new OpdnDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OpdnDTO> lstOpdnDTO = oOpdnDAO.ValidaTipoProductoOPDN(ArticuloMuestra,BaseDatos,ref mensaje_error);
            if (lstOpdnDTO.Count >= 0 && mensaje_error.Length == 0)
            {
                
                return JsonConvert.SerializeObject(lstOpdnDTO);

            }

            return mensaje_error;
        }
        public string GenerarReporteOPDN(string NombreReporte, string Formato, int IdOPDN,string BaseDatos)
        {
            if (BaseDatos == "" || BaseDatos == null)
            {
                BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            }

            RespuestaDTO oRespuestaDTO = new RespuestaDTO();
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string requestData;
            string response;
            string mensaje_error;
            WebServiceDTO oWebServiceDTO = new WebServiceDTO();
            oWebServiceDTO.Formato = Formato;
            oWebServiceDTO.NombreReporte = NombreReporte;
            oWebServiceDTO.IdOPDN = IdOPDN;
          
            requestData = JsonConvert.SerializeObject(oWebServiceDTO);


            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&IdOPDN=" + IdOPDN + "&BaseDatos=" + BaseDatos;
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReporteEntregaMercancias";
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteEntregaMercancias";
                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);

                request.Method = "POST";
                //request.ContentType = "application/json;charset=utf-8";
                request.ContentType = "application/x-www-form-urlencoded";


                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);

                requestWriter.Write(strNew);


                requestWriter.Close();



                webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                response = responseReader.ReadToEnd();

                //var Resultado = response;
                //XmlSerializer xmlSerializer = new XmlSerializer(response);
                var rr = 33;
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(response);
                var dd = "";

                oRespuestaDTO.Result = xDoc.ChildNodes[1].ChildNodes[0].InnerText;
                oRespuestaDTO.Mensaje = xDoc.ChildNodes[1].ChildNodes[1].InnerText;
                oRespuestaDTO.Base64ArchivoPDF = xDoc.ChildNodes[1].ChildNodes[2].InnerText;

                return JsonConvert.SerializeObject(oRespuestaDTO);
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

            return "";
        }
        public string UpdateCuadrillasOPDN(OPDNDetalle oOPDNDetalle)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpdnDAO oOpdnDAO = new OpdnDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int respuesta = oOpdnDAO.UpdateCuadrillas(oOPDNDetalle,BaseDatos,ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta == 1)
                {
                    return "1";
                }
                else
                {
                    return "error";
                }
            }

        }

        public string ProcesarExcel(string archivo,int IdAlmacen, int ClaseArticulo, int TipoArticulo)
        {
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            string filePath = "wwwroot/Anexos/" + archivo;

            List<ExcelDTO> Datos = new List<ExcelDTO>();

            // Leer el archivo Excel
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {

                var worksheet = package.Workbook.Worksheets[0];

                int rowCount = worksheet.Dimension.Rows;

                List<string> codigosExistentes = new List<string>();

                try
                {
                    for (int row = 2; row <= rowCount; row++)
                    {

                        ExcelDTO excelDTO = new ExcelDTO();
                        excelDTO.Codigo = (worksheet.Cells[row, 1].Value.ToString().Trim());
                        excelDTO.Articulo = (worksheet.Cells[row, 2].Value.ToString().Trim());
                        excelDTO.Cantidad = decimal.Parse(worksheet.Cells[row, 3].Value.ToString().Trim());
                        excelDTO.PrecioUnitario = decimal.Parse(worksheet.Cells[row, 4].Value.ToString().Trim());
                        Datos.Add(excelDTO);
                    }

                    if (Datos.Count > 1000)
                    {
                        return "El Archivo no Debe contener mas de 1000 articulos";
                    }

                    ArticuloDAO oArticuloDAO = new ArticuloDAO();
                    List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();                           
                    List<string> ArticulosObtenidos = new List<string>();
                    if (ClaseArticulo == 1)
                    {
                        lstArticuloDTO = oArticuloDAO.ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto(IdSociedad, IdAlmacen, TipoArticulo,BaseDatos,ref mensaje_error, 1);
                    }
                    else
                    {
                        lstArticuloDTO = oArticuloDAO.ObtenerArticulosActivoFijo(1,BaseDatos,ref mensaje_error);
                    }

                    string NoExisten = "";
                              

                    for (int i = 0; i < lstArticuloDTO.Count; i++)
                    {
                        codigosExistentes.Add(lstArticuloDTO[i].Codigo);
                    }


                    for (int i = 0; i < Datos.Count; i++)
                    {
                        if (!codigosExistentes.Contains(Datos[i].Codigo))
                        {
                            NoExisten += "No Existe el Codigo " + Datos[i].Codigo + " En su Almacen </br>";
                        }
                    }

                    if(NoExisten != "")
                    {
                        return NoExisten;
                    }

                    return JsonConvert.SerializeObject(Datos);

                }
                catch (Exception e)
                {
                    return mensaje_error = "error";
                }

            }



        }


        public string GenerarResalida(int IdEntrada, int Serie, DateTime FechaDoc, DateTime FechaCont)
        {
            string mensaje_error= "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            ArticuloStockDTO oArticuloStockDTO = new ArticuloStockDTO();
            MovimientoDTO oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosDetallexIdMovimiento(IdEntrada,BaseDatos,ref mensaje_error);
            KardexDAO oKardexDAO = new KardexDAO();

            int validadStock = 0;
            for (int i = 0; i < oMovimientoDTO.detalles.Count(); i++)
            {
                oArticuloStockDTO = oKardexDAO.ObtenerArticuloxIdArticuloxIdAlm(oMovimientoDTO.detalles[i].IdArticulo, oMovimientoDTO.detalles[i].IdAlmacen,BaseDatos,ref mensaje_error);
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
            oMovimientoDTO.IdTipoDocumento = 332;
            oMovimientoDTO.Comentario = "RESALIDA DEL DOC " + oMovimientoDTO.NombSerie + "-" + +oMovimientoDTO.Correlativo;
            oMovimientoDTO.IdMovimiento = 0;
            oMovimientoDTO.IdSerie = Serie;
            oMovimientoDTO.FechaDocumento = FechaDoc;
            oMovimientoDTO.FechaContabilizacion = FechaCont;

            oSalidaMercanciaController.UpdateInsertMovimiento(oMovimientoDTO);


            return "1";


        }
        public int ValidarDevolucionConSalida(int IdMovimiento)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            MovimientoDTO oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosDetallexIdMovimiento(IdMovimiento,BaseDatos,ref mensaje_error);
            if(oMovimientoDTO.EsDevolucionAdm == 2)
            {
                return 1;
            }
            return 0;
        }
    }
}
