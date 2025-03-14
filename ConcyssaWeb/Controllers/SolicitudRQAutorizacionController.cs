using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Xml;

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
        public string ObtenerSolicitudesxAutorizar(string FechaInicio, string FechaFinal, int Estado, int IdAutorizador, int IdObra)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
           
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }

            IdAutorizador = IdUsuario;
            string mensaje_error = "";
            string Resultado = "";
            if (IdAutorizador != 0)
            {
                SolicitudRQAutorizacionDAO oSolicitudRQAutorizacionDAO = new SolicitudRQAutorizacionDAO();
                //List<SolicitudRQAutorizacionDTO> lstSolicitudRQAutorizacionDTO = oSolicitudRQAutorizacionDAO.ObtenerSolicitudesxAutorizar(IdAutorizador.ToString(), IdSociedad.ToString(), FechaInicio, FechaFinal, Estado);
                List<DetalleSolicitudRqAprobacionDTO> lstDetalleSolicitudRqAprobacionDTO = oSolicitudRQAutorizacionDAO.ObtenerSolicitudesxAutorizarDetalle(IdUsuario.ToString(), IdSociedad.ToString(), FechaInicio, FechaFinal, Estado,IdObra,BaseDatos, ref mensaje_error);
                if (lstDetalleSolicitudRqAprobacionDTO.Count > 0)
                {
                  
                    Resultado = JsonConvert.SerializeObject(lstDetalleSolicitudRqAprobacionDTO);
                }
                else
                {
                    Resultado = "error";
                }
            }
            else
            {
                SolicitudRQAutorizacionDAO oSolicitudRQAutorizacionDAO = new SolicitudRQAutorizacionDAO();
                //List<SolicitudRQAutorizacionDTO> lstSolicitudRQAutorizacionDTO = oSolicitudRQAutorizacionDAO.ObtenerSolicitudesxAutorizar(IdUsuario.ToString(), IdSociedad.ToString(), FechaInicio, FechaFinal, Estado);
                List<DetalleSolicitudRqAprobacionDTO> lstDetalleSolicitudRqAprobacionDTO = oSolicitudRQAutorizacionDAO.ObtenerSolicitudesxAutorizarDetalle(IdUsuario.ToString(), IdSociedad.ToString(), FechaInicio, FechaFinal, Estado, IdObra, BaseDatos, ref mensaje_error);
                if (lstDetalleSolicitudRqAprobacionDTO.Count > 0)
                {
                    Resultado = JsonConvert.SerializeObject(lstDetalleSolicitudRqAprobacionDTO);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            SolicitudRQAutorizacionDAO oSolicitudRQAutorizacionDAO = new SolicitudRQAutorizacionDAO();
            int puedeentrar = oSolicitudRQAutorizacionDAO.ValidarSipuedeAprobar(IdSolicitudRQ, IdEtapa,BaseDatos);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int valida = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }

            List<SolicitudRQModeloAprobacionesDTO> Rechazos = new List<SolicitudRQModeloAprobacionesDTO>();

            SolicitudRQModeloAprobacionesDAO oSolicitudRQModeloAprobacionesDAO = new SolicitudRQModeloAprobacionesDAO();
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            SolicitudRQModeloDAO oSolicitudRQModeloDAO = new SolicitudRQModeloDAO();
            EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            int resultado = oSolicitudRQModeloAprobacionesDAO.UpdateInsertModeloAprobaciones(solicitudRQModeloAprobacionesDTO, IdSociedad.ToString(),BaseDatos);
            if (resultado != 0)
            {

                

                //Valida si hay cambio en precio o cantidad y realiza la actualizacion dependiendo de eso
                for (int i = 0; i < solicitudRQModeloAprobacionesDTO.Count; i++)
                {

                    if (solicitudRQModeloAprobacionesDTO[i].Accion == 1)
                    {
                        Rechazos.Add(solicitudRQModeloAprobacionesDTO[i]);
                    }


                    var DatosSolicitud = oSolicitudRQDAO.ObtenerDatosxID(solicitudRQModeloAprobacionesDTO[i].IdSolicitud, BaseDatos);
                    //{
                        for (int j = 0; j < DatosSolicitud[0].Detalle.Count; j++) //datos de solicitud
                        {
                            if (solicitudRQModeloAprobacionesDTO[i].IdArticulo == DatosSolicitud[0].Detalle[j].IdArticulo)
                            {
                                if (solicitudRQModeloAprobacionesDTO[i].CantidadItem != DatosSolicitud[0].Detalle[j].CantidadNecesaria)
                                {
                                    oSolicitudRQModeloAprobacionesDAO.ActualizarCantidadPrecioDetalle(solicitudRQModeloAprobacionesDTO[i], IdSociedad.ToString(),BaseDatos);
                                }
                            }
                        }
                    //}
                }

               
                //Valida si hay cambio en precio o cantidad y realiza la actualizacion dependiendo de eso


                var SolicitudRQModeloResult = oSolicitudRQModeloDAO.ObtenerDatosxID(solicitudRQModeloAprobacionesDTO[0].IdSolicitudModelo, IdSociedad.ToString(),BaseDatos);
                var EtapasAutorizacionResult = oEtapaAutorizacionDAO.ObtenerDatosxID(SolicitudRQModeloResult[0].IdEtapa,BaseDatos);

                for (int i = 0; i < solicitudRQModeloAprobacionesDTO.Count; i++)
                {

                    //valida cuantas autorizaciones tiene el item
                    int validarItemsAutorizados = oSolicitudRQModeloAprobacionesDAO.ValidarItemsAutorizados(solicitudRQModeloAprobacionesDTO[i].IdSolicitudModelo, solicitudRQModeloAprobacionesDTO[i].IdArticulo, solicitudRQModeloAprobacionesDTO[i].IdDetalle,BaseDatos);
                    int validarItemsRechazados = oSolicitudRQModeloAprobacionesDAO.ValidarItemsDesAutorizados(solicitudRQModeloAprobacionesDTO[i].IdSolicitudModelo, solicitudRQModeloAprobacionesDTO[i].IdArticulo, solicitudRQModeloAprobacionesDTO[i].IdDetalle,BaseDatos);

                    if (EtapasAutorizacionResult[0].AutorizacionesRequeridas == validarItemsAutorizados)
                    {
                        var ActualizarEstadoDisabledItem = oSolicitudRQModeloAprobacionesDAO.ActualizarEstadoDisabledItem(solicitudRQModeloAprobacionesDTO[i], IdSociedad.ToString(),BaseDatos);
                    }

                    if (EtapasAutorizacionResult[0].RechazosRequeridos == validarItemsRechazados)
                    {
                        var ActualizarEstadoDisabledItem = oSolicitudRQModeloAprobacionesDAO.ActualizarEstadoDisabledItem(solicitudRQModeloAprobacionesDTO[i], IdSociedad.ToString(),BaseDatos);
                    }

                    if (solicitudRQModeloAprobacionesDTO[i].Accion == 4)
                    {
                        oSolicitudRQModeloAprobacionesDAO.ActualizarEstadoDisabledItem(solicitudRQModeloAprobacionesDTO[i], IdSociedad.ToString(),BaseDatos);
                    }

                }


                // List<SolicitudRQDTO> lstSolicitudRQDTO = JsonConvert.DeserializeObject<List<SolicitudRQDTO>>(DatosItemsAprobados);


                string mensaje_error = "";
                List<SolicitudDetalleDTO> detallesCorreo = new List<SolicitudDetalleDTO>();

                for (int i = 0; i < Rechazos.Count; i++)
                {
                    SolicitudDetalleDTO solicitudRQDetalleDTO = oSolicitudRQDAO.ObtenerSolicitudDetallexId(Rechazos[i].IdDetalle, BaseDatos, ref mensaje_error);
                    detallesCorreo.Add(solicitudRQDetalleDTO);
                }

                if (detallesCorreo.Count > 0) {


                    List<List<SolicitudDetalleDTO>> Lista = detallesCorreo.GroupBy(x => x.IdAlmacen).Select(g =>  g.ToList()).ToList();

                    for (int i = 0; i < Lista.Count; i++)
                    {
                        ConfiguracionSociedadController homeController = new ConfiguracionSociedadController();

                        CorreoDAO oCorreoDAO = new CorreoDAO();
                        CorreoDTO oCorreoDTO = oCorreoDAO.ObtenerDatosCorreo("GENERAL", BaseDatos, ref mensaje_error);

                        List<string> Correos = new List<string>();
                        Correos.Add("garrieta@concyssa.com");
                        Correos.Add(Lista[i][0].CorreoAlmacen);

                        homeController.EnviarCorreo2025(oCorreoDTO, "REQUERIMIENTOS DESAPROBADOS", Correos, CuerpoCorreoRechazo(Lista[i]), BaseDatos, ref mensaje_error);

                    }

                }

            }

            return resultado;

        }


        public string ObtenerDatosxID(int IdSolicitudRQ, int IdAprobador, int IdEtapa)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }



            string Resultado = "";

            SolicitudRQModeloAprobacionesDAO solicitudRQModeloAprobacionesDAO = new SolicitudRQModeloAprobacionesDAO();
            SolicitudRQDAO solicitud = new SolicitudRQDAO();

            var existe = solicitudRQModeloAprobacionesDAO.istarDetalleItemAprobados(IdSolicitudRQ, IdAprobador, IdEtapa,BaseDatos);
            var DetalleRQ = solicitud.ObtenerDatosxID(IdSolicitudRQ,BaseDatos);

            if (existe[0].Detalle.Count == DetalleRQ[0].Detalle.Count)
            {
                SolicitudRQDAO oSolicitudDAO = new SolicitudRQDAO();
                List<SolicitudRQDTO> lstSolicitudRQDTO = solicitudRQModeloAprobacionesDAO.istarDetalleItemAprobados(IdSolicitudRQ, IdAprobador, IdEtapa,BaseDatos);

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
                List<SolicitudRQDTO> lstSolicitudRQDTO = oSolicitudDAO.ObtenerDatosxIDNuevo(IdSolicitudRQ, IdAprobador, IdEtapa,BaseDatos);


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




        public int PasarPendiente(int IdSolicitud)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            SolicitudRQAutorizacionDAO solicitud = new SolicitudRQAutorizacionDAO();
            int resultado = solicitud.PasarPendiente(IdSolicitud,BaseDatos);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;
        }



        public string validarEmpresaActual()
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
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

        public string CuerpoCorreoRechazo(List<SolicitudDetalleDTO> lstSolicitudDetalleDTO)
        {
            string html = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN'
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
                            <td class='container' width='900'
                                style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 900px !important; clear: both !important; margin: 0 auto;'
                                valign='top'>
                                <div class='content'
                                     style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; max-width: 900px; display: block; margin: 0 auto; padding: 20px;'>
                                    <table class='main' width='100%' cellpadding='0' cellspacing='0'
                                           style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-radius: 3px;  margin: 0'>
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
                                                            Requerimientos Rechazados</h2>
                                                        </td>
                                                    </tr>
                                                    <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                                        <td class='content-block aligncenter'
                                                            style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; text-align: center; margin: 0; padding: 0 0 20px;'
                                                            align='center' valign='top'>
                                                            <table class='invoice'
                                                                   style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; text-align: left; width: 90%; margin: auto;'>
                                                                <tr align='center'  style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                                                    <td style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 5px 0;'
                                                                        valign='top'><span>Estimado/a:
                                                
                                                                            Se notificada que los siguientes Correos Item no fueron aprobados:<br>
                                                
                                                                           </span>
                                                                    </td>
                                                                </tr>
                                                
                                                            </table>
                              
                                                            <table class='invoice'
                                                            style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; text-align: left; width: 90%; margin: auto;'>
                                                            <thead style='background-color: #ccc;'>
                                                                <th align='center'  style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                                                    NRO. REQ.
                                                                </th>
                                                                <th align='center'  style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                                                   ITEM
                                                                </th>
                                                                <th align='center'  style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                                                    CANTIDAD
                                                                </th>
                                                            </thead>
                                                            <tbody style='background-color: #eee;'>";

                                                                for (int i = 0; i < lstSolicitudDetalleDTO.Count; i++)
                                                                {
                                                                    html += @"  <tr align='left'  style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0'>
                                                                         <td style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 5px 0;'
                                                                             valign='top'><span>"+ lstSolicitudDetalleDTO[i].NumeroSerie + @"</span>
                                                                         </td>
                                                                         <td style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 5px 0;'
                                                                             valign='top'><span>"+ lstSolicitudDetalleDTO[i].CodArticulo +"-"+ lstSolicitudDetalleDTO[i].DescripcionItem + @"</span>
                                                                         </td>
                                                                         <td style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 5px 0;'
                                                                             valign='top'><span>"+ Math.Round(lstSolicitudDetalleDTO[i].CantidadSolicitada,2) + @"</span>
                                                                         </td>
                                                                     </tr>";
                                                                }
 
                                                           
                                                            html += @"</tbody>
                                
                                   
                                                        </table>                       
                               
                                                    </table>
                                                        </td>
                                                    </tr>                  
                                                </table>
                                            </td>
                                        </tr>
                                    </table>     
                                </div>
                            </td>
                            </tr>
                            </table>";

            return html;
        }
     
    }
}
