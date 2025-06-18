using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Transactions;
using System.Xml;
using System.Web;

namespace ConcyssaWeb.Controllers
{
    public class SolicitudRQController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public ActionResult Upload()
        {
             int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            if (IdSociedad == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public string ObtenerSolicitudesRQ(int IdBase,string FechaInicio, string FechaFinal, int Estado, int IdSolicitante)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdPerfil = Convert.ToInt32(HttpContext.Session.GetInt32("IdPerfil"));
            int Usuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }



            if (IdSolicitante != 0)
            {
                SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
                int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
                List<SolicitudRQDTO> lstSolicitudRQDTO = oSolicitudRQDAO.ObtenerSolicitudesRQ(Usuario,IdPerfil,IdBase,IdSolicitante, IdSociedad.ToString(), FechaInicio, FechaFinal, Estado,BaseDatos);
                if (lstSolicitudRQDTO.Count > 0)
                {
                    return JsonConvert.SerializeObject(lstSolicitudRQDTO);
                }
                else
                {
                    return "error";
                }
            }
            else
            {
                SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
                int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
                int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
                List<SolicitudRQDTO> lstSolicitudRQDTO = oSolicitudRQDAO.ObtenerSolicitudesRQ(Usuario,IdPerfil,IdBase,int.Parse(IdUsuario.ToString()), IdSociedad.ToString(), FechaInicio, FechaFinal, Estado,BaseDatos);
                if (lstSolicitudRQDTO.Count > 0)
                {
                    return JsonConvert.SerializeObject(lstSolicitudRQDTO);
                }
                else
                {
                    return "error";
                }
            }

        }


