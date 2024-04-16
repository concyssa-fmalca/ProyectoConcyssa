using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Xml;
using FE;
using System.Text;

namespace ConcyssaWeb.Controllers
{
    public class FacturaProveedorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listado()
        {
            return View();
        }
        public IActionResult ReporteFacturaServicio()
        {
            return View();
        }
        public IActionResult ReportePendienteEnvioSAP()
        {
            return View();
        }

        // public string ListarOPCHDT(int IdBase,string EstadoOPCH = "ABIERTO")
        public string ListarOPCHDT(int IdObra,int IdTipoRegistro,int IdSemana,string EstadoOPCH = "ABIERTO")
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpchDAO oOpchDAO = new OpchDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OpchDTO> lstOpchDTO = oOpchDAO.ObtenerOPCHxEstado(IdObra,IdTipoRegistro,IdSemana,BaseDatos,ref mensaje_error);


            ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            var configuracion = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(1, BaseDatos, ref mensaje_error);
            string BaseDatosSAP = configuracion[0].NombreBDSAP;
            IntregadorV1DAO intregadorV1DAO = new IntregadorV1DAO();
            if (lstOpchDTO.Count >= 0 && mensaje_error.Length==0)
            {

                for (int i = 0; i < lstOpchDTO.Count; i++)
                {

                    lstOpchDTO[i].DocNumCont = intregadorV1DAO.ObtenerDocNumOPCH(lstOpchDTO[i].RUCProveedor, lstOpchDTO[i].NumSerieTipoDocumentoRef, BaseDatosSAP, ref mensaje_error);
                }

                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstOpchDTO.Count;
                oDataTableDTO.iTotalRecords = lstOpchDTO.Count;
                oDataTableDTO.aaData = (lstOpchDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            else
            {
                return mensaje_error;

            }
        }

        public string ObtenerTotalesTipoRegistro(int IdObra, int IdTipoRegistro, int IdSemana, string EstadoOPCH = "ABIERTO")
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpchDAO oOpchDAO = new OpchDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

     
            List<OpchDTO> lstOpchDTO = oOpchDAO.ObtenerOPCHxEstado(IdObra, IdTipoRegistro, IdSemana, BaseDatos, ref mensaje_error);

            TotalesOPCH oTotalesOPCH = new TotalesOPCH();

            for (int i = 0; i < lstOpchDTO.Count; i++)
            {
                if (lstOpchDTO[i].IdMoneda == 1)
                {
                    oTotalesOPCH.SumaSoles += lstOpchDTO[i].Total;
                }
                else
                {
                    oTotalesOPCH.SumaDolares += lstOpchDTO[i].Total;
                }
            }

            return JsonConvert.SerializeObject(oTotalesOPCH);
           
        }

        public string ListarOPCHDTxProveedor(int IdProveedor, string NumSerie)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpchDAO oOpchDAO = new OpchDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OpchDTO> lstOpchDTO = oOpchDAO.ObtenerOPCHXProveedor(IdProveedor, NumSerie, BaseDatos, ref mensaje_error);


            ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            var configuracion = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(1, BaseDatos, ref mensaje_error);
            string BaseDatosSAP = configuracion[0].NombreBDSAP;
            IntregadorV1DAO intregadorV1DAO = new IntregadorV1DAO();
            if (lstOpchDTO.Count >= 0 && mensaje_error.Length == 0)
            {

                for (int i = 0; i < lstOpchDTO.Count; i++)
                {

                    lstOpchDTO[i].DocNumCont = intregadorV1DAO.ObtenerDocNumOPCH(lstOpchDTO[i].RUCProveedor, lstOpchDTO[i].NumSerieTipoDocumentoRef, BaseDatosSAP, ref mensaje_error);
                }

                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstOpchDTO.Count;
                oDataTableDTO.iTotalRecords = lstOpchDTO.Count;
                oDataTableDTO.aaData = (lstOpchDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            else
            {
                return mensaje_error;

            }
        }

        public string ObtenerTotalesXProveedor(int IdProveedor, string NumSerie)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpchDAO oOpchDAO = new OpchDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OpchDTO> lstOpchDTO = oOpchDAO.ObtenerOPCHXProveedor(IdProveedor, NumSerie, BaseDatos, ref mensaje_error);

            TotalesOPCH oTotalesOPCH = new TotalesOPCH();

            for (int i = 0; i < lstOpchDTO.Count; i++)
            {
                if (lstOpchDTO[i].IdMoneda == 1)
                {
                    oTotalesOPCH.SumaSoles += lstOpchDTO[i].Total;
                }
                else
                {
                    oTotalesOPCH.SumaDolares += lstOpchDTO[i].Total;
                }
            }

            return JsonConvert.SerializeObject(oTotalesOPCH);

        }

