using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;

namespace ConcyssaWeb.Controllers
{
    public class SolicitudRQAutorizacionController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        //public ActionResult GenerarPDF(string Id)
        //{



        //SolicitudRQModeloAprobacionesDAO solicitudRQModeloAprobacionesDAO = new SolicitudRQModeloAprobacionesDAO();
        //SolicitudRQDAO solicitudRQDAO = new SolicitudRQDAO();
        //UsuarioDAO usuarioDAO = new UsuarioDAO();
        //IndicadorImpuestoDAO indicadorImpuestoDAO = new IndicadorImpuestoDAO();
        //MonedaDAO monedaDAO = new MonedaDAO();
        //DepartamentoDAO departamentoDAO = new DepartamentoDAO();
        //ConsultasHana consultasHana = new ConsultasHana();

        //List<SolicitudRQDTO> lstsolicitudRQDTO = new List<SolicitudRQDTO>();


        //lstsolicitudRQDTO = solicitudRQModeloAprobacionesDAO.istarDetalleItemAprobados(int.Parse(Id), int.Parse(IdUsuario.ToString()), 13); //etapa 13

        //if (lstsolicitudRQDTO[0].Detalle.Count == 0)
        //{
        //    lstsolicitudRQDTO = solicitudRQDAO.ObtenerDatosxID(int.Parse(Id));
        //}

        //var Solicitante = usuarioDAO.ObtenerDatosxID(lstsolicitudRQDTO[0].IdSolicitante);
        //var Impuesto = indicadorImpuestoDAO.ObtenerDatosxID(lstsolicitudRQDTO[0].IdIndicadorImpuesto);

        ////var Moneda = monedaDAO.ObtenerDatosxID(lstsolicitudRQDTO[0].IdMoneda);
        //var Moneda = consultasHana.ListarMonedaxIDHana(lstsolicitudRQDTO[0].IdMoneda);

        ////var Area = departamentoDAO.ObtenerDatosxID(lstsolicitudRQDTO[0].IdDepartamento);
        //var Area = consultasHana.ListarDepartamentosxIDHana(lstsolicitudRQDTO[0].IdDepartamento.ToString());

        //ViewBag.Solicitante = Solicitante[0].NombreUsuario;
        //ViewBag.Impuesto = Impuesto[0].Descripcion;
        //ViewBag.Moneda = Moneda[0].Descripcion;
        //ViewBag.Area = Area[0].Descripcion;

        //List<SolicitudDetallePDFDTO> detalle = new List<SolicitudDetallePDFDTO>();

        //for (int i = 0; i < lstsolicitudRQDTO[0].Detalle.Count; i++)
        //{
        //    SolicitudDetallePDFDTO soli = new SolicitudDetallePDFDTO();
        //    soli.IdArticulo = lstsolicitudRQDTO[0].Detalle[i].IdArticulo;
        //    var UnidadMedida = consultasHana.ListarUnidadMedidaxIDHana(lstsolicitudRQDTO[0].Detalle[i].IdUnidadMedida);
        //    if (UnidadMedida.Count > 0)
        //    {
        //        soli.UnidadMedida = UnidadMedida[0].Descripcion;
        //    }
        //    else
        //    {
        //        soli.UnidadMedida = "-";
        //    }

        //    soli.CantidadNecesaria = lstsolicitudRQDTO[0].Detalle[i].CantidadNecesaria;
        //    soli.PrecioInfo = lstsolicitudRQDTO[0].Detalle[i].PrecioInfo;
        //    var IndicadorImpuesto = indicadorImpuestoDAO.ObtenerDatosxID(lstsolicitudRQDTO[0].Detalle[i].IdIndicadorImpuesto);
        //    if (IndicadorImpuesto.Count > 0)
        //    {
        //        soli.IndicadorImpuesto = IndicadorImpuesto[0].Descripcion;
        //    }
        //    else
        //    {
        //        soli.IndicadorImpuesto = "-";
        //    }