        public string ObtenerDatosxID(int IdSolicitudRQ)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }


            SolicitudRQDAO oSerieDAO = new SolicitudRQDAO();
            List<SolicitudRQDTO> lstSolicitudRQDTO = oSerieDAO.ObtenerDatosxIDV2(IdUsuario,IdSolicitudRQ,BaseDatos);

            if (lstSolicitudRQDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSolicitudRQDTO);
            }
            else
            {
                return "error";
            }

        }

        public string ObtenerAnexoxID(int IdSolicitudRQ)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }


            SolicitudRQDAO oSerieDAO = new SolicitudRQDAO();
            List<AnexoDTO> lstSolicitudRQDTO = oSerieDAO.ObtenerAnexosxID( IdSolicitudRQ, BaseDatos);

            if (lstSolicitudRQDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSolicitudRQDTO);
            }
            else
            {
                return "error";
            }

        }


        public string ObtenerDatosRqDetallexID(int IdDetalleRq)
        {
            //return "ddd";
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }

            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            Decimal precio = oSolicitudRQDAO.ObtenerSolicitudesRQDetallePrecio(IdDetalleRq,BaseDatos);
            return precio.ToString(); 
            //if (oSolicitudRQDetalleDTO !=null)
            //{
            //    return JsonConvert.SerializeObject(oSolicitudRQDetalleDTO);
            //}
            //else
            //{
            //    return "error";
            //}

        }


        public int EliminarDetalleSolicitud(int IdSolicitudRQDetalle)
        {
            int valida = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            int resultado = oSolicitudRQDAO.Delete(IdSolicitudRQDetalle,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
        
        public int CerrarSolicitudEstado(int IdSolicitud)
        {
            int valida = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }


            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            int resultado = oSolicitudRQDAO.CerrarSolicitudEstado(IdSolicitud,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }



        public int EliminarAnexoSolicitud(int IdSolicitudRQAnexos)
        {
            int valida = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }

            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            int resultado = oSolicitudRQDAO.DeleteAnexo(IdSolicitudRQAnexos,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


        public int UpdateInsertSolicitud(string JsonDatosEnviar)
        {

            JsonDatosEnviar = JsonDatosEnviar.Remove(JsonDatosEnviar.Length - 1, 1);
            JsonDatosEnviar = JsonDatosEnviar.Remove(0, 1);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            SolicitudRQDTO solicitudRQDTO = JsonConvert.DeserializeObject<SolicitudRQDTO>(JsonDatosEnviar, settings);


            int valida = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;

            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
             int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));


            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();

            int IdDocumento = 1;

            if (solicitudRQDTO.EsSubContrato)
            {
                IdDocumento = 5;
            }

            var ModeloAuorizacion = oModeloAutorizacionDAO.VerificarExisteModeloSolicitud(int.Parse(IdSociedad.ToString()), IdDocumento, BaseDatos); //valida si existe alguina modelo aplicado a documento solicitud
            if (ModeloAuorizacion.Count > 0)
            {
                int CantidadAutores = 0;
                string Modelo = "";
                for (int i = 0; i < ModeloAuorizacion.Count; i++)
                {
                    var ResultadoModelo = oModeloAutorizacionDAO.ObtenerDatosxID(ModeloAuorizacion[i].IdModeloAutorizacion, BaseDatos);
                    for (int a = 0; a < ResultadoModelo[0].DetallesAutor.Count; a++)
                    {
                        if (ResultadoModelo[0].DetallesAutor[a].IdAutor == solicitudRQDTO.IdSolicitante)
                        {
                            CantidadAutores++;
                        }

                    }
                }


                if (CantidadAutores == 0)
                {
                    return -99;
                }


                if (CantidadAutores > 1)
                {
                    return -98;
                }
            
            }

             var resultado = oSolicitudRQDAO.UpdateInsertSolicitud(solicitudRQDTO, IdSociedad.ToString(),BaseDatos);

            if (resultado[0] == 5) //es un insert
            {
                var IdInsert = resultado[1];

                SolicitudRQDAO oSerieDAO = new SolicitudRQDAO();
                List<SolicitudRQDTO> lstSolicitudRQDTO = oSerieDAO.ObtenerDatosxID(IdInsert,BaseDatos);
                solicitudRQDTO = lstSolicitudRQDTO[0];
               
                SolicitudRQModeloDAO oSolicitudRQModeloDAO = new SolicitudRQModeloDAO();
                EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();
               

                //listado de modelos obtenidos
                if (ModeloAuorizacion.Count > 0)
                {

                    for (int i = 0; i < ModeloAuorizacion.Count; i++)
                    {
                        //obtengo datos de los modelos obtenidos
                        var ResultadoModelo = oModeloAutorizacionDAO.ObtenerDatosxID(ModeloAuorizacion[i].IdModeloAutorizacion,BaseDatos);

                        for (int a = 0; a < ResultadoModelo[0].DetallesAutor.Count; a++)
                        {
                            //valida a que usuarios se le va a aplicar este modelo de autorizacion
                            //usuario al que se le aplica el modelo
                            if (ResultadoModelo[0].DetallesAutor[a].IdAutor == solicitudRQDTO.IdSolicitante)
                            {
                                for (int c = 0; c < ResultadoModelo[0].DetallesCondicion.Count; c++)
                                {
                                    var Resultado = oModeloAutorizacionDAO.ObtenerResultadoCondicion(ResultadoModelo[0].DetallesCondicion[c].Condicion, IdInsert,1,BaseDatos);

                                    if (Resultado[0].EntraAlProcesoAutorizar == 1) //si es una solicitud para autorizar 
                                    {

                                        for (int e = 0; e < ResultadoModelo[0].DetallesEtapa.Count; e++)
                                        {
                                            
                                                var result = oSolicitudRQModeloDAO.UpdateInsertSolicitudRQModelo(new SolicitudRQModeloDTO
                                                {
                                                    IdSolicitudRQModelo = 0,
                                                    IdSolicitud = IdInsert,
                                                    IdModelo = ResultadoModelo[0].IdModeloAutorizacion,
                                                    IdEtapa = ResultadoModelo[0].DetallesEtapa[e].IdEtapa,
                                                    Aprobaciones = ResultadoModelo[0].DetallesEtapa[e].AutorizacionesRequeridas,
                                                    Rechazos = ResultadoModelo[0].DetallesEtapa[e].RechazosRequeridos
                                                }, IdSociedad.ToString(),BaseDatos);

                                                var ResultadoEtapa = oEtapaAutorizacionDAO.ObtenerDatosxID(ResultadoModelo[0].DetallesEtapa[e].IdEtapa,BaseDatos);
                                                UsuarioDAO oUsuarioDAO = new UsuarioDAO();
                                                //enviar correo
                                                string mensaje_error = "";
                                            if (e == 0)
                                            {
                                                for (int k = 0; k < ResultadoEtapa[0].Detalles.Count; k++)
                                                {
                                                    var UsersDeEtapa = oUsuarioDAO.ObtenerDatosxID(ResultadoEtapa[0].Detalles[k].IdUsuario,BaseDatos,ref mensaje_error);
                                                    var Solicitante = oUsuarioDAO.ObtenerDatosxID(solicitudRQDTO.IdSolicitante,BaseDatos,ref mensaje_error);
                                                    //EnviarCorreo(UsersDeEtapa[0].Correo, Solicitante[0].Nombre, solicitudRQDTO.Serie, solicitudRQDTO.Numero, Resultado[0].Mensaje, solicitudRQDTO);
                                                   

                                                }
                                                //enviar correo
                                            }


                                        }


                                    }
                                    else // no necesita pasar por autorizacion
                                    {
                                        //SolicitudRQModeloAprobacionesDAO oSolicitudRQModeloAprobacionesDAO = new SolicitudRQModeloAprobacionesDAO();
                                        //oSolicitudRQModeloAprobacionesDAO.AprobarSolicitudRQxIdSolicitud(IdInsert);
                                    }

                                }
                            }
                            else //usuario que no se le aplica el modelo de autorizacion
                            {
                                //SolicitudRQModeloAprobacionesDAO oSolicitudRQModeloAprobacionesDAO = new SolicitudRQModeloAprobacionesDAO();
                                //oSolicitudRQModeloAprobacionesDAO.AprobarSolicitudRQxIdSolicitud(IdInsert);
                            }
                        }

                    }
                }

            }


            if (resultado[0] != 0)
            {
                //if (solicitudRQDTO.AnexoDetalle != null)
                //{
                //    MovimientoDAO oMovimientoDAO = new MovimientoDAO();
                //    string mensaje_error = "";
                //    for (int i = 0; i < solicitudRQDTO.AnexoDetalle.Count; i++)
                //    {
                //        solicitudRQDTO.AnexoDetalle[i].ruta = "/Anexos/" + solicitudRQDTO.AnexoDetalle[i].NombreArchivo;
                //        solicitudRQDTO.AnexoDetalle[i].IdSociedad = IdSociedad;
                //        solicitudRQDTO.AnexoDetalle[i].Tabla = "SolicitudRQ";
                //        solicitudRQDTO.AnexoDetalle[i].IdTabla = resultado[1];

                //        oMovimientoDAO.InsertAnexoMovimiento(solicitudRQDTO.AnexoDetalle[i],BaseDatos,ref mensaje_error);
                //    }
                //}


                resultado[0] = 1;
            }
            CorregirSolicitudRQSinModelo();
            return resultado[0];

        }




        public void EnviarCorreo(string Correo, string Solicitante, string Serie, int Numero, string Condicion, SolicitudRQDTO oSolicitudRQDTO=null)
        //public void EnviarCorreo()
        {
            string body;
            try
            {



                //body = TemplateEmail();
                body = TemplateEmail(Serie, Numero, Solicitante, Condicion, oSolicitudRQDTO);
                //body = "<body>" +
                //    "<h2>Se creo una nueva Solicitud</h2>" +
                //    "<h4>Detalles de Solicitud:</h4>" +
                //    "<span>N° Solicitud: " + Serie + "-" + Numero + "</span>" +
                //    "<br/><span>Solicitante: "+ Solicitante + "</span>" +
                //    "<br/><span>Condicion: "+Condicion+"</span>" +
                //    "<br/><span>Url: http://localhost:58025 </span>" +
                //    "</body>";

                string msge = "";
                string from = "concyssa.smc@gmail.com";
                string correo = from;
                string password = "tlbvngkvjcetzunr";
                string displayName = "SGC-WEB";
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, displayName);
                
                mail.To.Add(Correo);

                MailAddress copy = new MailAddress("jhuniors.ramos@smartcode.pe");
                mail.CC.Add(copy);
                if (oSolicitudRQDTO != null)
                {
                    mail.Subject = "[Solicitud de aprobacion] " + oSolicitudRQDTO.Serie + "-" + oSolicitudRQDTO.Numero + " / " + oSolicitudRQDTO.Detalle[0].NombObra+" - "+oSolicitudRQDTO.Solicitante;
                }
                else
                {
                    mail.Subject = "Autorizacion "+ Solicitante+ " "+ Serie+"-"+Numero;
                }
                
                mail.Body = body + "</body></html>";

                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Aquí debes sustituir tu servidor SMTP y el puerto
                client.Credentials = new NetworkCredential(from, password);
                client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false
                client.Send(mail);
            }
            catch (Exception)
            {

                var dd = "";
            }


        }



        public string GenerarReporte(string NombreReporte, string Formato, int Id)
        {

            IConfiguration _IConfiguration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();


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
                cadenaUri = _IConfiguration["Reporte:UrlBase"] + "/ReportCrystal.asmx/ObtenerReportCrystal";
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


        //[HttpPost]
        //public string GuardarFile(HttpPostedFileBase file)
        //{
        //    string valida = "";
        //    valida = validarEmpresaActual();
        //    if (valida != "")
        //    {
        //        return valida;
        //    }


        //    List<string> Archivos = new List<string>();
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        try
        //        {
        //            string dir = "~/Anexos/" + file.FileName;
        //            if (Directory.Exists(dir))
        //            {
        //                ViewBag.Message = "Archivo ya existe";
        //            }
        //            else
        //            {
        //                string path = Path.Combine(Server.MapPath("~/Anexos"), Path.GetFileName(file.FileName));
        //                file.SaveAs(path);
        //                Archivos.Add(file.FileName);
        //                ViewBag.Message = "Anexo guardado correctamente";
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.Message = "Error:" + ex.Message.ToString();
        //            throw;
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Debe especificar el archivo";
        //    }
        //    var items = GetFiles(Archivos);
        //    return items;
        //}

        //public FileResult Download(string ImageName)
        //{
        //    var FileVirtualPath = "~/Anexos/" + ImageName;
        //    return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        //}

        //public FileResult DownloadImagen(string ImageName)
        //{
        //    ConsultasHana consultas = new ConsultasHana();
        //    string ruta = consultas.ObtenerPathImagenes();
        //    var FileVirtualPath = ruta + ImageName;
        //    return File(FileVirtualPath, Path.GetFileName(FileVirtualPath));
        //}

        //public string GetFiles(List<string> files)
        //{
        //    string valida = "";
        //    valida = validarEmpresaActual();
        //    if (valida != "")
        //    {
        //        return valida;
        //    }


        //    var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Anexos"));
        //    List<string> items = new List<string>();

        //    for (int i = 0; i < files.Count; i++)
        //    {
        //        System.IO.FileInfo[] fileNames = dir.GetFiles(files[i] + ".*");
        //        foreach (var file in fileNames)
        //        {
        //            items.Add(file.Name);
        //        }
        //    }


        //    return JsonConvert.SerializeObject(items);
        //}

        public string TemplateEmail(string Serie, int Numero, string Solicitante, string Condicion, SolicitudRQDTO oSolicitudRQDTO = null)
        {
            string bodyhtml;
            bodyhtml = @"
            <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN'
            'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
            <html xmlns='http://www.w3.org/1999/xhtml'
            style='font-family:  Helvetica, Arial, sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
            <head>
            <meta name='viewport' content='width=device-width'/>
            <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'/>
            <title>Adminto - Responsive Bootstrap 5 Admin Dashboard</title>

            </head>

            <body itemscope itemtype='http://schema.org/EmailMessage'
            style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; -webkit-font-smoothing: antialiased; -webkit-text-size-adjust: none; width: 100% !important; height: 100%; line-height: 1.6em; background-color: #f6f6f6; margin: 0;'
            bgcolor='#f6f6f6'>

            <table class='body-wrap'
            style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; background-color: #f6f6f6; margin: 0;'
            bgcolor='#f6f6f6'>
            <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
            <td style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;'
                valign='top'></td>
            <td class='container' width='600'
                style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;'
                valign='top'>
                <div class='content'
                     style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 20px;'>
                    <table class='main' width='100%' cellpadding='0' cellspacing='0'
                           style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-radius: 3px;  margin: 0; border: none;'
                           >
                        <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                            <td class='content-wrap aligncenter'
                                style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;padding: 20px;border: 3px solid #4fc6e1;border-radius: 7px; background-color: #fff;'
                                align='center' valign='top'>
                                <table width='100%' cellpadding='0' cellspacing='0'
                                       style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                    <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                        <td class='content-block'
                                            style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;'
                                            valign='top'>
                                            <h2 class='aligncenter'
                                                style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 24px; color: #000; line-height: 1.2em; font-weight: 400; text-align: center; margin: 40px 0 0;'
                                                align='center'>
                                                <a href='#' style='display: block;margin-bottom: 10px;'> <img src='https://smartcode.pe/img/elements/LOGO%20SMARTCODE.png' height='50' alt='logo'/></a> <br/>
                                                SE CREO UNA NUEVA SOLICITUD</h2>
                                        </td>
                                    </tr>
                                    <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                        <td class='content-block aligncenter'
                                            style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; text-align: center; margin: 0; padding: 0 0 20px;'
                                            align='center' valign='top'>
                                            <table class='invoice'
                                                   style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; text-align: left; width: 80%; margin: 40px auto;'>
                                                <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                                    <td style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 5px 0;'
                                                        valign='top'><b>DETALLES:</b>
                                                    </td>
                                                </tr>
                                                <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                                    <td style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 5px 0;'
                                                        valign='top'><b>Obra: " + oSolicitudRQDTO.Detalle[0].NombObra + @"</b>
                                                    </td>
                                                </tr>

                                                <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                                    <td style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 5px 0;'
                                                        valign='top'><b>N° Solicitud: " + Serie + "-" + Numero + @"</b>
                                                    </td>
                                                </tr>
                                                <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                                    <td style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 5px 0;'
                                                        valign='top'><b>Solicitante: " + Solicitante + @"</b>
                                                    </td>
                                                </tr>
                                                <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                                    <td style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 5px 0;'
                                                        valign='top'><b>Condicion: " + Condicion + @"</b>
                                                    </td>
                                                </tr>   
                                                <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                                    <td style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 5px 0;'
                                                        valign='top'>
                                                            <table class='invoice-items' cellpadding='0' cellspacing='0'
                                                               style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; margin: 0;'>";






            bodyhtml += @"
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                        <td class='content-block aligncenter'
                                            style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; text-align: center; margin: 0; padding: 0 0 20px;'
                                            align='center' valign='top'>
                                            <a href='http://192.168.0.209/'
                                               style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; color: #02c0ce; text-decoration: underline; margin: 0;'>IR a Nuestra Pagina</a>
                                        </td>
                                    </tr>
                        
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div class='footer'
                         style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; clear: both; color: #999; margin: 0; padding: 20px;'>
                        <table width='100%'
                               style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                            <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                    
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
            <td style = 'font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;'
                valign= 'top' ></ td >
            </tr>
            </table>";

            return bodyhtml;
        }


        public string ObtenerTipoCambio(string Moneda)
        {
            string valida = "";
            string Resultado = "";

            //valida = validarEmpresaActual();
            //if (valida != "")
            //{
            //    return valida;
            //}


            //int ConSAP = 1;
            //string Resultado = "";

            //if (ConSAP == 1)
            //{
            //    ConsultasHana consultasHana = new ConsultasHana();
            //    List<TipoCambioDTO> lstTipoCambioDTO = consultasHana.ListarTipoCambioxDiaHana(Moneda);
            //    if (lstTipoCambioDTO.Count > 0)
            //    {
            //        Resultado = JsonConvert.SerializeObject(lstTipoCambioDTO);
            //    }
            //    else
            //    {
            //        Resultado = "error";
            //    }
            //}
            //else
            //{
            //    Resultado = "0";
            //}
            Resultado = "0";
            return Resultado;

        }


        public int CerrarSolicitud(int IdSolictudRQ)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int valida = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }



            SolicitudRQDAO solicitudRQDAO = new SolicitudRQDAO();
            int resultado = solicitudRQDAO.CerrarSolicitud(IdSolictudRQ, IdSociedad.ToString(),BaseDatos);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;
        }



        public string validarEmpresaActual()
        {
            string rpta = "";
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            if (IdSociedad == null)
            {
                return "SinBD";
            }
            return rpta;
        }

        public int validarEmpresaActualUpdateInsert()
        {
            int rpta = 0;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            if (IdSociedad == null)
            {
                return -999;
            }
            return rpta;
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

        public string DatosSolicitudModeloAprobaciones(int IdSolicitudRQ)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            SolicitudRQDTO oSolicitudRQDTO = new SolicitudRQDTO();
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            oSolicitudRQDTO=oSolicitudRQDAO.DatosSolicitudRq(IdSolicitudRQ,BaseDatos);
            if (oSolicitudRQDTO!=null)
            {
                return JsonConvert.SerializeObject(oSolicitudRQDTO);
            }
            else
            {
                return "Error";
            }
            
        }

        public string SaveDetalleSolicitud (int IdSolicitudDetalle, decimal Cantidad)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string respuesta = "";
            int respuetadao;
            SolicitudRQDTO oSolicitudRQDTO = new SolicitudRQDTO();
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            respuetadao = oSolicitudRQDAO.SaveDetalleSolicitud(IdSolicitudDetalle, Cantidad,BaseDatos);
            if (respuetadao ==1)
            {
                respuesta = "1";
            }
            else
            {
                respuesta = "0";
            }
            return respuesta;
        }

        public string  EliminarDetalleSolicitudNew(int IdSolicitudRQDetalle)
        {
            int valida = 0;
            string respuesta="";
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida.ToString();
            }
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            int resultado = oSolicitudRQDAO.DeleteDetalleSolicitud(IdSolicitudRQDetalle,BaseDatos);
            if (resultado == 0)
            {
                respuesta = "1";
            }
            else
            {
                respuesta = "0";
            }

            return respuesta;
        }



        public int ValidaSolicitudDetalleAprobado(int IdSolicitudRQDetalle)
        {

            int valida = 0;
            int respuesta = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            int resultado = oSolicitudRQDAO.ValidaSolicitudDetalleAprobado(IdSolicitudRQDetalle,BaseDatos);
            if (resultado == 0)
            {
                respuesta = 0;
            }
            else
            {
                respuesta = 1;
            }

            return respuesta;

        }


        public string ObtenerAnexosSolicitudRQ(int IdSolicitudRQ)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<AnexoDTO> lstAnexoDTO = oSolicitudRQDAO.ObtenerAnexosSolicitudRQ(IdSolicitudRQ,BaseDatos);
            if (lstAnexoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstAnexoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public void CorregirSolicitudRQSinModelo()
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            List<SolicitudRQDTO> lstSolicitudRQDTO = oSolicitudRQDAO.ObtenerRQSinModelo(BaseDatos);
            for (int i = 0; i < lstSolicitudRQDTO.Count; i++)
            {
                oSolicitudRQDAO.CorregirSolicitudRQSinModelo(lstSolicitudRQDTO[i].IdSolicitudRQ, lstSolicitudRQDTO[i].IdSolicitante, BaseDatos);
            }
        }

        public string ValidarNroCotizacion(int IdProveedor,string NroCotizacion)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            string Documento = oSolicitudRQDAO.ValidarAnexoNroCotizacion(IdProveedor, NroCotizacion, BaseDatos);
            object json = null;

            if (Documento == "")
            {
                json = new { existe = false, documento = "" };
            }
            else
            {
                json = new { existe = true, documento = Documento };
            }

            return JsonConvert.SerializeObject(json);


        }

    }
}
