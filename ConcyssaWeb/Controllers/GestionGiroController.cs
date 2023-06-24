using DAO;
using DTO;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Xml;

namespace ConcyssaWeb.Controllers
{
    public class GestionGiroController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public string ObtenerGiro(int estado = 3, int IdObra = 0, int IdTipoRegistro=0, int IdSemana = 0,int IdEstadoGiro = 0 )
        {

            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }
            string mensaje_error = "";
            GiroDAO oGiroDAO = new GiroDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<GiroDTO> lstSemanaDTO = oGiroDAO.ObtenerGiro(ref mensaje_error, IdSociedad,IdObra, IdTipoRegistro, IdSemana, IdEstadoGiro,  estado, IdUsuario);
            DataTableDTO oDataTableDTO = new DataTableDTO();
            if (lstSemanaDTO.Count > 0 || mensaje_error == "")
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstSemanaDTO.Count;
                oDataTableDTO.iTotalRecords = lstSemanaDTO.Count;
                oDataTableDTO.aaData = (lstSemanaDTO);
                return JsonConvert.SerializeObject(oDataTableDTO);
            }
            else
            {
                return "error";
            }
        }



        public string ObtenerGiroxID(int IdGiro)
        {
            string mensaje_error = "";
            GiroDAO oGiroDAO = new GiroDAO();
            List<GiroDTO> lstSemanaDTO = oGiroDAO.ObtenerGiroxId(IdGiro, ref mensaje_error);

            if (lstSemanaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSemanaDTO[0]);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ObtenerGiroDetalle(int IdGiro)
        {

            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }
            string mensaje_error = "";
            GiroDAO oGiroDAO = new GiroDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<GiroDetalleDTO> lstSemanaDTO = oGiroDAO.ObtenerGiroDetalle(IdGiro, ref mensaje_error);
            DataTableDTO oDataTableDTO = new DataTableDTO();
            if (lstSemanaDTO.Count > 0 || mensaje_error == "")
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstSemanaDTO.Count;
                oDataTableDTO.iTotalRecords = lstSemanaDTO.Count;
                oDataTableDTO.aaData = (lstSemanaDTO);
                return JsonConvert.SerializeObject(oDataTableDTO);
            }
            else
            {
                return "error";
            }
        }


        public string ObtenerGiroDetallesxID(int IdGiroDetalle)
        {
            string mensaje_error = "";
            GiroDAO oGiroDAO = new GiroDAO();
            List<GiroDetalleDTO> lstSemanaDTO = oGiroDAO.ObtenerGiroDetallexId(IdGiroDetalle, ref mensaje_error);

            if (lstSemanaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSemanaDTO[0]);
            }
            else
            {
                return mensaje_error;
            }
        }
        //ObtenerGiroDetallesxID


        public string UpdateInsertGiroDetalle(GiroDetalleDTO oSemanaDTO)
        {

            string mensaje_error = "";
            GiroDAO oSemanaDAO = new GiroDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            //oSemanaDTO.IdSociedad = IdSociedad;
            int respuesta = oSemanaDAO.UpdateInsertGiroDetalle(oSemanaDTO, ref mensaje_error);

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
                    return "error";
                }
            }
        }
        public string UpdateInsertGiro(GiroDTO oGiroDTO)
        {

            string mensaje_error = "";
            GiroDAO oSemanaDAO = new GiroDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            //oSemanaDTO.IdSociedad = IdSociedad;
            var resultado = oSemanaDAO.UpdateInsertGiro(oGiroDTO, IdUsuario, IdSociedad, ref mensaje_error);

            if (resultado[0] == "5") //es un insert
            {
                var IdGiroSplit = resultado[1].Split("-");
                var IdInsert = int.Parse(IdGiroSplit[0]);
                GiroDAO giro = new GiroDAO();
                List<GiroDTO> lstGiro = giro.ObtenerGiroxId(IdInsert, ref mensaje_error);
                oGiroDTO = lstGiro[0];
                ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
                GiroModeloDAO oGiroModeloDAO = new GiroModeloDAO();
                EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();

                var ModeloAuorizacion = oModeloAutorizacionDAO.VerificarExisteModeloSolicitud(int.Parse(IdSociedad.ToString()),2);

                //listado de modelos obtenidos
                if (ModeloAuorizacion.Count > 0)
                {

                    for (int i = 0; i < ModeloAuorizacion.Count; i++)
                    {
                        //obtengo datos de los modelos obtenidos
                        var ResultadoModelo = oModeloAutorizacionDAO.ObtenerDatosxID(ModeloAuorizacion[i].IdModeloAutorizacion);
                        for (int a = 0; a < ResultadoModelo[0].DetallesAutor.Count; a++)
                        {
                            //valida a que usuarios se le va a aplicar este modelo de autorizacion
                            //usuario al que se le aplica el modelo
                            if (ResultadoModelo[0].DetallesAutor[a].IdAutor == oGiroDTO.IdCreador)
                            {
                                for (int c = 0; c < ResultadoModelo[0].DetallesCondicion.Count; c++)
                                {
                                    var Resultado = oModeloAutorizacionDAO.ObtenerResultadoCondicion(ResultadoModelo[0].DetallesCondicion[c].Condicion, IdInsert,2);

                                    if (Resultado[0].EntraAlProcesoAutorizar == 1) //si es una solicitud para autorizar 
                                    {

                                        for (int e = 0; e < ResultadoModelo[0].DetallesEtapa.Count; e++)
                                        {

                                            var result = oGiroModeloDAO.UpdateInsertGiroModelo(new GiroModeloDTO
                                            {
                                                IdGiroModelo = 0,
                                                IdGiro = IdInsert,
                                                IdModelo = ResultadoModelo[0].IdModeloAutorizacion,
                                                IdEtapa = ResultadoModelo[0].DetallesEtapa[e].IdEtapa,
                                                Aprobaciones = ResultadoModelo[0].DetallesEtapa[e].AutorizacionesRequeridas,
                                                Rechazos = ResultadoModelo[0].DetallesEtapa[e].RechazosRequeridos
                                            }, IdSociedad.ToString());

                                            var ResultadoEtapa = oEtapaAutorizacionDAO.ObtenerDatosxID(ResultadoModelo[0].DetallesEtapa[e].IdEtapa);
                                            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
                                            //enviar correo
                                            //string mensaje_error = "";
                                            if (e == 0)
                                            {
                                                for (int k = 0; k < ResultadoEtapa[0].Detalles.Count; k++)
                                                {
                                                    var UsersDeEtapa = oUsuarioDAO.ObtenerDatosxID(ResultadoEtapa[0].Detalles[k].IdUsuario, ref mensaje_error);
                                                    var Solicitante = oUsuarioDAO.ObtenerDatosxID(oGiroDTO.IdSolicitante, ref mensaje_error);
                                                    //EnviarCorreo(UsersDeEtapa[0].Correo, Solicitante[0].Nombre, solicitudRQDTO.Serie, solicitudRQDTO.Numero, Resultado[0].Mensaje, solicitudRQDTO);
                                                    //EnviarCorreo("jhuniors.ramos@smartcode.pe", Solicitante[0].Nombre, solicitudRQDTO.Serie, solicitudRQDTO.Numero, Resultado[0].Mensaje);

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
            //if (mensaje_error.Length > 0)
            //{
            //    return mensaje_error;
            //}
            //else
            //{
            //    return respuesta;
            //}
            return "1";
         }





        public int EliminarGiro(int IdGiro)
        {
            string mensaje_error = "";
            GiroDAO oSemanaDAO = new GiroDAO();
            int resultado = oSemanaDAO.DeleteGiro(IdGiro, ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public int EliminarGiroDetalle(int IdGiroDetalle)
        {
            string mensaje_error = "";
            GiroDAO oSemanaDAO = new GiroDAO();
            int resultado = oSemanaDAO.DeleteGiroDetalle(IdGiroDetalle, ref mensaje_error);
            if (resultado == 0)
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

        public string  UpLoadFile(IFormFile uploadfile)
        {
           
            FileUploadDTO fileUploadDTO = new FileUploadDTO(); 
            if (uploadfile != null && uploadfile.Length > 0)
            {
                try
                {
                    string dir = "wwwroot/Requerimiento/" + uploadfile.FileName;
                    if (Directory.Exists(dir))
                    {
                        ViewBag.Message = "Archivo ya existe";
                        fileUploadDTO.success = true;                   
                        fileUploadDTO.msg = uploadfile.FileName;
                        fileUploadDTO.filename = uploadfile.FileName;
                    }
                    else
                    {
                        string filePath = Path.Combine(dir, Path.GetFileName(uploadfile.FileName));
                        using (Stream fileStream = new FileStream(dir, FileMode.Create, FileAccess.Write))
                        {
                            uploadfile.CopyTo(fileStream);
                            fileUploadDTO.success = true;
                            fileUploadDTO.msg = uploadfile.FileName;
                       
                            fileUploadDTO.filename = uploadfile.FileName;

                        }

                        ViewBag.Message = "Requerimiento  guardado correctamente";
                    }

                }
                catch (Exception ex)
                {
                    fileUploadDTO.success = false;
                    fileUploadDTO.msg = "error";
                    fileUploadDTO.filename = "";
                    ViewBag.Message = "Error:" + ex.Message.ToString();
                }
            }
            else
            {
                fileUploadDTO.success = false;
                fileUploadDTO.msg = "error2";
                fileUploadDTO.filename = "";

            }
            return JsonConvert.SerializeObject(fileUploadDTO);
        }




        public string DatosSolicitudModeloAprobaciones(int IdGiro)
        {
            GiroDTO oGiroDTO = new GiroDTO();
            GiroDAO oGiroDAO = new GiroDAO();
            oGiroDTO = oGiroDAO.DatosSolicitudRq(IdGiro);
            if (oGiroDTO != null)
            {
                return JsonConvert.SerializeObject(oGiroDTO);
            }
            else
            {
                return "Error";
            }

        }


        public string GenerarReporteSemana(string NombreReporte, string Formato, int IdSemana, int IdObra)
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
            //oWebServiceDTO.Id = Id;
            requestData = JsonConvert.SerializeObject(oWebServiceDTO);


            try
            {
                int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&IdSemana=" + IdSemana + "&IdObra=" + IdObra + "&IdSociedad=" + IdSociedad;
                //cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReportCrystal";
                //cadenaUri = "http://192.168.0.209/ReporteCrystal/ReportCrystal.asmx/ObtenerReportCrystal";
                cadenaUri = "http://192.168.0.209/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteSemanaGiro";

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
        public string ObtenerGiroAprobado(int IdObra)
        {
            string mensaje_error = "";
            GiroDAO oGiroDAO = new GiroDAO();
            List<GiroDTO> lstSemanaDTO = oGiroDAO.ObtenerGiroxAprobado(IdObra, ref mensaje_error);

            if (lstSemanaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSemanaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


    }
}
  