        //    soli.ItemTotal = lstsolicitudRQDTO[0].Detalle[i].ItemTotal;
        //    var CentroCosto = consultasHana.ListarCentroCostoxIDHana(lstsolicitudRQDTO[0].Detalle[i].IdCentroCostos);
        //    if (CentroCosto.Count > 0)
        //    {
        //        soli.CentroCostos = CentroCosto[0].Descripcion;
        //    }
        //    else
        //    {
        //        soli.CentroCostos = "";
        //    }

        //    soli.Proyecto = lstsolicitudRQDTO[0].Detalle[i].IdProyecto;

        //    if (lstsolicitudRQDTO[0].Detalle[i].EstadoItemAutorizado == 2)
        //    {
        //        soli.EstadoItemAutorizado = "Aprobado";
        //    }
        //    else if (lstsolicitudRQDTO[0].Detalle[i].EstadoItemAutorizado == 3)
        //    {
        //        soli.EstadoItemAutorizado = "Rechazado";
        //    }
        //    else
        //    {
        //        soli.EstadoItemAutorizado = "Pendiente";
        //    }


        //    if (lstsolicitudRQDTO[0].IdClaseArticulo == 2)
        //    {
        //        //var DescripcionItem = consultasHana.ListarServicioxIDDescripcionHana(lstsolicitudRQDTO[0].Detalle[i].IdArticulo);
        //        soli.DescripcionItem = lstsolicitudRQDTO[0].Detalle[i].Descripcion;
        //    }
        //    else if (lstsolicitudRQDTO[0].IdClaseArticulo == 3)
        //    {
        //        soli.DescripcionItem = lstsolicitudRQDTO[0].Detalle[i].Descripcion;
        //    }
        //    else
        //    {
        //        var DescripcionItem = consultasHana.ListarProductosxIDDescripcionHana(lstsolicitudRQDTO[0].Detalle[i].IdArticulo);
        //        soli.DescripcionItem = DescripcionItem[0].Descripcion1;
        //    }


        //    detalle.Add(soli);
        //}

        //ViewBag.Detalle = detalle;


        //string fileName = Id + "-Solicitud-" + lstsolicitudRQDTO[0].Serie + "-" + lstsolicitudRQDTO[0].Numero + ".pdf";
        //string filePath = Server.MapPath("~/Anexos/" + fileName);


        //var PDF = new Rotativa.ViewAsPdf("ReportePDF", lstsolicitudRQDTO)
        //{
        //    FileName = fileName
        //};

        //byte[] applicationPDFData = PDF.BuildFile(ControllerContext);

        //var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
        //fileStream.Write(applicationPDFData, 0, applicationPDFData.Length);
        //fileStream.Close();
        //    var PDF;
        //    return PDF;

        //}
        public ActionResult ReportePDF()
        {
            return View();
        }
        public string ObtenerSolicitudesxAutorizar(string FechaInicio, string FechaFinal, int Estado, int IdAutorizador)
        {
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }



            string Resultado = "";
            if (IdAutorizador != 0)
            {
                SolicitudRQAutorizacionDAO oSolicitudRQAutorizacionDAO = new SolicitudRQAutorizacionDAO();

                List<SolicitudRQAutorizacionDTO> lstSolicitudRQAutorizacionDTO = oSolicitudRQAutorizacionDAO.ObtenerSolicitudesxAutorizar(IdAutorizador.ToString(), IdSociedad.ToString(), FechaInicio, FechaFinal, Estado);
                if (lstSolicitudRQAutorizacionDTO.Count > 0)
                {
                    Resultado = JsonConvert.SerializeObject(lstSolicitudRQAutorizacionDTO);
                }
                else
                {
                    Resultado = "error";
                }
            }
            else
            {
                SolicitudRQAutorizacionDAO oSolicitudRQAutorizacionDAO = new SolicitudRQAutorizacionDAO();
                List<SolicitudRQAutorizacionDTO> lstSolicitudRQAutorizacionDTO = oSolicitudRQAutorizacionDAO.ObtenerSolicitudesxAutorizar(IdUsuario.ToString(), IdSociedad.ToString(), FechaInicio, FechaFinal, Estado);
                if (lstSolicitudRQAutorizacionDTO.Count > 0)
                {
                    Resultado = JsonConvert.SerializeObject(lstSolicitudRQAutorizacionDTO);
                }
                else
                {
                    Resultado = "error";
                }
            }
            return Resultado;


        }

        public bool ValidarSipuedeAprobar(int IdSolicitudRQ, int IdEtapa)
        {
            SolicitudRQAutorizacionDAO oSolicitudRQAutorizacionDAO = new SolicitudRQAutorizacionDAO();
            int puedeentrar = oSolicitudRQAutorizacionDAO.ValidarSipuedeAprobar(IdSolicitudRQ, IdEtapa);
            if (puedeentrar == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public int UpdateInsertModeloAprobaciones(List<SolicitudRQModeloAprobacionesDTO> solicitudRQModeloAprobacionesDTO)
        {
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int valida = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }



            SolicitudRQModeloAprobacionesDAO oSolicitudRQModeloAprobacionesDAO = new SolicitudRQModeloAprobacionesDAO();
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            SolicitudRQModeloDAO oSolicitudRQModeloDAO = new SolicitudRQModeloDAO();
            EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            int resultado = oSolicitudRQModeloAprobacionesDAO.UpdateInsertModeloAprobaciones(solicitudRQModeloAprobacionesDTO, IdSociedad.ToString());
            if (resultado != 0)
            {

                //Valida si hay cambio en precio o cantidad y realiza la actualizacion dependiendo de eso
                var DatosSolicitud = oSolicitudRQDAO.ObtenerDatosxID(solicitudRQModeloAprobacionesDTO[0].IdSolicitud);
                for (int i = 0; i < solicitudRQModeloAprobacionesDTO.Count; i++) //datos enviados de aprobador
                {
                    for (int j = 0; j < DatosSolicitud[0].Detalle.Count; j++) //datos de solicitud
                    {
                        if (solicitudRQModeloAprobacionesDTO[i].IdArticulo == DatosSolicitud[0].Detalle[j].IdArticulo)
                        {
                            if (solicitudRQModeloAprobacionesDTO[i].CantidadItem != DatosSolicitud[0].Detalle[j].CantidadNecesaria)
                            {
                                oSolicitudRQModeloAprobacionesDAO.ActualizarCantidadPrecioDetalle(solicitudRQModeloAprobacionesDTO[i], IdSociedad.ToString());
                            }
                            if (solicitudRQModeloAprobacionesDTO[i].PrecioItem != DatosSolicitud[0].Detalle[j].PrecioInfo)
                            {
                                oSolicitudRQModeloAprobacionesDAO.ActualizarCantidadPrecioDetalle(solicitudRQModeloAprobacionesDTO[i], IdSociedad.ToString());
                            }
                        }
                    }
                }
                //Valida si hay cambio en precio o cantidad y realiza la actualizacion dependiendo de eso


                var SolicitudRQModeloResult = oSolicitudRQModeloDAO.ObtenerDatosxID(solicitudRQModeloAprobacionesDTO[0].IdSolicitudModelo, IdSociedad.ToString());
                var EtapasAutorizacionResult = oEtapaAutorizacionDAO.ObtenerDatosxID(SolicitudRQModeloResult[0].IdEtapa);

                for (int i = 0; i < solicitudRQModeloAprobacionesDTO.Count; i++)
                {

                    //for (int j = 0; j < SolicitudRQModeloResult.Count; j++)
                    //{
                    //encuentras las etapas


                    //valida cuantas autorizaciones tiene el item
                    int validarItemsAutorizados = oSolicitudRQModeloAprobacionesDAO.ValidarItemsAutorizados(solicitudRQModeloAprobacionesDTO[i].IdSolicitudModelo, solicitudRQModeloAprobacionesDTO[i].IdArticulo, solicitudRQModeloAprobacionesDTO[i].IdDetalle);
                    int validarItemsRechazados = oSolicitudRQModeloAprobacionesDAO.ValidarItemsDesAutorizados(solicitudRQModeloAprobacionesDTO[i].IdSolicitudModelo, solicitudRQModeloAprobacionesDTO[i].IdArticulo, solicitudRQModeloAprobacionesDTO[i].IdDetalle);

                    if (EtapasAutorizacionResult[0].AutorizacionesRequeridas == validarItemsAutorizados)
                    {
                        var ActualizarEstadoDisabledItem = oSolicitudRQModeloAprobacionesDAO.ActualizarEstadoDisabledItem(solicitudRQModeloAprobacionesDTO[i], IdSociedad.ToString());
                    }

                    if (EtapasAutorizacionResult[0].RechazosRequeridos == validarItemsRechazados)
                    {
                        var ActualizarEstadoDisabledItem = oSolicitudRQModeloAprobacionesDAO.ActualizarEstadoDisabledItem(solicitudRQModeloAprobacionesDTO[i], IdSociedad.ToString());
                    }

                    if (solicitudRQModeloAprobacionesDTO[i].Accion == 4)
                    {
                        oSolicitudRQModeloAprobacionesDAO.ActualizarEstadoDisabledItem(solicitudRQModeloAprobacionesDTO[i], IdSociedad.ToString());
                    }
                    //}

                }



                var DatosItemsAprobados = ObtenerDatosxID(solicitudRQModeloAprobacionesDTO[0].IdSolicitud, solicitudRQModeloAprobacionesDTO[0].IdAutorizador, SolicitudRQModeloResult[0].IdEtapa);
                List<SolicitudRQDTO> lstSolicitudRQDTO = JsonConvert.DeserializeObject<List<SolicitudRQDTO>>(DatosItemsAprobados);


                //enviar correo
                //for (int k = 0; k < EtapasAutorizacionResult[0].Detalles.Count; k++)
                //{
                //    var Solicitante = oUsuarioDAO.ObtenerDatosxID(DatosSolicitud[0].IdSolicitante);
                //    var Autorizadores = oUsuarioDAO.ObtenerDatosxID(EtapasAutorizacionResult[0].Detalles[k].IdUsuario);

                //    EnviarCorreoEstado(solicitudRQModeloAprobacionesDTO[0].IdSolicitud, Autorizadores[0].Correo, Solicitante[0].NombreUsuario, DatosSolicitud[0].Serie, DatosSolicitud[0].Numero, lstSolicitudRQDTO);


                //}
                //enviar correo











                ////Validar cantidad de aprobaciones y rechazo
                //for (int i = 0; i < solicitudRQModeloAprobacionesDTO.Count; i++)
                //{

                //    //valida cuantas autorizaciones tiene el item
                //    int validarItemsAutorizados = oSolicitudRQModeloAprobacionesDAO.ValidarItemsAutorizados(solicitudRQModeloAprobacionesDTO[i].IdSolicitudModelo, solicitudRQModeloAprobacionesDTO[i].IdArticulo);
                //    int validarItemsRechazados = oSolicitudRQModeloAprobacionesDAO.ValidarItemsDesAutorizados(solicitudRQModeloAprobacionesDTO[i].IdSolicitudModelo, solicitudRQModeloAprobacionesDTO[i].IdArticulo);



                //    if (EtapasAutorizacionResult[0].AutorizacionesRequeridas == validarItemsAutorizados)
                //    {
                //        var ActualizarEstadoDisabledItem = oSolicitudRQModeloAprobacionesDAO.ActualizarEstadoDisabledItem(solicitudRQModeloAprobacionesDTO[i], IdSociedad.ToString());
                //    }

                //    if (EtapasAutorizacionResult[0].RechazosRequeridos == validarItemsRechazados)
                //    {
                //        var ActualizarEstadoDisabledItem = oSolicitudRQModeloAprobacionesDAO.ActualizarEstadoDisabledItem(solicitudRQModeloAprobacionesDTO[i], IdSociedad.ToString());
                //    }

                //}


                //var DatosItemsAprobados = ObtenerDatosxID(solicitudRQModeloAprobacionesDTO[0].IdSolicitud, solicitudRQModeloAprobacionesDTO[0].IdAutorizador);
                //List<SolicitudRQDTO> lstSolicitudRQDTO = JsonConvert.DeserializeObject<List<SolicitudRQDTO>>(DatosItemsAprobados);


                //for (int i = 0; i < EtapasAutorizacionResult[0].Detalles.Count; i++)
                //{
                //    var Solicitante = oUsuarioDAO.ObtenerDatosxID(DatosSolicitud[0].IdSolicitante);
                //    var Autorizadores = oUsuarioDAO.ObtenerDatosxID(EtapasAutorizacionResult[0].Detalles[i].IdUsuario);
                //    for (int j = 0; j < Autorizadores.Count; j++)
                //    {
                //        EnviarCorreoEstado(solicitudRQModeloAprobacionesDTO[0].IdSolicitud, Autorizadores[j].Correo, Solicitante[0].NombreUsuario, DatosSolicitud[0].Serie, DatosSolicitud[0].Numero, lstSolicitudRQDTO);
                //    }

                //}






                //for (int i = 0; i < solicitudRQModeloAprobacionesDTO.Count; i++)
                //{
                //    if (solicitudRQModeloAprobacionesDTO[i].Accion == 1)
                //    {
                //        oSolicitudRQModeloAprobacionesDAO.AprobarSolicitudRQ(solicitudRQModeloAprobacionesDTO[i].IdSolicitud, solicitudRQModeloAprobacionesDTO[i].IdSolicitudModelo);
                //    }
                //    else
                //    {
                //        oSolicitudRQModeloAprobacionesDAO.RechazarSolicitudRQ(solicitudRQModeloAprobacionesDTO[i].IdSolicitud, solicitudRQModeloAprobacionesDTO[i].IdSolicitudModelo);
                //    }

                //    var ValidarSiEstaAceptado = oSolicitudRQDAO.ObtenerDatosxID(solicitudRQModeloAprobacionesDTO[i].IdSolicitud);
                //    var SolicitudRQModeloResult = oSolicitudRQModeloDAO.ObtenerDatosxID(solicitudRQModeloAprobacionesDTO[i].IdSolicitudModelo, IdSociedad.ToString());
                //    var EtapasAutorizacionResult = oEtapaAutorizacionDAO.ObtenerDatosxID(SolicitudRQModeloResult[0].IdEtapa);
                //    UsuarioDAO oUsuarioDAO = new UsuarioDAO();

                //    var Solicitante = oUsuarioDAO.ObtenerDatosxID(ValidarSiEstaAceptado[0].IdSolicitante);

                //    //solicitud aprobada
                //    if (ValidarSiEstaAceptado[0].Estado == 2)
                //    {
                //        //enviar correo a usuarios
                //        for (int k = 0; k < EtapasAutorizacionResult[0].Detalles.Count; k++)
                //        {
                //            var Usuario = oUsuarioDAO.ObtenerDatosxID(EtapasAutorizacionResult[0].Detalles[k].IdUsuario);
                //            EnviarCorreoEstado(solicitudRQModeloAprobacionesDTO[i].IdSolicitud, Usuario[0].Correo, Solicitante[0].NombreUsuario, ValidarSiEstaAceptado[0].Serie, ValidarSiEstaAceptado[0].Numero,"Aprobo");
                //        }
                //        //envia correo a solicitante
                //        EnviarCorreoEstado(solicitudRQModeloAprobacionesDTO[i].IdSolicitud, Solicitante[0].Correo, Solicitante[0].NombreUsuario, ValidarSiEstaAceptado[0].Serie, ValidarSiEstaAceptado[0].Numero, "Aprobo");
                //    }
                //    //solicitud desaprobada
                //    else if (ValidarSiEstaAceptado[0].Estado == 3)
                //    {
                //        //enviar correo a usuarios
                //        for (int k = 0; k < EtapasAutorizacionResult[0].Detalles.Count; k++)
                //        {
                //            var Usuario = oUsuarioDAO.ObtenerDatosxID(EtapasAutorizacionResult[0].Detalles[k].IdUsuario);
                //            EnviarCorreoEstado(solicitudRQModeloAprobacionesDTO[i].IdSolicitud, Usuario[0].Correo, Solicitante[0].NombreUsuario, ValidarSiEstaAceptado[0].Serie, ValidarSiEstaAceptado[0].Numero,"Desaprobo");
                //        }
                //        //envia correo a solicitante
                //         EnviarCorreoEstado(solicitudRQModeloAprobacionesDTO[i].IdSolicitud, Solicitante[0].Correo, Solicitante[0].NombreUsuario, ValidarSiEstaAceptado[0].Serie, ValidarSiEstaAceptado[0].Numero, "Desaprobo");
                //    }

                //}

                //resultado = 1;
            }

            return resultado;

        }


        public string ObtenerDatosxID(int IdSolicitudRQ, int IdAprobador, int IdEtapa)
        {
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }



            string Resultado = "";

            SolicitudRQModeloAprobacionesDAO solicitudRQModeloAprobacionesDAO = new SolicitudRQModeloAprobacionesDAO();
            SolicitudRQDAO solicitud = new SolicitudRQDAO();

            var existe = solicitudRQModeloAprobacionesDAO.istarDetalleItemAprobados(IdSolicitudRQ, IdAprobador, IdEtapa);
            var DetalleRQ = solicitud.ObtenerDatosxID(IdSolicitudRQ);

            if (existe[0].Detalle.Count == DetalleRQ[0].Detalle.Count)
            {
                SolicitudRQDAO oSolicitudDAO = new SolicitudRQDAO();
                List<SolicitudRQDTO> lstSolicitudRQDTO = solicitudRQModeloAprobacionesDAO.istarDetalleItemAprobados(IdSolicitudRQ, IdAprobador, IdEtapa);

                if (lstSolicitudRQDTO.Count > 0)
                {
                    Resultado = JsonConvert.SerializeObject(lstSolicitudRQDTO);
                }
                else
                {
                    Resultado = "error";
                }
            }
            else
            {
                SolicitudRQDAO oSolicitudDAO = new SolicitudRQDAO();
                //List<SolicitudRQDTO> lstSolicitudRQDTO = oSolicitudDAO.ObtenerDatosxID(IdSolicitudRQ);
                List<SolicitudRQDTO> lstSolicitudRQDTO = oSolicitudDAO.ObtenerDatosxIDNuevo(IdSolicitudRQ, IdAprobador, IdEtapa);


                if (lstSolicitudRQDTO.Count > 0)
                {
                    Resultado = JsonConvert.SerializeObject(lstSolicitudRQDTO);
                }
                else
                {
                    Resultado = "error";
                }
            }
            return Resultado;

        }



        public void EnviarCorreoEstado(int IdSolicitud, string Correo, string Solicitante, string Serie, int Numero, List<SolicitudRQDTO> lstSolicitudRQDTO)
        //public void EnviarCorreo()
        {


            string body;
            body = TemplateEmail(Serie, Numero, Solicitante, lstSolicitudRQDTO);

            //body = "<body>" +
            //    "<h2>Se "+Estado+" una Solicitud</h2>" +
            //    "<h4>Detalles de Solicitud:</h4>" +
            //    "<span>N° Solicitud: " + Serie + "-" + Numero + "</span>" +
            //    "<br/><span>Solicitante: " + Solicitante + "</span>" +
            //    "</body>";

            string msge = "";
            string from = "concyssa.smc@gmail.com";
            string correo = from;
            string password = "tlbvngkvjcetzunr";
            string displayName = "SMC - Proceso de Autorizacion";
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from, displayName);
            mail.To.Add(Correo);

            mail.Subject = "Autorizacion";
            mail.Body = body;

            mail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Aquí debes sustituir tu servidor SMTP y el puerto
            client.Credentials = new NetworkCredential(from, password);
            client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false



            string pathPDF = IdSolicitud + "-Solicitud-" + Serie + "-" + Numero;

            //string path = "C:\\inetpub\\wwwroot\\Binario\\Anexos\\" + pathPDF + ".pdf";
            string path = "C:\\Users\\soporte.sap\\source\\repos\\SMC_AddonRequerimientos\\SMC_AddonRequerimientos\\Anexos\\" + pathPDF + ".pdf";
            bool result = System.IO.File.Exists(path);
            if (result == true)
            { }
            else
            {
                //GenerarPDF(IdSolicitud.ToString());
            }

            //Attachment archivo = new Attachment("C:\\inetpub\\wwwroot\\Binario\\Anexos\\" + pathPDF + ".pdf");
            Attachment archivo = new Attachment("C:\\Users\\soporte.sap\\source\\repos\\SMC_AddonRequerimientos\\SMC_AddonRequerimientos\\Anexos\\" + pathPDF + ".pdf");
            mail.Attachments.Add(archivo);

            client.Send(mail);



        }



        public string TemplateEmail(string Serie, int Numero, string Solicitante, List<SolicitudRQDTO> lstSolicitudRQDTO)
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
                                                Informacion de la Solicitud</h2>
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
                                                        valign='top'>
                                                            <table class='invoice-items' cellpadding='0' cellspacing='0'
                                                               style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; margin: 0;'>";


            //if (lstSolicitudRQDTO[0].Detalle.Count() > 0)
            {
                for (int i = 0; i < lstSolicitudRQDTO[0].Detalle.Count(); i++)
                {
                    string estadoItem = "";
                    if (lstSolicitudRQDTO[0].Detalle[i].EstadoItemAutorizado == 1)
                    {
                        estadoItem = @"<strong>Pendiente</strong>";
                    }
                    else if (lstSolicitudRQDTO[0].Detalle[i].EstadoItemAutorizado == 2)
                    {
                        estadoItem = @"<strong>Aprobado</strong>";
                    }
                    else
                    {
                        estadoItem = @"<strong>Rechazado</strong>";
                    }

                    //ConsultasHana consultas = new ConsultasHana();
                    List<ArticuloDTO> Item;
                    var DescripcionItem = "";
                    if (lstSolicitudRQDTO[0].IdClaseArticulo == 2)
                    {
                        //Item = consultas.ListarServicioxIDDescripcionHana(lstSolicitudRQDTO[0].Detalle[i].IdArticulo);
                        //bodyhtml += @"<tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                        //                                        <td style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; border-top-width: 1px; border-top-color: #eee; border-top-style: solid; margin: 0; padding: 5px 0;'
                        //                                            valign='top'>" + Item[0].Descripcion1 + @"
                        //                                        </td>
                        //                                        <td class='alignright'
                        //                                            style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; text-align: right; border-top-width: 1px; border-top-color: #eee; border-top-style: solid; margin: 0; padding: 5px 0;'
                        //                                            align='right' valign='top'>" + estadoItem + @"
                        //                                        </td>
                        //                                    </tr>";
                    }
                    else
                    {
                        DescripcionItem = lstSolicitudRQDTO[0].Detalle[i].Descripcion;//consultas.ListarProductosxIDDescripcionHana(lstSolicitudRQDTO[0].Detalle[i].IdArticulo);
                        bodyhtml += @"<tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                                                <td style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; border-top-width: 1px; border-top-color: #eee; border-top-style: solid; margin: 0; padding: 5px 0;'
                                                                    valign='top'>" + DescripcionItem + @"
                                                                </td>
                                                                <td class='alignright'
                                                                    style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; text-align: right; border-top-width: 1px; border-top-color: #eee; border-top-style: solid; margin: 0; padding: 5px 0;'
                                                                    align='right' valign='top'>" + estadoItem + @"
                                                                </td>
                                                            </tr>";
                    }




                }
            }




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
                                            <a href='https://smartcode.pe/'
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



        public int PasarPendiente(int IdSolicitud)
        {
            SolicitudRQAutorizacionDAO solicitud = new SolicitudRQAutorizacionDAO();
            int resultado = solicitud.PasarPendiente(IdSolicitud);
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
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
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
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            if (IdSociedad == null)
            {
                return -999;
            }
            return rpta;
        }

    }
}
