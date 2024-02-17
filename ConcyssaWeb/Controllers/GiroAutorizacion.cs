using DAO;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class GiroAutorizacion : Controller
    {
        // GET: GiroAutorizacion
        public ActionResult Listado()
        {
            return View();
        }



        public string ObtenerGirosCabeceraxAutorizar(string FechaInicio, string FechaFinal, int Estado, int IdAutorizador, int IdObra)
        {
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;

            string Resultado = "";


            GiroAutorizacionDAO oGiroAutorizacionDAO = new GiroAutorizacionDAO();
            //List<SolicitudRQAutorizacionDTO> lstSolicitudRQAutorizacionDTO = oSolicitudRQAutorizacionDAO.ObtenerSolicitudesxAutorizar(IdUsuario.ToString(), IdSociedad.ToString(), FechaInicio, FechaFinal, Estado);
            List<GiroAprobacionDTO> lstGiroAprobacionDTO = oGiroAutorizacionDAO.ObtenerGiroCabeceraAutorizar(IdUsuario.ToString(), IdSociedad.ToString(), FechaInicio, FechaFinal, Estado,IdObra,BaseDatos);
            if (lstGiroAprobacionDTO.Count > 0)
            {
                Resultado = JsonConvert.SerializeObject(lstGiroAprobacionDTO);
            }
            else
            {
                Resultado = "error";
            }

            return Resultado;


        }




        public string ObtenerGirosxAutorizar(string FechaInicio, string FechaFinal, int Estado, int IdAutorizador,int IdGiro)
        {
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            //string valida = "";
            //valida = validarEmpresaActual();
            //if (valida != "")
            //{
            //    return valida;
            //}



            string Resultado = "";
            if (IdAutorizador != 0)
            {
                GiroAutorizacionDAO oGiroAutorizacionDAO = new GiroAutorizacionDAO();
                //List<SolicitudRQAutorizacionDTO> lstSolicitudRQAutorizacionDTO = oSolicitudRQAutorizacionDAO.ObtenerSolicitudesxAutorizar(IdAutorizador.ToString(), IdSociedad.ToString(), FechaInicio, FechaFinal, Estado);
                List<DetalleGiroAprobacionDTO> lstDetalleGiroAprobacionDTO = oGiroAutorizacionDAO.ObtenerSolicitudesxAutorizarDetalleGiro(IdUsuario.ToString(), IdSociedad.ToString(), FechaInicio, FechaFinal, Estado,IdGiro,BaseDatos);
                if (lstDetalleGiroAprobacionDTO.Count > 0)
                {

                    Resultado = JsonConvert.SerializeObject(lstDetalleGiroAprobacionDTO);
                }
                else
                {
                    Resultado = "error";
                }
            }
            else
            {
                GiroAutorizacionDAO oGiroAutorizacionDAO = new GiroAutorizacionDAO();
                //List<SolicitudRQAutorizacionDTO> lstSolicitudRQAutorizacionDTO = oSolicitudRQAutorizacionDAO.ObtenerSolicitudesxAutorizar(IdUsuario.ToString(), IdSociedad.ToString(), FechaInicio, FechaFinal, Estado);
                List<DetalleGiroAprobacionDTO> lstDetalleGiroAprobacionDTO = oGiroAutorizacionDAO.ObtenerSolicitudesxAutorizarDetalleGiro(IdUsuario.ToString(), IdSociedad.ToString(), FechaInicio, FechaFinal, Estado, IdGiro,BaseDatos);
                if (lstDetalleGiroAprobacionDTO.Count > 0)
                {
                    Resultado = JsonConvert.SerializeObject(lstDetalleGiroAprobacionDTO);
                }
                else
                {
                    Resultado = "error";
                }
            }
            return Resultado;


        }






        public int UpdateInsertModeloAprobacionesGiro(List<GiroModeloAprobacionesDTO> giroModeloAprobacionesDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int valida = 0;
            //valida = validarEmpresaActualUpdateInsert();
            //if (valida != 0)
            //{
            //    return valida;
            //}



            GiroModeloAprobacionesDAO oGiroModeloAprobacionesDAO = new GiroModeloAprobacionesDAO();
            GiroDAO oSolicitudRQDAO = new GiroDAO();
            GiroModeloDAO oSolicitudRQModeloDAO = new GiroModeloDAO();
            EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            int resultado = oGiroModeloAprobacionesDAO.UpdateInsertModeloAprobacionesGiro(giroModeloAprobacionesDTO, IdSociedad.ToString(),BaseDatos);
            //if (resultado != 0)
            //{



            //    var SolicitudRQModeloResult = oSolicitudRQModeloDAO.ObtenerDatosxID(solicitudRQModeloAprobacionesDTO[0].IdSolicitudModelo, IdSociedad.ToString());
            //    var EtapasAutorizacionResult = oEtapaAutorizacionDAO.ObtenerDatosxID(SolicitudRQModeloResult[0].IdEtapa);

            //    for (int i = 0; i < solicitudRQModeloAprobacionesDTO.Count; i++)
            //    {

            //        //valida cuantas autorizaciones tiene el item
            //        int validarItemsAutorizados = oSolicitudRQModeloAprobacionesDAO.ValidarItemsAutorizados(solicitudRQModeloAprobacionesDTO[i].IdSolicitudModelo, solicitudRQModeloAprobacionesDTO[i].IdArticulo, solicitudRQModeloAprobacionesDTO[i].IdDetalle);
            //        int validarItemsRechazados = oSolicitudRQModeloAprobacionesDAO.ValidarItemsDesAutorizados(solicitudRQModeloAprobacionesDTO[i].IdSolicitudModelo, solicitudRQModeloAprobacionesDTO[i].IdArticulo, solicitudRQModeloAprobacionesDTO[i].IdDetalle);

            //        if (EtapasAutorizacionResult[0].AutorizacionesRequeridas == validarItemsAutorizados)
            //        {
            //            var ActualizarEstadoDisabledItem = oSolicitudRQModeloAprobacionesDAO.ActualizarEstadoDisabledItem(solicitudRQModeloAprobacionesDTO[i], IdSociedad.ToString());
            //        }

            //        if (EtapasAutorizacionResult[0].RechazosRequeridos == validarItemsRechazados)
            //        {
            //            var ActualizarEstadoDisabledItem = oSolicitudRQModeloAprobacionesDAO.ActualizarEstadoDisabledItem(solicitudRQModeloAprobacionesDTO[i], IdSociedad.ToString());
            //        }

            //        if (solicitudRQModeloAprobacionesDTO[i].Accion == 4)
            //        {
            //            oSolicitudRQModeloAprobacionesDAO.ActualizarEstadoDisabledItem(solicitudRQModeloAprobacionesDTO[i], IdSociedad.ToString());
            //        }
            //        //}

            //    }



            //    var DatosItemsAprobados = ObtenerDatosxID(solicitudRQModeloAprobacionesDTO[0].IdSolicitud, solicitudRQModeloAprobacionesDTO[0].IdAutorizador, SolicitudRQModeloResult[0].IdEtapa);
            //    List<SolicitudRQDTO> lstSolicitudRQDTO = JsonConvert.DeserializeObject<List<SolicitudRQDTO>>(DatosItemsAprobados);


            //}

            return resultado;

        }







    }
}
