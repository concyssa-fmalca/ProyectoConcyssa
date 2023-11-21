using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Xml;

namespace ConcyssaWeb.Controllers
{
    public class SolicitudDespachoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerSolicitudesDespacho(string ClientParameters)
        {
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            //DataTableParameter dtp = JsonConvert.DeserializeObject<DataTableParameter>(ClientParameters);

            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = oSolicitudDespachoDAO.ObtenerSolicitudesDespacho(IdUsuario.ToString(), IdSociedad.ToString(), 0, 99999, "");



            if (lstSolicitudDespachoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSolicitudDespachoDTO);
            }
            else
            {
                return "error";
            }
        }


        public string ObtenerDatosxID(int IdSolicitudDespacho)
        {

            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = oSolicitudDespachoDAO.ObtenerDatosxID(IdSolicitudDespacho);

            if (lstSolicitudDespachoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSolicitudDespachoDTO);
            }
            else
            {
                return "error";
            }

        }



        public string ObtenerCuadrillaxUsuario()
        {
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string mensaje_error = "";
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = oSolicitudDespachoDAO.ObtenerCuadrillaxUsuario(IdUsuario, ref mensaje_error);

            if (lstSolicitudDespachoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSolicitudDespachoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerObraBasexCuadrilla(int IdCuadrilla)
        {
            //int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string mensaje_error = "";
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = oSolicitudDespachoDAO.ObtenerObraBasexCuadrilla(IdCuadrilla, ref mensaje_error);

            if (lstSolicitudDespachoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSolicitudDespachoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string UpdateInsertSolicitudDespacho(SolicitudDespachoDTO solicitudRQDTO)
        {
            string mensaje_error = "";
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            var respuesta = oSolicitudDespachoDAO.UpdateInsertSolicitudDespacho(solicitudRQDTO, ref mensaje_error, IdSociedad.ToString(), IdUsuario.ToString());

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



        public int EliminarDetalleSolicitud(int IdSolicitudDespachoDetalle)
        {

            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            int resultado = oSolicitudDespachoDAO.Delete(IdSolicitudDespachoDetalle);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }




        public class DataTableParameter
        {
            public int draw { get; set; }
            public int length { get; set; }
            public int start { get; set; }
            public searchtxt search { get; set; }
        }

        public class searchtxt
        {
            public string value { get; set; }
        }

        public struct DataTableResponse<T>
        {
            public int draw;
            public int recordsTotal;
            public int recordsfiltered;
            public List<T> data;
        }
        public string ObtenerSolicitudesDespachoxObra(int IdObra)
        {
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            //DataTableParameter dtp = JsonConvert.DeserializeObject<DataTableParameter>(ClientParameters);

            List<SolicitudDespachoDetalleDTO> lstSolicitudDespachoDetalleDTO = oSolicitudDespachoDAO.ObtenerSolicitudesDespachoxObra(IdSociedad, IdObra);



            if (lstSolicitudDespachoDetalleDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSolicitudDespachoDetalleDTO);
            }
            else
            {
                return "error";
            }
        }
        public string ObtenerSolicitudesDespachoAtender(int IdBase, DateTime FechaInicio, DateTime FechaFin, int EstadoSolicitud, int SerieFiltro)
        {
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = oSolicitudDespachoDAO.ObtenerSolicitudesDespachoAtender(IdSociedad, IdBase, FechaInicio, FechaFin, EstadoSolicitud, SerieFiltro);

            if (lstSolicitudDespachoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSolicitudDespachoDTO);
            }
            else
            {
                return "error";
            }

        }
        public int UpdateInsertSolicitudDespachoDetalle(SolicitudDespachoDetalleDTO oSolicitudDespachoDetalleDTO)
        {
            string mensaje_error = "";
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            int respuesta = oSolicitudDespachoDAO.UpdateInsertSolicitudDespachoDetalle(oSolicitudDespachoDetalleDTO, ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return 0;
            }
            else
            {
                if (respuesta == 0)
                {
                    return 0;
                }
                else
                {
                    return respuesta;
                }
            }
        }
        public int AtencionConfirmada(int Cantidad, int IdSolicitud, int IdArticulo, int EstadoSolicitud)
        {
            string mensaje_error = "";
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            int respuesta = oSolicitudDespachoDAO.AtencionConfirmada(Cantidad, IdSolicitud, IdArticulo, EstadoSolicitud, ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return 0;
            }
            else
            {
                if (respuesta == 0)
                {
                    return 0;
                }
                else
                {
                    return respuesta;
                }
            }
        }
        public int CerrarSolicitud(int IdSolicitud)
        {
            string mensaje_error = "";
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            int respuesta = oSolicitudDespachoDAO.CerrarSolicitud(IdSolicitud, IdUsuario, ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return 0;
            }
            else
            {
                if (respuesta == 0)
                {
                    return 0;
                }
                else
                {
                    return respuesta;
                }
            }
        }

        [HttpPost]
        [Route("SolicitudDespacho/InsertSolicitudDespachoMobile")]
        public int InsertSolicitudDespachoMobile([FromBody] SolcitudDespachoMovil oSolcitudDespachoMovil)
        {
            //Validar Modelos Aprobacion 
            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            SolicitudDespachoModeloDAO oSolicitudDespachoModeloDAO = new SolicitudDespachoModeloDAO();
            var ModeloAuorizacion = oModeloAutorizacionDAO.VerificarExisteModeloSolicitud(1, 3); //valida si existe alguina modelo aplicado a documento solicitud Despacho

            if (ModeloAuorizacion.Count > 0)
            {
                for (int i = 0; i < ModeloAuorizacion.Count; i++)
                {
                    var ResultadoModelo = oModeloAutorizacionDAO.ObtenerDatosxID(ModeloAuorizacion[i].IdModeloAutorizacion);
                    for (int a = 0; a < ResultadoModelo[0].DetallesAutor.Count; a++)
                    {
                        if (ResultadoModelo[0].DetallesAutor[a].IdAutor == oSolcitudDespachoMovil.UsuarioCreador)
                        {
                            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
                            int IdInsert = oSolicitudDespachoDAO.UpdateInsertSolicitudDespachoMovil(oSolcitudDespachoMovil);
                            for (int e = 0; e < ResultadoModelo[0].DetallesEtapa.Count; e++)
                            {
                                var result = oSolicitudDespachoModeloDAO.UpdateInsertSolicitudDespachoModelo(new SolicitudDespachoModeloDTO
                                {
                                    IdSolicitudDespachoModelo = 0,
                                    IdSolicitud = IdInsert,
                                    IdModelo = ResultadoModelo[0].IdModeloAutorizacion,
                                    IdEtapa = ResultadoModelo[0].DetallesEtapa[e].IdEtapa,
                                    Aprobaciones = ResultadoModelo[0].DetallesEtapa[e].AutorizacionesRequeridas,
                                    Rechazos = ResultadoModelo[0].DetallesEtapa[e].RechazosRequeridos
                                }, "1");

                                //var ResultadoEtapa = oEtapaAutorizacionDAO.ObtenerDatosxID(ResultadoModelo[0].DetallesEtapa[e].IdEtapa);
                            }
                            return IdInsert;
                        }
                    }


                }

            }
            return -1;
        }

        public string ObtenerDetalleAprobacionSolicitudDespacho(int IdUsuario, DateTime FechaInicio, DateTime FechaFinal, int EstadoPR)
        {
            DataTableDTO oDataTableDTO = new DataTableDTO();
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();


            List<SolicitudDetalleMovilAprobacion> lstSolicitudDetalleMovilAprobacion = oSolicitudDespachoDAO.ObtenerSolicitudesDespachoDetalleAprobacion(IdUsuario, FechaInicio, FechaFinal, EstadoPR);

            oDataTableDTO.sEcho = 1;
            oDataTableDTO.iTotalDisplayRecords = lstSolicitudDetalleMovilAprobacion.Count;
            oDataTableDTO.iTotalRecords = lstSolicitudDetalleMovilAprobacion.Count;
            oDataTableDTO.aaData = (lstSolicitudDetalleMovilAprobacion);
            return JsonConvert.SerializeObject(oDataTableDTO);
      
        }


        public int UpdateInsertModeloAprobaciones(SolicitudDespachoModeloAprobacionesDTO oSolicitudDespachoModeloAprobacionesDTO)
        {

            SolicitudDespachoDAO oSolicitudRQModeloAprobacionesDAO = new SolicitudDespachoDAO();

            SolicitudRQModeloDAO oSolicitudRQModeloDAO = new SolicitudRQModeloDAO();
            EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            int resultado = oSolicitudRQModeloAprobacionesDAO.UpdateInsertModeloAprobacionesDespacho(oSolicitudDespachoModeloAprobacionesDTO);
            if (resultado > 0)
            {
                return 1;
            }
            return resultado;

        }
    }
}