        public string ListarOPCHDTModal(int IdProveedor,int IdObra,string EstadoOPCH = "ABIERTO")
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpchDAO oOpchDAO = new OpchDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OpchDTO> lstOpchDTO = oOpchDAO.ObtenerOPCHxEstadoModal(IdSociedad, IdProveedor, IdObra,BaseDatos, ref mensaje_error, EstadoOPCH, IdUsuario);
            if (lstOpchDTO.Count >= 0 && mensaje_error.Length == 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstOpchDTO.Count;
                oDataTableDTO.iTotalRecords = lstOpchDTO.Count;
                oDataTableDTO.aaData = (lstOpchDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            else
            {
                return mensaje_error;

            }
        }

        public string ObtenerOPCHDetalle(int IdOPCH)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpchDAO oOpchDAO = new OpchDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OPCHDetalle> lstOPCHDetalle = oOpchDAO.ObtenerDetalleOpch(IdOPCH,BaseDatos,ref mensaje_error);
            if (lstOPCHDetalle.Count > 0)
            {
                //oDataTableDTO.sEcho = 1;
                //oDataTableDTO.iTotalDisplayRecords = lstOPCHDetalle.Count;
                //oDataTableDTO.iTotalRecords = lstOPCHDetalle.Count;
                //oDataTableDTO.aaData = (lstOPCHDetalle);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(lstOPCHDetalle);

            }
            else
            {
                return mensaje_error;

            }
        }

        
        public string UpdateInsertMovimientoFacturaProveedor(OpchDTO oOpchDTO)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32((String.IsNullOrEmpty(oOpchDTO.IdSociedad.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oOpchDTO.IdSociedad);
            int IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(oOpchDTO.IdUsuario.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")) : oOpchDTO.IdUsuario);
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

            oOpchDTO.IdSociedad = IdSociedad;
            oOpchDTO.IdUsuario = IdUsuario;
            MovimientoDAO oMovimimientoDAO = new MovimientoDAO();
            OpchDAO oOpchDAO = new OpchDAO();
            int respuesta = oMovimimientoDAO.InsertUpdateMovimientoOPCH(oOpchDTO,BaseDatos,ref mensaje_error);
            int respuesta1 = 0;
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {             
                if (oOpchDTO.AnexoDetalle!=null)
                {
                    for (int i = 0; i < oOpchDTO.AnexoDetalle.Count; i++)
                    {
                        oOpchDTO.AnexoDetalle[i].ruta = "/Anexos/" + oOpchDTO.AnexoDetalle[i].NombreArchivo;
                        oOpchDTO.AnexoDetalle[i].IdSociedad = oOpchDTO.IdSociedad;
                        oOpchDTO.AnexoDetalle[i].Tabla = "Opch";
                        oOpchDTO.AnexoDetalle[i].IdTabla = respuesta;

                        oMovimimientoDAO.InsertAnexoMovimiento(oOpchDTO.AnexoDetalle[i],BaseDatos,ref mensaje_error);
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
                    string tabla = oOpchDTO.TablaOrigen;
                    int tipo = oOpchDTO.IdTipoDocumento;
                    if (tabla != "Entrega" && tipo == 18 )
                    {
                        enviarCorreo(respuesta);
                    }
                  
                    return respuesta.ToString();
                }
                else
                {
                    return respuesta.ToString();
                }
            }
        }

        public string UpdateInsertMovimientoFacturaProveedorString(string JsonDatosEnviar)
        {
            JsonDatosEnviar = JsonDatosEnviar.Remove(JsonDatosEnviar.Length - 1, 1);
            JsonDatosEnviar = JsonDatosEnviar.Remove(0, 1);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            OpchDTO oOpchDTO = JsonConvert.DeserializeObject<OpchDTO>(JsonDatosEnviar, settings);

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32((String.IsNullOrEmpty(oOpchDTO.IdSociedad.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oOpchDTO.IdSociedad);
            int IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(oOpchDTO.IdUsuario.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")) : oOpchDTO.IdUsuario);
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

            oOpchDTO.IdSociedad = IdSociedad;
            oOpchDTO.IdUsuario = IdUsuario;
            MovimientoDAO oMovimimientoDAO = new MovimientoDAO();
            OpchDAO oOpchDAO = new OpchDAO();
            int respuesta = oMovimimientoDAO.InsertUpdateMovimientoOPCH(oOpchDTO, BaseDatos, ref mensaje_error);
            int respuesta1 = 0;
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {
                if (oOpchDTO.AnexoDetalle != null)
                {
                    for (int i = 0; i < oOpchDTO.AnexoDetalle.Count; i++)
                    {
                        oOpchDTO.AnexoDetalle[i].ruta = "/Anexos/" + oOpchDTO.AnexoDetalle[i].NombreArchivo;
                        oOpchDTO.AnexoDetalle[i].IdSociedad = oOpchDTO.IdSociedad;
                        oOpchDTO.AnexoDetalle[i].Tabla = "Opch";
                        oOpchDTO.AnexoDetalle[i].IdTabla = respuesta;

                        oMovimimientoDAO.InsertAnexoMovimiento(oOpchDTO.AnexoDetalle[i], BaseDatos, ref mensaje_error);
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
                    string tabla = oOpchDTO.TablaOrigen;
                    int tipo = oOpchDTO.IdTipoDocumento;
                    if (tabla != "Entrega" && tipo == 18)
                    {
                        enviarCorreo(respuesta);
                    }

                    return respuesta.ToString();
                }
                else
                {
                    return respuesta.ToString();
                }
            }
        }

        public string ObtenerDatosxIdOpch(int IdOpch)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpchDAO oOpchDAO = new OpchDAO();
            OpchDTO oOpchDTO = oOpchDAO.ObtenerDatosxIdOpch(IdOpch,BaseDatos,ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {
                List<OPCHDetalle> lstOPCHDetalle = new List<OPCHDetalle>();
                lstOPCHDetalle = oOpchDAO.ObtenerDetalleOpch(IdOpch,BaseDatos,ref mensaje_error);
                oOpchDTO.detalles = new OPCHDetalle[lstOPCHDetalle.Count()];
                for (int i = 0; i < lstOPCHDetalle.Count; i++)
                {
                    oOpchDTO.detalles[i] = lstOPCHDetalle[i];
                }

                List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
                lstAnexoDTO = oOpchDAO.ObtenerAnexoOpch(IdOpch,BaseDatos,ref mensaje_error);
                oOpchDTO.AnexoDetalle = new AnexoDTO[lstAnexoDTO.Count()];
                for (int i = 0; i < lstAnexoDTO.Count; i++)
                {
                    oOpchDTO.AnexoDetalle[i] = lstAnexoDTO[i];
                }

                return JsonConvert.SerializeObject(oOpchDTO);
            }
            else
            {
                return mensaje_error;
            }
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
        public string ObtenerTipoCambio(string Moneda, string Fecha)
        {
            string mensaje_error;
            string valida = "";
            string Resultado = "1";

            if (Moneda == "1")
            {
                Resultado = "1";
            }
            else
            {
                WebResponse webResponse;
                HttpWebRequest request;
                Uri uri;
                string response;
                try
                {

                    string cadenaUri = "https://api.apis.net.pe/v1/tipo-cambio-sunat?fecha=" + Fecha;
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
        public string UpdateOPCH(OpchDTO oOpchDTO)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpchDAO oOpchDAO = new OpchDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int respuesta = oOpchDAO.UpdateOPCH(IdUsuario, oOpchDTO,BaseDatos,ref mensaje_error);

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
        public string UpdateCuadrillas(OPCHDetalle oOPCHDetalle)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpchDAO oOpchDAO = new OpchDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int respuesta = oOpchDAO.UpdateCuadrillas(oOPCHDetalle,BaseDatos,ref mensaje_error);

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
        public string ObtenerOrigenesFactura(int IdOPCH)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpchDAO oOpchDAO = new OpchDAO();
         
            List<OpchDTO> lstOpchDTO = oOpchDAO.ObtenerOrigenesFactura(IdOPCH,BaseDatos,ref mensaje_error);
            if (lstOpchDTO.Count >= 0 && mensaje_error.Length == 0)
            {
              
                return JsonConvert.SerializeObject(lstOpchDTO);

            }
            else
            {
                return mensaje_error;

            }
        }
        public string ValidarStockExtorno(int IdOPCH)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpchDAO oOpchDAO = new OpchDAO();
            List<string> SinStock = new List<string>();
            List<OPCHDetalle> lstoOpchDTO = oOpchDAO.ObtenerStockParaExtornoOPCH(IdOPCH,BaseDatos,ref mensaje_error);
            for (int i = 0; i < lstoOpchDTO.Count; i++)
            {
                if (lstoOpchDTO[i].Resta == -1) SinStock.Add("No hay Stock para " + lstoOpchDTO[i].DescripcionArticulo);
            }
            if (SinStock.Count != 0)
            {
                return JsonConvert.SerializeObject(SinStock);
            }
            else
            {
                return "bien";
            }

        }
        public string ExtornoConfirmado(int IdOPCH, string EsServicio, string TablaOrigen)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpchDAO oOpchDAO = new OpchDAO();
            int respuesta = oOpchDAO.ExtornoConfirmado(IdOPCH, EsServicio, TablaOrigen,BaseDatos,ref mensaje_error);

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

        public string ListarOPCHxIdObra(int IdObra, DateTime FechaInicio, DateTime FechaFin)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpchDAO oOpchDAO = new OpchDAO();

            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OpchDTO> lstOpchDTO = oOpchDAO.ObtenerOPCHxIdObra(IdObra, FechaInicio, FechaFin,BaseDatos,ref mensaje_error);
            if (lstOpchDTO.Count >= 0 && mensaje_error.Length == 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstOpchDTO.Count;
                oDataTableDTO.iTotalRecords = lstOpchDTO.Count;
                oDataTableDTO.aaData = (lstOpchDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            else
            {
                return mensaje_error;

            }
        }


        public int enviarCorreo(int IdOpch) {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            try
            {

                string base64;
                string html = @"";
                string mensaje_error = "";

                int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

                string body;
                body = "BASE PRUBAS";


                OpchDAO oOpchDAO = new OpchDAO();
                OpchDTO oOpchDTO = oOpchDAO.ObtenerDatosxIdOpch(IdOpch, BaseDatos, ref mensaje_error);
                if (mensaje_error.ToString().Length == 0)
                {
                    List<OPCHDetalle> lstOPCHDetalle = new List<OPCHDetalle>();
                    lstOPCHDetalle = oOpchDAO.ObtenerDetalleOpch(IdOpch, BaseDatos, ref mensaje_error);
                    oOpchDTO.detalles = new OPCHDetalle[lstOPCHDetalle.Count()];
                    for (int i = 0; i < lstOPCHDetalle.Count; i++)
                    {
                        oOpchDTO.detalles[i] = lstOPCHDetalle[i];
                    }

                }

                //solo para pruebas
                //oPedidoDTO.EmailProveedor = "fperez@smartcode.pe";

                string msge = "";
                string from = "concyssa.smc@gmail.com";
                string correo = from;
                string password = "tlbvngkvjcetzunr";
                string displayName = "CONCYSSA NOTIFICACION: FACTURA " + oOpchDTO.NombSerie + "-" + oOpchDTO.Correlativo + " MOVIÓ STOCK";
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, displayName);

                AlmacenDAO oAlmacenDAO = new AlmacenDAO();
                List<AlmacenDTO> lstAlmacenDTO = oAlmacenDAO.ObtenerDatosxID(oOpchDTO.IdAlmacen, BaseDatos, ref mensaje_error);

                string CorreoAlmacen = lstAlmacenDTO[0].CorreoAlmacen;

                if (CorreoAlmacen != "")
                {
                    mail.To.Add(lstAlmacenDTO[0].CorreoAlmacen);

                }

                
                
                //mail.To.Add("fperez@smartcode.pe");
                //mail.To.Add("fmalca@concyssa.com");
                //mail.To.Add("compras@concyssa.com ");
                mail.Subject = "CONCYSSA NOTIFICACION: FACTURA " + oOpchDTO.NombSerie + "-" + oOpchDTO.Correlativo +" MOVIÓ STOCK";
                //mail.CC.Add(new MailAddress("camala145@gmail.com"));
                mail.Body = TemplateEmail(oOpchDTO);

                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Aquí debes sustituir tu servidor SMTP y el puerto
                client.Credentials = new NetworkCredential(from, password);
                client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false
               
                //Attachment att = new Attachment(new MemoryStream(archivopdf), "OrdenCompra.pdf");
                //mail.Attachments.Add(att);
                client.Send(mail);
            }
            catch (WebException e)
            {
                using (WebResponse responses = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)responses;
                    using (Stream data = responses.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        var dd = reader.ReadToEnd();
                    }
                }
                string err = e.ToString();
            }
            return 0;

        }

        public string TemplateEmail(OpchDTO oOpchDTO)
        {
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<ProveedorDTO> lstProveedorDTO = oProveedorDAO.ObtenerDatosxID(oOpchDTO.IdProveedor,BaseDatos);

            string Plantilla = @"
            <html>

            <head>
                <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
                <title></title>
                <style>
                    table {     font-family: Sans-Serif;
                        font-size: 12px;    margin: 10px;     width: 480px; text-align: left;    border-collapse: collapse; }

                    th {     font-size: 13px;     font-weight: normal;     padding: 8px;     background: #b9c9fe;
                        border-top: 4px solid #aabcfe;    border-bottom: 1px solid #fff; color: #039; }

                    td {    padding: 8px;     background: #e8edff;     border-bottom: 1px solid #fff;
                        color: #669;    border-top: 1px solid transparent; }

                    tr:hover td { background: #d0dafd; color: #339; }
                </style>
            </head>

            <body>
                <p>Estimado Almacenero</p>

                <p>Se le notifica que se acaba de crear una Factura con Nro de Doc: <b>" + oOpchDTO.NombSerie + "-" + oOpchDTO.Correlativo + @"</b> , la cual acaba de realizar ingresos en su Almacen: <b> " + oOpchDTO.NombAlmacen + @"</b> </p>

                <p><b>RUC Proveedor: </b>" + lstProveedorDTO[0].NumeroDocumento + @"</p>
                <p><b>Razón Social Proveedor: </b>" + lstProveedorDTO[0].RazonSocial + @"</p>
                <p><b>Fecha Documento: </b>" + oOpchDTO.FechaDocumento+ @"</p>
                <p><b>Fecha Contabilizacion: </b> " + oOpchDTO.FechaContabilizacion+ @"</p>
                <p><b>Fecha Creacion del Doc: </b>" + oOpchDTO.CreatedAt+ @"</p>
                <p><b>Usuario Registro Factura: </b>" + oOpchDTO.NombUsuario+@"</p>


                <table>
                    <caption>Datos de la Factura</caption>
                    <thead>
                        <tr>
                            <th>Codigo</th>
                            <th>Articulo</th>
                            <th>Cantidad</th>
                            <th>Precio Unitario</th>
                        </tr>
                    </thead>
                    <tbody>";

                    for (int i = 0; i < oOpchDTO.detalles.Count; i++)
                    {
                        Plantilla += @"
                                        <tr>
                                            <td>" + oOpchDTO.detalles[i].CodigoArticulo+@"</td>
                                            <td>"+ oOpchDTO.detalles[i].DescripcionArticulo+ @"</td>
                                            <td>"+decimal.Round(oOpchDTO.detalles[i].Cantidad,2) +@"</td>
                                            <td>"+decimal.Round(oOpchDTO.detalles[i].valor_unitario,2) +@"</td>
                                        </tr>
                                    ";
                    }

                    Plantilla +=@"</tbody>
                </table>


            </body>

            </html>";

            return Plantilla;
        }


        public int ActualizarEstadoValidacionSUNAT(int IdOPCH)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpchDAO oOpchDAO = new OpchDAO();
            OpchDTO oOpchDTO = oOpchDAO.ObtenerDatosxIdOpch(IdOPCH,BaseDatos,ref mensaje_error);

            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            List<ProveedorDTO> lstProveedorDTO = oProveedorDAO.ObtenerDatosxID(oOpchDTO.IdProveedor,BaseDatos);

            TiposDocumentosDAO oTiposDocumentosDAO = new TiposDocumentosDAO();
            List<TiposDocumentosDTO> lstTiposDocumentosDTO = oTiposDocumentosDAO.ObtenerDatosxID(oOpchDTO.IdTipoDocumentoRef,BaseDatos,ref mensaje_error);

            ConfiguracionSociedadDAO oSociedadDAO = new ConfiguracionSociedadDAO();
            List<ConfiguracionSociedadDTO> sociedad = oSociedadDAO.ObtenerConfiguracionSociedad(oOpchDTO.IdSociedad,BaseDatos,ref mensaje_error);

            int estadoCp = 0;
            string estadoRuc = "-";
            string condDomiRuc = "-";


            try
            {


                string RUC = lstProveedorDTO[0].NumeroDocumento;
                string Serie = oOpchDTO.NumSerieTipoDocumentoRef.Split('-')[0];
                string Numero = oOpchDTO.NumSerieTipoDocumentoRef.Split('-')[1];
                DateTime FechaEmision = oOpchDTO.FechaDocumento;
                string NumDocAdquiriente = sociedad[0].Ruc;
                string TotalDocumento = oOpchDTO.Total.ToString("N2");
                string TipoDoc = lstTiposDocumentosDTO[0].CodSunat;


                string SunatID = "0a0af984-ae1e-47b3-a200-edc706a14fa0";
                string SunatClave = "N61pn1uXKGoQZahHzp7B0Q==";




                ResponseDocumentoConsultaDTO reponseDocumento = new ResponseDocumentoConsultaDTO();
                reponseDocumento = null;
                int contador = 0;
                while (reponseDocumento == null && contador <= 3)
                {
                    reponseDocumento = validar_cpe_sunat(RUC, Serie, Numero, FechaEmision, NumDocAdquiriente, TotalDocumento, TipoDoc, SunatID, SunatClave);
                    contador++;
                }

                if (reponseDocumento != null)
                {

                    switch (reponseDocumento.data.estadoCp)
                    {
                        //NO EXISTE
                        case "0":
                            estadoCp = 2;
                            break;
                        //VALIDO
                        case "1":
                            estadoCp = 1;
                            break;
                        //ANULADO
                        case "2":
                            estadoCp = 3;
                            break;
                        //AUTORIZADO
                        case "3":
                            estadoCp = 4;
                            break;
                        //AUTORIZADO
                        case "4":
                            estadoCp = 5;
                            break;
                        //ERROR
                        default:
                            estadoCp = 0;
                            break;
                    }

                    switch (reponseDocumento.data.estadoRuc)
                    {
                        case "00":
                            estadoRuc = "ACTIVO";
                            break;
                        case "01":
                            estadoRuc = "BAJA PROVISIONAL";
                            break;
                        case "02":
                            estadoRuc = "BAJA PROV. POR OFICIO";
                            break;
                        case "03":
                            estadoRuc = "SUSPENSION TEMPORAL";
                            break;
                        case "10":
                            estadoRuc = "BAJA DEFINITIVA";
                            break;
                        case "11":
                            estadoRuc = "BAJA DE OFICIO";
                            break;
                        case "12":
                            estadoRuc = "INHABILITADO-VENT.UNICA";
                            break;
                        //ERROR
                        default:
                            estadoRuc = "-";
                            break;
                    }

                    switch (reponseDocumento.data.condDomiRuc)
                    {
                        case "00":
                            condDomiRuc = "HABIDO";
                            break;
                        case "09":
                            condDomiRuc = "PENDIENTE";
                            break;
                        case "11":
                            condDomiRuc = "POR VERIFICAR";
                            break;
                        case "12":
                            condDomiRuc = "NO HABIDO";
                            break;
                        case "20":
                            condDomiRuc = "NO HALLADO";
                            break;
                        //ERROR
                        default:
                            condDomiRuc = "-";
                            break;
                    }
                }
                int actualizar = oOpchDAO.GuardarValidacionSUNAT(IdOPCH, estadoCp, estadoRuc, condDomiRuc, BaseDatos);
                return estadoCp;
            }
            catch (Exception e)
            {
                int actualizar = oOpchDAO.GuardarValidacionSUNAT(IdOPCH, 0, "-", "-", BaseDatos);
                return estadoCp;
            }



        }


        public ResponseDocumentoConsultaDTO validar_cpe_sunat(string RUC, string Serie, string Numero, DateTime FechaEmision, string NumDocAdquiriente,
            string TotalDocumento, string TipoDoc, string SunatID, string SunatClave)
        {
            int respuesta;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 |
            //        (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
            string tokenSUNAT;
            ResponseDocumentoConsultaDTO _reponseDocumento;
            DocumentoConsultaDTO documento = new DocumentoConsultaDTO();
            tokenSUNAT = obtenerTokenSUNAT(SunatID, SunatClave);
            documento.numRuc = RUC;
            documento.codComp = TipoDoc;
            documento.numeroSerie = Serie;
            documento.numero = Numero.ToString().TrimStart('0');
            documento.fechaEmision = FechaEmision.ToString("dd/MM/yyyy");
            documento.monto = Convert.ToDecimal(TotalDocumento);
            _reponseDocumento = consultarCPESUNAT(NumDocAdquiriente, tokenSUNAT, documento);
           

            return _reponseDocumento;
        }

        public ResponseDocumentoConsultaDTO consultarCPESUNAT(string rucClienteFacturacionE, string tokenSUNAT, DocumentoConsultaDTO documento)
        {
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string requestData;
            string response;
            ResponseDocumentoConsultaDTO _reponseDocumento = null;

            try
            {
                cadenaUri = "https://api.sunat.gob.pe/v1/contribuyente/contribuyentes/" + rucClienteFacturacionE + "/validarcomprobante";

                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Bearer " + tokenSUNAT);

                requestData = JsonConvert.SerializeObject(documento);
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                requestWriter.Write(requestData);
                requestWriter.Close();

                webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                response = responseReader.ReadToEnd();
                _reponseDocumento = JsonConvert.DeserializeObject<ResponseDocumentoConsultaDTO>(response);
            }
            catch (Exception e)
            {
                string err = e.ToString();
            }

            return _reponseDocumento;
        }

        public string obtenerTokenSUNAT(string client_id, string client_secret)
        {
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string requestData;
            string response;
            byte[] bytesData;
            ResponseTokenSunatDTO _responseTokenSUNAT;

            try
            {
                cadenaUri = "https://api-seguridad.sunat.gob.pe/v1/clientesextranet/" + client_id + "/oauth2/token/";

                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                requestData = "grant_type=client_credentials&scope=https://api.sunat.gob.pe/v1/contribuyente/contribuyentes" +
                              "&client_id=" + client_id +
                              "&client_secret=" + client_secret;
                bytesData = Encoding.UTF8.GetBytes(requestData);
                request.ContentLength = bytesData.Length;

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(bytesData, 0, bytesData.Length);
                dataStream.Close();

                webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                response = responseReader.ReadToEnd();
                _responseTokenSUNAT = JsonConvert.DeserializeObject<ResponseTokenSunatDTO>(response);

                return _responseTokenSUNAT.access_token;
            }
            catch (Exception e)
            {
                string err = e.ToString();
            }

            return "";
        }

        public string ValidacionSunatMasiva(int IdObra, int IdTipoRegistro, int IdSemana)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpchDAO oOpchDAO = new OpchDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            List<OpchDTO> lstOpchDTO = oOpchDAO.ObtenerOPCHxEstado(IdObra, IdTipoRegistro, IdSemana, BaseDatos, ref mensaje_error);

            List<OpchDTO> PendientesValidar = new List<OpchDTO>();
            for (int i = 0; i < lstOpchDTO.Count; i++)
            {
                if (lstOpchDTO[i].IdTipoDocumentoRef==2 || lstOpchDTO[i].IdTipoDocumentoRef == 12 || lstOpchDTO[i].IdTipoDocumentoRef == 13 )
                {
                    if(lstOpchDTO[i].ValidadoSUNAT != 1)
                    {
                        PendientesValidar.Add(lstOpchDTO[i]);
                    }
                }
            }
            for (int i = 0; i < PendientesValidar.Count; i++)
            {
                int Respuesta = ActualizarEstadoValidacionSUNAT(PendientesValidar[i].IdOPCH);

                
            }


            return "OK";
        }

        public string ObtenerSerieOPCH(int IdOPCH)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OpchDAO oOpchDAO = new OpchDAO();
            string respuesta = oOpchDAO.ObtenerNroOperacionOPCH(IdOPCH, BaseDatos);
            return respuesta;
        }

        public string GenerarReporteValidacionSUNAT(int IdObra, int IdTipoRegistro, int IdSemana,string BaseDatos)
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
            requestData = JsonConvert.SerializeObject(oWebServiceDTO);


            try
            {
                string strNew = "IdObra=" + IdObra + "&IdTipoRegistro=" + IdTipoRegistro + "&IdSemana=" + IdSemana + "&BaseDatos=" + BaseDatos;
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReportCrystal";
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ReporteValidacionSUNAT";
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

        public string GenerarReporteFacturaServicioObra(string NombreReporte,DateTime FechaInicio,DateTime FechaFin, string Formato,int IdObra, int IdProveedor, string BaseDatos)
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
            string response;
            string mensaje_error;
 

            try
            {
                string strNew = "Formato="  +Formato+"&IdObra=" + IdObra + "&IdProveedor=" + IdProveedor + "&BaseDatos=" + BaseDatos + "&NombreReporte=" + NombreReporte + "&FechaInicio=" + FechaInicio + "&FechaFin=" + FechaFin;
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ReporteFacturaServicioObra";
                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";


                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                requestWriter.Write(strNew);
                requestWriter.Close();



                webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                response = responseReader.ReadToEnd();

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(response);

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

        public string GenerarReporteFacturaPendienteSAP(string NombreReporte, DateTime FechaInicio, DateTime FechaFin, string Formato, int IdObra, int IdTipoRegistro, string BaseDatos)
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
            string response;
            string mensaje_error;


            try
            {
                string strNew = "Formato=" + Formato + "&IdObra=" + IdObra + "&IdTipoRegistro=" + IdTipoRegistro + "&BaseDatos=" + BaseDatos + "&NombreReporte=" + NombreReporte + "&FechaInicio=" + FechaInicio + "&FechaFin=" + FechaFin;
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ReporteFacturasPendienteSAP";
                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";


                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                requestWriter.Write(strNew);
                requestWriter.Close();



                webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                response = responseReader.ReadToEnd();

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(response);

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


    }
}
