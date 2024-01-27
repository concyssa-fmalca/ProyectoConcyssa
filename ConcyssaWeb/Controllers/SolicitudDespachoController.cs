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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            //DataTableParameter dtp = JsonConvert.DeserializeObject<DataTableParameter>(ClientParameters);

            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = oSolicitudDespachoDAO.ObtenerSolicitudesDespacho(IdUsuario.ToString(), IdSociedad.ToString(), 0, 99999, "",BaseDatos);



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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = oSolicitudDespachoDAO.ObtenerDatosxID(IdSolicitudDespacho,BaseDatos);

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string mensaje_error = "";
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = oSolicitudDespachoDAO.ObtenerCuadrillaxUsuario(IdUsuario,BaseDatos,ref mensaje_error);

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            //int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string mensaje_error = "";
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = oSolicitudDespachoDAO.ObtenerObraBasexCuadrilla(IdCuadrilla,BaseDatos,ref mensaje_error);

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            var respuesta = oSolicitudDespachoDAO.UpdateInsertSolicitudDespacho(solicitudRQDTO,BaseDatos,ref mensaje_error, IdSociedad.ToString(), IdUsuario.ToString());

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            int resultado = oSolicitudDespachoDAO.Delete(IdSolicitudDespachoDetalle,BaseDatos);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            //DataTableParameter dtp = JsonConvert.DeserializeObject<DataTableParameter>(ClientParameters);

            List<SolicitudDespachoDetalleDTO> lstSolicitudDespachoDetalleDTO = oSolicitudDespachoDAO.ObtenerSolicitudesDespachoxObra(IdSociedad, IdObra,BaseDatos);



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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = oSolicitudDespachoDAO.ObtenerSolicitudesDespachoAtender(IdSociedad, IdBase, FechaInicio, FechaFin, EstadoSolicitud, SerieFiltro,BaseDatos);

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            int respuesta = oSolicitudDespachoDAO.UpdateInsertSolicitudDespachoDetalle(oSolicitudDespachoDetalleDTO,BaseDatos,ref mensaje_error);

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            int respuesta = oSolicitudDespachoDAO.AtencionConfirmada(Cantidad, IdSolicitud, IdArticulo, EstadoSolicitud,BaseDatos,ref mensaje_error);

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
            int respuesta = oSolicitudDespachoDAO.CerrarSolicitud(IdSolicitud, IdUsuario,BaseDatos,ref mensaje_error);

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            if(BaseDatos == "")
            {
                BaseDatos = "AddonsConcyssa";
            }
            //Validar Modelos Aprobacion 
            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            SolicitudDespachoModeloDAO oSolicitudDespachoModeloDAO = new SolicitudDespachoModeloDAO();
            var ModeloAuorizacion = oModeloAutorizacionDAO.VerificarExisteModeloSolicitud(1, 3,BaseDatos); //valida si existe alguina modelo aplicado a documento solicitud Despacho

            if (ModeloAuorizacion.Count > 0)
            {
                for (int i = 0; i < ModeloAuorizacion.Count; i++)
                {
                    var ResultadoModelo = oModeloAutorizacionDAO.ObtenerDatosxID(ModeloAuorizacion[i].IdModeloAutorizacion,BaseDatos);
                    for (int a = 0; a < ResultadoModelo[0].DetallesAutor.Count; a++)
                    {
                        if (ResultadoModelo[0].DetallesAutor[a].IdAutor == oSolcitudDespachoMovil.UsuarioCreador)
                        {
                            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
                            int IdInsert = oSolicitudDespachoDAO.UpdateInsertSolicitudDespachoMovil(oSolcitudDespachoMovil,BaseDatos);
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
                                }, "1",BaseDatos);

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            if (BaseDatos == "")
            {
                BaseDatos = "AddonsConcyssa";
            }
            DataTableDTO oDataTableDTO = new DataTableDTO();
            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();


            List<SolicitudDetalleMovilAprobacion> lstSolicitudDetalleMovilAprobacion = oSolicitudDespachoDAO.ObtenerSolicitudesDespachoDetalleAprobacion(IdUsuario, FechaInicio, FechaFinal, EstadoPR,BaseDatos);

            oDataTableDTO.sEcho = 1;
            oDataTableDTO.iTotalDisplayRecords = lstSolicitudDetalleMovilAprobacion.Count;
            oDataTableDTO.iTotalRecords = lstSolicitudDetalleMovilAprobacion.Count;
            oDataTableDTO.aaData = (lstSolicitudDetalleMovilAprobacion);
            return JsonConvert.SerializeObject(oDataTableDTO);
      
        }


        public int UpdateInsertModeloAprobaciones(SolicitudDespachoModeloAprobacionesDTO oSolicitudDespachoModeloAprobacionesDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            if (BaseDatos == "")
            {
                BaseDatos = "AddonsConcyssa";
            }
            SolicitudDespachoDAO oSolicitudRQModeloAprobacionesDAO = new SolicitudDespachoDAO();

            SolicitudRQModeloDAO oSolicitudRQModeloDAO = new SolicitudRQModeloDAO();
            EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            int resultado = oSolicitudRQModeloAprobacionesDAO.UpdateInsertModeloAprobacionesDespacho(oSolicitudDespachoModeloAprobacionesDTO,BaseDatos);
            if (resultado > 0)
            {
                return 1;
            }
            return resultado;

        }
    }
}
