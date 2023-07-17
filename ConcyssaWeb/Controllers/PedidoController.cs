using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;
using System.Xml;
using Microsoft.SqlServer.Server;
using System;
using System.Xml.Linq;
using Karambolo.AspNetCore.Bundling;

namespace ConcyssaWeb.Controllers
{
    public class PedidoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public IActionResult Conformidad()
        {
            return View();
        }

        public IActionResult CorreoProveedor()
        {
            return View();
        }

        public string ObtenerPedidosEntregaLDT(int IdObra,string EstadoPedido = "ABIERTO")
        {
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<PedidoDTO> lstPedidoDTO = oPedidoDAO.ObtenerPedidosEntregaLDT(IdSociedad, ref mensaje_error, EstadoPedido, IdObra, IdUsuario);
            if (lstPedidoDTO.Count > 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstPedidoDTO.Count;
                oDataTableDTO.iTotalRecords = lstPedidoDTO.Count;
                oDataTableDTO.aaData = (lstPedidoDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            else
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstPedidoDTO.Count;
                oDataTableDTO.iTotalRecords = lstPedidoDTO.Count;
                oDataTableDTO.aaData = (lstPedidoDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            return mensaje_error;
        }


        public string UpdateInsertPedido(PedidoDTO oPedidoDTO)
        {
            string mensaje_error = "";
            int IdSociedad = Convert.ToInt32((String.IsNullOrEmpty(oPedidoDTO.IdSociedad.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oPedidoDTO.IdSociedad);
            int IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(oPedidoDTO.IdUsuario.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oPedidoDTO.IdUsuario);

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

            oPedidoDTO.IdSociedad = IdSociedad;
            oPedidoDTO.IdUsuario = IdUsuario;
            PedidoDAO oPedidoDAO = new PedidoDAO();
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int respuesta = oPedidoDAO.UpdateInsertPedido(oPedidoDTO, ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {
                for (int i = 0; i < oPedidoDTO.detalles.Count; i++)
                {
                    oPedidoDTO.detalles[i].IdPedido = respuesta;
                    int respuesta1 = oPedidoDAO.InsertUpdatePedidoDetalle(oPedidoDTO.detalles[i], ref mensaje_error);
                }


                /*INSERTAR CUADRO COMPARATIVO*/
                RespuestaDTO oRespuestaDTOReporte = new RespuestaDTO();
                try
                {
                    string respuestareporte = GenerarReporte("CuadroComparativo", "PDF", respuesta);
                    oRespuestaDTOReporte = JsonConvert.DeserializeObject<RespuestaDTO>(respuestareporte);
                    Byte[] archivoreporte = Convert.FromBase64String(oRespuestaDTOReporte.Base64ArchivoPDF);
                    string nombrearchivocuadro = "CuadroComparativoPedido" + respuesta + ".pdf";


                    if (System.IO.File.Exists("wwwroot\\Anexos\\" + nombrearchivocuadro))
                        System.IO.File.WriteAllBytes("wwwroot\\Anexos\\" + nombrearchivocuadro, archivoreporte);
                    else
                    {
                        System.IO.File.Delete("wwwroot\\Anexos\\" + nombrearchivocuadro);
                        System.IO.File.WriteAllBytes("wwwroot\\Anexos\\" + nombrearchivocuadro, archivoreporte);
                    }
                    AnexoDTO oPedAnexo = new AnexoDTO();
                    oPedAnexo.ruta = "/Anexos/" + nombrearchivocuadro;
                    oPedAnexo.IdSociedad= oPedidoDTO.IdSociedad;
                    oPedAnexo.Tabla= "Pedido";
                    oPedAnexo.IdTabla = respuesta;
                    oPedAnexo.NombreArchivo = nombrearchivocuadro;

                    oMovimientoDAO.InsertAnexoMovimiento(oPedAnexo, ref mensaje_error);

                }
                catch (Exception ex)
                {

                    var dd = "";
                }
               

                /*INSERTAR CUADRO COMPARATIVO*/
                if (oPedidoDTO.AnexoDetalle!=null)
                {
                    for (int i = 0; i < oPedidoDTO.AnexoDetalle.Count; i++)
                    {
                        oPedidoDTO.AnexoDetalle[i].ruta = "/Anexos/" + oPedidoDTO.AnexoDetalle[i].NombreArchivo;
                        oPedidoDTO.AnexoDetalle[i].IdSociedad = oPedidoDTO.IdSociedad;
                        oPedidoDTO.AnexoDetalle[i].Tabla = "Pedido";
                        oPedidoDTO.AnexoDetalle[i].IdTabla = respuesta;

                        oMovimientoDAO.InsertAnexoMovimiento(oPedidoDTO.AnexoDetalle[i], ref mensaje_error);
                    }
                }
               
                

                oPedidoDAO.UpdateTotalesPedido(respuesta, ref mensaje_error);

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

        public string ObtenerPedidoDTConfirmidad(int Conformidad = 0)
        {
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<PedidoDTO> lstPedidoDTO = oPedidoDAO.ObtenerPedidoxEstadoConformidad(IdSociedad, ref mensaje_error, Conformidad);
            if (lstPedidoDTO.Count > 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstPedidoDTO.Count;
                oDataTableDTO.iTotalRecords = lstPedidoDTO.Count;
                oDataTableDTO.aaData = (lstPedidoDTO);
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            else
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstPedidoDTO.Count;
                oDataTableDTO.iTotalRecords = lstPedidoDTO.Count;
                oDataTableDTO.aaData = (lstPedidoDTO);
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            return mensaje_error;
        }


        
        public string ObtenerPedidoDTCorreoProveedor(int EnvioCorreo = 0, int Proveedor=0)
        {
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<PedidoDTO> lstPedidoDTO = oPedidoDAO.ObtenerPedidoDTCorreoProveedor(IdSociedad, EnvioCorreo, Proveedor, ref mensaje_error);
            if (lstPedidoDTO.Count > 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstPedidoDTO.Count;
                oDataTableDTO.iTotalRecords = lstPedidoDTO.Count;
                oDataTableDTO.aaData = (lstPedidoDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            else
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstPedidoDTO.Count;
                oDataTableDTO.iTotalRecords = lstPedidoDTO.Count;
                oDataTableDTO.aaData = (lstPedidoDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            return mensaje_error;
        }



        public string ObtenerPedidoDT(int estado = 3)
        {
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<PedidoDTO> lstPedidoDTO = oPedidoDAO.ObtenerPedido(IdSociedad, ref mensaje_error, estado);
            if (lstPedidoDTO.Count > 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstPedidoDTO.Count;
                oDataTableDTO.iTotalRecords = lstPedidoDTO.Count;
                oDataTableDTO.aaData = (lstPedidoDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            else
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstPedidoDTO.Count;
                oDataTableDTO.iTotalRecords = lstPedidoDTO.Count;
                oDataTableDTO.aaData = (lstPedidoDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            return mensaje_error;
        }

        public string ObtenerPedidoDetalle(int IdPedido)
        {
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<PedidoDetalleDTO> lstPedidoDetalleDTO = oPedidoDAO.ObtenerDetallePedido(IdPedido, ref mensaje_error);
            if (lstPedidoDetalleDTO.Count > 0)
            {
                //oDataTableDTO.sEcho = 1;
                //oDataTableDTO.iTotalDisplayRecords = lstPedidoDetalleDTO.Count;
                //oDataTableDTO.iTotalRecords = lstPedidoDetalleDTO.Count;
                //oDataTableDTO.aaData = (lstPedidoDetalleDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(lstPedidoDetalleDTO);

            }
            else
            {
                return mensaje_error;

            }
        }

        public string ObtenerDatosxID(int IdPedido)
        {
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            PedidoDTO oPedidoDTO = new PedidoDTO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            oPedidoDTO = oPedidoDAO.ObtenerPedidoxId(IdPedido, ref mensaje_error);
            if (oPedidoDTO != null)
            {
                //oDataTableDTO.sEcho = 1;
                //oDataTableDTO.iTotalDisplayRecords = lstPedidoDetalleDTO.Count;
                //oDataTableDTO.iTotalRecords = lstPedidoDetalleDTO.Count;
                //oDataTableDTO.aaData = (lstPedidoDetalleDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oPedidoDTO);

            }
            else
            {
                return mensaje_error;

            }
        }


        public string ListarItemAprobadosxSociedadDT(int IdPedido)
        {
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            List<ItemAprobadosDTO> lstItemAprobadosDTO = new List<ItemAprobadosDTO>();
            DataTableDTO oDataTableDTO = new DataTableDTO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            lstItemAprobadosDTO = oPedidoDAO.ListarItemAprobadosxSociedad(IdSociedad, ref mensaje_error);
            oDataTableDTO.sEcho = 1;
            oDataTableDTO.iTotalDisplayRecords = lstItemAprobadosDTO.Count;
            oDataTableDTO.iTotalRecords = lstItemAprobadosDTO.Count;
            oDataTableDTO.aaData = (lstItemAprobadosDTO);
            //return oDataTableDTO;
            return JsonConvert.SerializeObject(oDataTableDTO);
        }


        public string ObtenerStockxIdDetalleSolicitudRQ(int IdDetalleRQ)
        {
            string mensaje_error = "";
            List<ArticuloStockDTO> lstArticuloStockDTO = new List<ArticuloStockDTO>();
            PedidoDAO oPedidoDAO = new PedidoDAO();
            lstArticuloStockDTO = oPedidoDAO.ObtenerStockxIdDetalleSolicitudRQ(IdDetalleRQ, ref mensaje_error);
            if (lstArticuloStockDTO.Count() > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloStockDTO);

            }
            else
            {
                return mensaje_error;
            }
            return JsonConvert.SerializeObject(lstArticuloStockDTO);

        }

        public string ObtenerPrecioxProductoUltimasVentas(int IdArticulo)
        {
            string mensaje_error = "";
            List<ProveedoresPrecioProductoDTO> lstProveedoresPrecioProductoDTO = new List<ProveedoresPrecioProductoDTO>();
            PedidoDAO oPedidoDAO = new PedidoDAO();
            lstProveedoresPrecioProductoDTO = oPedidoDAO.ObtenerPrecioxProductoUltimasVentas(IdArticulo, ref mensaje_error);
            if (lstProveedoresPrecioProductoDTO.Count() > 0)
            {
                return JsonConvert.SerializeObject(lstProveedoresPrecioProductoDTO);

            }
            else
            {
                return mensaje_error;
            }
            return JsonConvert.SerializeObject(lstProveedoresPrecioProductoDTO);

        }public string ObtenerProveedoresPrecioxProducto(int IdArticulo)
        {
            string mensaje_error = "";
            List<ProveedoresPrecioProductoDTO> lstProveedoresPrecioProductoDTO = new List<ProveedoresPrecioProductoDTO>();
            PedidoDAO oPedidoDAO = new PedidoDAO();
            lstProveedoresPrecioProductoDTO = oPedidoDAO.ObtenerProveedoresPrecioxProducto(IdArticulo, ref mensaje_error);
            if (lstProveedoresPrecioProductoDTO.Count() > 0)
            {
                return JsonConvert.SerializeObject(lstProveedoresPrecioProductoDTO);

            }
            else
            {
                return mensaje_error;
            }
            return JsonConvert.SerializeObject(lstProveedoresPrecioProductoDTO);

        }
        public string ObtenerProveedoresPrecioxProductoConObras(int IdObra,int IdArticulo)
        {
            string mensaje_error = "";
            List<ProveedoresPrecioProductoDTO> lstProveedoresPrecioProductoDTO = new List<ProveedoresPrecioProductoDTO>();
            PedidoDAO oPedidoDAO = new PedidoDAO();
            lstProveedoresPrecioProductoDTO = oPedidoDAO.ObtenerProveedoresPrecioxProductoConObras(IdObra,IdArticulo, ref mensaje_error);
            if (lstProveedoresPrecioProductoDTO.Count() > 0)
            {
                return JsonConvert.SerializeObject(lstProveedoresPrecioProductoDTO);

            }
            else
            {
                return mensaje_error;
            }
            return JsonConvert.SerializeObject(lstProveedoresPrecioProductoDTO);

        }

        public string ActualizarProveedorPrecio(int IdProveedor, decimal precionacional, decimal precioextranjero, int idproducto, int IdDetalleRq, string Comentario)
        {
            if (Comentario==null)
            {
                Comentario = "";
            }
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            oPedidoDAO.UpdateInsertPedidoAsignadoPedidoRQ(IdProveedor, precionacional, precioextranjero, idproducto, IdDetalleRq, Comentario, ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            return "EXITO";
        }

        public string ListarProductosAsignadosxProveedorxIdUsuarioDT()
        {
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<AsignadoPedidoRequeridoDTO> lstAsignadoPedidoRequeridoDTO = new List<AsignadoPedidoRequeridoDTO>();
            lstAsignadoPedidoRequeridoDTO = oPedidoDAO.ListarProductosAsignadosxProveedorxIdUsuario(IdUsuario, ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            DataTableDTO oDataTableDTO = new DataTableDTO();
            oDataTableDTO.sEcho = 1;
            oDataTableDTO.iTotalDisplayRecords = lstAsignadoPedidoRequeridoDTO.Count;
            oDataTableDTO.iTotalRecords = lstAsignadoPedidoRequeridoDTO.Count;
            oDataTableDTO.aaData = (lstAsignadoPedidoRequeridoDTO);
            return JsonConvert.SerializeObject(oDataTableDTO);
        }

        public string ObtenerDatosProveedorXRQAsignados(int IdProveedor,int TipoItem,int IdObra)
        {
            string mensaje_error = "";
            List<AsignadoPedidoRequeridoDTO> lstAsignadoPedidoRequeridoDTO = new List<AsignadoPedidoRequeridoDTO>();
            PedidoDAO oPedidoDAO = new PedidoDAO();
            lstAsignadoPedidoRequeridoDTO = oPedidoDAO.ListarProductosAsignadosxProveedorDetalle(IdProveedor,TipoItem, IdObra, ref mensaje_error);
            return JsonConvert.SerializeObject(lstAsignadoPedidoRequeridoDTO);
        }

        public string updatedInsertConformidadPedido(ConformidadPedidoDTO oConformidadPedidoDTO)
        {
            PedidoDAO oPedidoDAO = new PedidoDAO();
            string mensaje_error = "";
            int respuesta = oPedidoDAO.UpdateInsertPedidoConformidadPedido(oConformidadPedidoDTO, ref mensaje_error);
            return respuesta.ToString();
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

        public string EnviarCorreoPedido(IList<IdpedidoDTO> detalles)
        {
            if (detalles.Count()>0)
            {
                for (int i = 0; i < detalles.Count(); i++)
                {
                    EnviarCorreoxPedido(detalles[i].IdPedido);
                }
            }
            return "";
        }

        public int EnviarCorreoxPedido(int IdPedido)
        {
            

            try
            {

                string base64;
                string html = @"";
                string mensaje_error = "";
                PedidoDAO oPedidoDAO = new PedidoDAO();
                PedidoDTO oPedidoDTO = new PedidoDTO();
                int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

                oPedidoDTO = oPedidoDAO.ObtenerPedidoxId(IdPedido, ref mensaje_error);
                string body;
                body = "BASE PRUBAS";

                //body = "<body>" +
                //    "<h2>Se "+Estado+" una Solicitud</h2>" +
                //    "<h4>Detalles de Solicitud:</h4>" +
                //    "<span>N° Solicitud: " + Serie + "-" + Numero + "</span>" +
                //    "<br/><span>Solicitante: " + Solicitante + "</span>" +
                //    "</body>";


                //solo para pruebas
                oPedidoDTO.EmailProveedor = "fperez@smartcode.pe";

                string msge = "";
                string from = "concyssa.smc@gmail.com";
                string correo = from;
                string password = "tlbvngkvjcetzunr";
                string displayName = "SMC - ENVIO ORDEN COMPRA "+ oPedidoDTO.NombSerie + "-"+oPedidoDTO.Correlativo;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, displayName);


                mail.To.Add(oPedidoDTO.EmailProveedor);
                mail.To.Add("jhuniors.ramos@smartcode.pe");
                mail.Subject = "CONCYSSA - ENVIO ORDEN COMPRA";
                //mail.CC.Add(new MailAddress("camala145@gmail.com"));
                mail.Body = TemplateEmail();

                mail.IsBodyHtml = true; 
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Aquí debes sustituir tu servidor SMTP y el puerto
                client.Credentials = new NetworkCredential(from, password);
                client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false



                WebResponse webResponse;
                HttpWebRequest request;
                Uri uri;
                string cadenaUri;
                string response;
              
                string strNew = "NombreReporte=OrdenCompra&Formato=PDF&Id=" + IdPedido;
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReportCrystal";
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReportCrystal";
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
                base64 = xDoc.ChildNodes[1].ChildNodes[2].InnerText;
                Byte[] archivopdf = Convert.FromBase64String(base64);
                Attachment att = new Attachment(new MemoryStream(archivopdf), "OrdenCompra.pdf");
                mail.Attachments.Add(att);
                client.Send(mail);


                oPedidoDAO.UpdateEnvioCorreoPedido(IdPedido, ref mensaje_error);
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

            


            //string path = "C:\\inetpub\\wwwroot\\Binario\\Anexos\\" + pathPDF + ".pdf";
            //string path = "C:\\Users\\soporte.sap\\source\\repos\\SMC_AddonRequerimientos\\SMC_AddonRequerimientos\\Anexos\\" + pathPDF + ".pdf";
            //bool result = System.IO.File.Exists(path);
            //if (result == true)
            //{ }
            //else
            //{
            //    //GenerarPDF(IdSolicitud.ToString());
            //}

            ////Attachment archivo = new Attachment("C:\\inetpub\\wwwroot\\Binario\\Anexos\\" + pathPDF + ".pdf");
            //Attachment archivo = new Attachment("C:\\Users\\soporte.sap\\source\\repos\\SMC_AddonRequerimientos\\SMC_AddonRequerimientos\\Anexos\\" + pathPDF + ".pdf");
            //mail.Attachments.Add(archivo);

           


            return 0;
        }


        public string GenerarReporte(string NombreReporte, string Formato, int Id)
        {
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
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&Id=" + Id;
               //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReportCrystal";
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReportCrystal";
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



        public string TemplateEmail()
        {
            return @"
<html><head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
	<title></title>
	
</head>
<body>
<p>Estimado proveedor</p>

<p><u>Coordinar las entregas con Almacén y tener presente los siguientes recomendaciones:</u></p>

<p>Antes de proceder con el despacho tienen que haber llenado el formato adjunto, asimismo el vehículo que transporta el material y enviarlos a los correos de <a href='mailto:passante@concyssa.com'>passante@concyssa.com</a> y <a href='mailto:cbartolo@concyssa.com'>cbartolo@concyssa.com</a> para que puedan realizar las coordinaciones con el personal de vigilancia de la puerta principal de la universidad San Marcos y los dejen ingresar.</p>

<p>Sin este requisito ningún proveedor podrá ingresar, es por eso que resulta necesario que la comunicación sea oportuna para evitar falsos fletes.</p>

<p>Criterios de compra al momento del despacho:</p>

<h3>Equipos de protección personal:</h3>

<ul>
	<li>Los equipos de protección personal deben cumplir el estándar indicado en la orden de compra</li>
</ul>

<h3>Equipos y herramientas:</h3>

<ul>
	<li>Las herramientas deben ser normadas, contar con especificaciones técnicas que entregarán al momento del despacho.</li>
	<li>Los equipos tienen que ser normados, contar con manual en español y medidas de seguridad.</li>
	<li>El proveedor debe realizar una capacitación sobre el uso del equipo cuando se requiera.</li>
</ul>

<h3>Productos químicos:</h3>

<ul>
	<li>Al momento de la entrega, adjuntar la hoja de seguridad o MSDS y las recomendaciones de almacenamiento.</li>
	<li>En caso de no contar con lo indicado no se recibirá el material</li>
</ul>

<h3>Facturación:</h3>

<ul>
	<li>Enviar su factura electrónica a l correo: <b>comprobanteelectronico_proveedor@concyssa.com, cbartolo@concyssa.com y passante@concyssa.com</b></li>
</ul>

<h3>DOCUMENTOS A PRESENTAR PARA LA RECEPCION DE LA FACTURA</h3>

<ul>
<li>CERTIFICADO DE CALIDAD  (nombre de la obra, listado de productos)</li>
<li>CERTIFICADO Y/O CARTA DE GARANTIA (nombre de la obra, listado de productos)</li>
<li>PROTOCOLO DE APROBACION POR SEDAPAL (nombre de la obra, listado de productos)</li>
<li>FICHA TECNICA DEL PRODUCTO</li>
<li>HOJADE SEGURIDAD DE LOS PRODUCTOS</li>
<li>REGISTRO SANITARIO (DIGESA)</li>
</ul>
<h3>NOMBRE DE LA OBRA: </h3>
<p><b>“REUBICACION DE REDES DE AGUA POTABLE Y ALCANTARILLADO EN LA ESTACION JUAN PABLO II – E3    DE LA LINEA 2 Y RAMAL AV. FAUCETT – AV. GAMBETTA DE LA RED BASICA DEL METRO DE LIMA Y CALLAO”</b></p>

Favor de prever lo mencionado para no generar demoras en la logística.

<p>Lugar de entrega:</p>
<b>UNMSM (Puerta N° 08) av. Oscar R. Benavides cdra. 53, Lima.</b>



</body></html>";
        }


        public string CerrarPedido(PedidoDTO oPedidoDTO)
        {

            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int respuesta = oPedidoDAO.CerrarPedido(oPedidoDTO, ref mensaje_error);

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
        public string LiberarPedido(PedidoDTO oPedidoDTO)
        {

            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int respuesta = oPedidoDAO.LiberarPedido(oPedidoDTO, ref mensaje_error);

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
        public string AnularPedido(PedidoDTO oPedidoDTO)
        {

            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int respuesta = oPedidoDAO.AnularPedido(oPedidoDTO, ref mensaje_error);

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


    }
}
