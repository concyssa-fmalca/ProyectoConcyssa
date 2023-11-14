using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Xml;

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

        public string ListarOPCHDT(int IdBase,string EstadoOPCH = "ABIERTO")
        {
            string mensaje_error = "";
            OpchDAO oOpchDAO = new OpchDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OpchDTO> lstOpchDTO = oOpchDAO.ObtenerOPCHxEstado(IdBase,IdSociedad, ref mensaje_error, EstadoOPCH, IdUsuario);
            if (lstOpchDTO.Count >= 0 && mensaje_error.Length==0)
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


        public string ListarOPCHDTModal(string EstadoOPCH = "ABIERTO")
        {
            string mensaje_error = "";
            OpchDAO oOpchDAO = new OpchDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OpchDTO> lstOpchDTO = oOpchDAO.ObtenerOPCHxEstadoModal(IdSociedad, ref mensaje_error, EstadoOPCH, IdUsuario);
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
            OpchDAO oOpchDAO = new OpchDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OPCHDetalle> lstOPCHDetalle = oOpchDAO.ObtenerDetalleOpch(IdOPCH, ref mensaje_error);
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
            int respuesta = oMovimimientoDAO.InsertUpdateMovimientoOPCH(oOpchDTO, ref mensaje_error);
            int respuesta1 = 0;
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {
                for (int i = 0; i < oOpchDTO.detalles.Count; i++)
                {
                    oOpchDTO.detalles[i].IdOPCH = respuesta;
                    respuesta1 = oMovimimientoDAO.InsertUpdateOPCHDetalle(oOpchDTO.detalles[i], ref mensaje_error);
                    int respuesta2 = oMovimimientoDAO.InsertUpdateOPCHDetalleCuadrilla(respuesta1, oOpchDTO.detalles[i], ref mensaje_error);

                }
                oOpchDAO.UpdateTotalesOPCH(respuesta, ref mensaje_error);
                if (oOpchDTO.AnexoDetalle!=null)
                {
                    for (int i = 0; i < oOpchDTO.AnexoDetalle.Count; i++)
                    {
                        oOpchDTO.AnexoDetalle[i].ruta = "/Anexos/" + oOpchDTO.AnexoDetalle[i].NombreArchivo;
                        oOpchDTO.AnexoDetalle[i].IdSociedad = oOpchDTO.IdSociedad;
                        oOpchDTO.AnexoDetalle[i].Tabla = "Opch";
                        oOpchDTO.AnexoDetalle[i].IdTabla = respuesta;

                        oMovimimientoDAO.InsertAnexoMovimiento(oOpchDTO.AnexoDetalle[i], ref mensaje_error);
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
                    return mensaje_error;
                }
            }
        }

        public string ObtenerDatosxIdOpch(int IdOpch)
        {
            string mensaje_error = "";
            OpchDAO oOpchDAO = new OpchDAO();
            OpchDTO oOpchDTO = oOpchDAO.ObtenerDatosxIdOpch(IdOpch, ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {
                List<OPCHDetalle> lstOPCHDetalle = new List<OPCHDetalle>();
                lstOPCHDetalle = oOpchDAO.ObtenerDetalleOpch(IdOpch, ref mensaje_error);
                oOpchDTO.detalles = new OPCHDetalle[lstOPCHDetalle.Count()];
                for (int i = 0; i < lstOPCHDetalle.Count; i++)
                {
                    oOpchDTO.detalles[i] = lstOPCHDetalle[i];
                }

                List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
                lstAnexoDTO = oOpchDAO.ObtenerAnexoOpch(IdOpch, ref mensaje_error);
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
            OpchDAO oOpchDAO = new OpchDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int respuesta = oOpchDAO.UpdateOPCH(IdUsuario, oOpchDTO, ref mensaje_error);

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
            OpchDAO oOpchDAO = new OpchDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int respuesta = oOpchDAO.UpdateCuadrillas(oOPCHDetalle, ref mensaje_error);

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
            OpchDAO oOpchDAO = new OpchDAO();
         
            List<OpchDTO> lstOpchDTO = oOpchDAO.ObtenerOrigenesFactura(IdOPCH, ref mensaje_error);
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
            OpchDAO oOpchDAO = new OpchDAO();
            List<string> SinStock = new List<string>();
            List<OPCHDetalle> lstoOpchDTO = oOpchDAO.ObtenerStockParaExtornoOPCH(IdOPCH, ref mensaje_error);
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
            OpchDAO oOpchDAO = new OpchDAO();
            int respuesta = oOpchDAO.ExtornoConfirmado(IdOPCH, EsServicio, TablaOrigen, ref mensaje_error);

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
            OpchDAO oOpchDAO = new OpchDAO();

            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OpchDTO> lstOpchDTO = oOpchDAO.ObtenerOPCHxIdObra(IdObra, FechaInicio, FechaFin, ref mensaje_error);
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
            try
            {

                string base64;
                string html = @"";
                string mensaje_error = "";

                int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

                string body;
                body = "BASE PRUBAS";


                OpchDAO oOpchDAO = new OpchDAO();
                OpchDTO oOpchDTO = oOpchDAO.ObtenerDatosxIdOpch(IdOpch, ref mensaje_error);
                if (mensaje_error.ToString().Length == 0)
                {
                    List<OPCHDetalle> lstOPCHDetalle = new List<OPCHDetalle>();
                    lstOPCHDetalle = oOpchDAO.ObtenerDetalleOpch(IdOpch, ref mensaje_error);
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


           
                mail.To.Add("cristhian.chacaliaza@smartcode.pe");
                mail.To.Add("fperez@smartcode.pe");
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
            List<ProveedorDTO> lstProveedorDTO = oProveedorDAO.ObtenerDatosxID(oOpchDTO.IdProveedor);

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

    }
}
