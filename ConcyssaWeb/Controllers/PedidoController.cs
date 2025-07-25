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

        public string ObtenerPedidosEntregaLDT(int IdObra, int IdProveedor,string EstadoPedido = "ABIERTO" )
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<PedidoDTO> lstPedidoDTO = oPedidoDAO.ObtenerPedidosEntregaLDT(IdSociedad,BaseDatos, IdProveedor, ref mensaje_error, EstadoPedido, IdObra, IdUsuario);
            List<PedidoDTO> NewlstPedidoDTO = new List<PedidoDTO>();



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


        public string InsertPedidoModeloAut(string JsonDatosEnviar) 
        {
            JsonDatosEnviar = JsonDatosEnviar.Remove(JsonDatosEnviar.Length - 1, 1);
            JsonDatosEnviar = JsonDatosEnviar.Remove(0, 1);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            PedidoDTO oPedidoDTO = JsonConvert.DeserializeObject<PedidoDTO>(JsonDatosEnviar, settings);



            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
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
            int IdInsert = 0;
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ObtenerDatosxID(IdUsuario, BaseDatos, ref mensaje_error);
            PedidoDAO OPedidoDAO = new PedidoDAO();
            if (lstUsuarioDTO[0].CrearOCdirecto || oPedidoDTO.EsSubContrato)
            {
                IdInsert = int.Parse(UpdateInsertPedido(oPedidoDTO));
                OPedidoDAO.AprobarPedido(IdInsert, BaseDatos, ref mensaje_error);

                return IdInsert.ToString(); 
            }


            //Validar Modelos Aprobacion 
            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            var ModeloAuorizacion = oModeloAutorizacionDAO.VerificarExisteModeloSolicitud(1, 4, BaseDatos); //valida si existe alguina modelo aplicado a documento solicitud Despacho

            if (ModeloAuorizacion.Count > 0)
            {
                for (int i = 0; i < ModeloAuorizacion.Count; i++)
                {
                    var ResultadoModelo = oModeloAutorizacionDAO.ObtenerDatosxID(ModeloAuorizacion[i].IdModeloAutorizacion, BaseDatos);
                    for (int a = 0; a < ResultadoModelo[0].DetallesAutor.Count; a++)
                    {
                        if (ResultadoModelo[0].DetallesAutor[a].IdAutor == IdUsuario)
                        {
                            SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
                           
                            try
                            {
                                IdInsert = int.Parse(UpdateInsertPedido(oPedidoDTO));
                            }
                            catch (Exception e) {
                                return "-2";
                            }
                            for (int e = 0; e < ResultadoModelo[0].DetallesEtapa.Count; e++)
                            {
                                var result = OPedidoDAO.UpdateInsertPedidoModelo(new PedidoModeloDTO
                                {
                                    IdPedidoModelo = 0,
                                    IdPedido = IdInsert,
                                    IdModelo = ResultadoModelo[0].IdModeloAutorizacion,
                                    IdEtapa = ResultadoModelo[0].DetallesEtapa[e].IdEtapa,
                                    Aprobaciones = ResultadoModelo[0].DetallesEtapa[e].AutorizacionesRequeridas,
                                    Rechazos = ResultadoModelo[0].DetallesEtapa[e].RechazosRequeridos
                                }, "1", BaseDatos);

                                //var ResultadoEtapa = oEtapaAutorizacionDAO.ObtenerDatosxID(ResultadoModelo[0].DetallesEtapa[e].IdEtapa);
                            }
                            CorregirSolicitudRQSinModelo();
                            return IdInsert.ToString();
                        }
                    }
                }
            }
            CorregirSolicitudRQSinModelo();
            return "-1";

        }



        public string UpdateInsertPedido(PedidoDTO oPedidoDTO)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
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
            int respuesta = oPedidoDAO.UpdateInsertPedido(oPedidoDTO,BaseDatos,ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {
                for (int i = 0; i < oPedidoDTO.detalles.Count; i++)
                {
                    oPedidoDTO.detalles[i].IdPedido = respuesta;
                    int respuesta1 = oPedidoDAO.InsertUpdatePedidoDetalle(oPedidoDTO.detalles[i],BaseDatos,ref mensaje_error);
                }

                oPedidoDAO.UpdateTotalesPedido(respuesta,BaseDatos,ref mensaje_error);

            }

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta > 0)
                {

                    /*INSERTAR CUADRO COMPARATIVO*/
                    RespuestaDTO oRespuestaDTOReporte = new RespuestaDTO();
                    try
                    {
                        PedidoDTO datosObtenidos = oPedidoDAO.ObtenerPedidoxId(respuesta,BaseDatos,ref mensaje_error);


                        string respuestareporte = GenerarReporte("CuadroComparativo", "PDF", respuesta,BaseDatos);
                        oRespuestaDTOReporte = JsonConvert.DeserializeObject<RespuestaDTO>(respuestareporte);
                        Byte[] archivoreporte = Convert.FromBase64String(oRespuestaDTOReporte.Base64ArchivoPDF);
                        string nombrearchivocuadro = "CuadroComparativoPedido" + datosObtenidos.NombSerie + "-" + datosObtenidos.Correlativo + ".pdf";


                        if (System.IO.File.Exists("wwwroot\\Anexos\\" + nombrearchivocuadro))
                            System.IO.File.WriteAllBytes("wwwroot\\Anexos\\" + nombrearchivocuadro, archivoreporte);
                        else
                        {
                            System.IO.File.Delete("wwwroot\\Anexos\\" + nombrearchivocuadro);
                            System.IO.File.WriteAllBytes("wwwroot\\Anexos\\" + nombrearchivocuadro, archivoreporte);
                        }
                        AnexoDTO oPedAnexo = new AnexoDTO();
                        oPedAnexo.ruta = "/Anexos/" + nombrearchivocuadro;
                        oPedAnexo.IdSociedad = oPedidoDTO.IdSociedad;
                        oPedAnexo.Tabla = "Pedido";
                        oPedAnexo.IdTabla = respuesta;
                        oPedAnexo.NombreArchivo = nombrearchivocuadro;

                        oMovimientoDAO.InsertAnexoMovimiento(oPedAnexo,BaseDatos,ref mensaje_error);

                    }
                    catch (Exception ex)
                    {

                        var dd = "";
                    }


                    /*INSERTAR CUADRO COMPARATIVO*/
                    if (oPedidoDTO.AnexoDetalle != null)
                    {
                        for (int i = 0; i < oPedidoDTO.AnexoDetalle.Count; i++)
                        {
                            if (!oPedidoDTO.AnexoDetalle[i].web)
                            {
                                oPedidoDTO.AnexoDetalle[i].ruta = "/Anexos/" + oPedidoDTO.AnexoDetalle[i].NombreArchivo;
                            }

                            oPedidoDTO.AnexoDetalle[i].IdSociedad = oPedidoDTO.IdSociedad;
                            oPedidoDTO.AnexoDetalle[i].Tabla = "Pedido";
                            oPedidoDTO.AnexoDetalle[i].IdTabla = respuesta;

                            oMovimientoDAO.InsertAnexoMovimiento(oPedidoDTO.AnexoDetalle[i],BaseDatos,ref mensaje_error);
                        }
                    }

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<PedidoDTO> lstPedidoDTO = oPedidoDAO.ObtenerPedidoxEstadoConformidad(IdSociedad,BaseDatos,ref mensaje_error, Conformidad);
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

        public string ObtenerPedidoDTConfirmidadXAutorizador(int Accion = 0)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<PedidoDTO> lstPedidoDTO = oPedidoDAO.ObtenerPedidoxEstadoConformidadAutorizacion(IdUsuario, BaseDatos, ref mensaje_error, Accion);
            List<PedidoDTO> lstPedidoDTO2 = oPedidoDAO.ObtenerPedidoxEstadoConformidad(1, BaseDatos, ref mensaje_error, Accion);

            for (int i = 0; i < lstPedidoDTO2.Count; i++)
            {
                lstPedidoDTO.Add(lstPedidoDTO2[i]);
            }

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<PedidoDTO> lstPedidoDTO = oPedidoDAO.ObtenerPedidoDTCorreoProveedor(IdSociedad, EnvioCorreo, Proveedor,BaseDatos,ref mensaje_error);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<PedidoDTO> lstPedidoDTO = oPedidoDAO.ObtenerPedido(IdSociedad,BaseDatos,ref mensaje_error, estado);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<PedidoDetalleDTO> lstPedidoDetalleDTO = oPedidoDAO.ObtenerDetallePedido(IdPedido,BaseDatos,ref mensaje_error);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PedidoDAO oPedidoDAO = new PedidoDAO();
            PedidoDTO oPedidoDTO = new PedidoDTO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            oPedidoDTO = oPedidoDAO.ObtenerPedidoxId(IdPedido,BaseDatos,ref mensaje_error);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PedidoDAO oPedidoDAO = new PedidoDAO();
            List<ItemAprobadosDTO> lstItemAprobadosDTO = new List<ItemAprobadosDTO>();
            DataTableDTO oDataTableDTO = new DataTableDTO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            lstItemAprobadosDTO = oPedidoDAO.ListarItemAprobadosxSociedad(IdSociedad,BaseDatos,ref mensaje_error);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<ArticuloStockDTO> lstArticuloStockDTO = new List<ArticuloStockDTO>();
            PedidoDAO oPedidoDAO = new PedidoDAO();
            lstArticuloStockDTO = oPedidoDAO.ObtenerStockxIdDetalleSolicitudRQ(IdDetalleRQ,BaseDatos,ref mensaje_error);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<ProveedoresPrecioProductoDTO> lstProveedoresPrecioProductoDTO = new List<ProveedoresPrecioProductoDTO>();
            PedidoDAO oPedidoDAO = new PedidoDAO();
            lstProveedoresPrecioProductoDTO = oPedidoDAO.ObtenerPrecioxProductoUltimasVentas(IdArticulo,BaseDatos,ref mensaje_error);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<ProveedoresPrecioProductoDTO> lstProveedoresPrecioProductoDTO = new List<ProveedoresPrecioProductoDTO>();
            PedidoDAO oPedidoDAO = new PedidoDAO();
            lstProveedoresPrecioProductoDTO = oPedidoDAO.ObtenerProveedoresPrecioxProducto(IdArticulo,BaseDatos,ref mensaje_error);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<ProveedoresPrecioProductoDTO> lstProveedoresPrecioProductoDTO = new List<ProveedoresPrecioProductoDTO>();
            PedidoDAO oPedidoDAO = new PedidoDAO();
            lstProveedoresPrecioProductoDTO = oPedidoDAO.ObtenerProveedoresPrecioxProductoConObras(IdObra,IdArticulo,BaseDatos,ref mensaje_error);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PedidoDAO oPedidoDAO = new PedidoDAO();
            oPedidoDAO.UpdateInsertPedidoAsignadoPedidoRQ(IdProveedor, precionacional, precioextranjero, idproducto, IdDetalleRq, Comentario,BaseDatos,ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            return "EXITO";
        }

        public string ListarProductosAsignadosxProveedorxIdUsuarioDT()
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<AsignadoPedidoRequeridoDTO> lstAsignadoPedidoRequeridoDTO = new List<AsignadoPedidoRequeridoDTO>();
            lstAsignadoPedidoRequeridoDTO = oPedidoDAO.ListarProductosAsignadosxProveedorxIdUsuario(IdUsuario,BaseDatos,ref mensaje_error);
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

        public string ObtenerDatosProveedorXRQAsignados(int IdProveedor,int TipoItem,int IdObra,bool EsSubContrato)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<AsignadoPedidoRequeridoDTO> lstAsignadoPedidoRequeridoDTO = new List<AsignadoPedidoRequeridoDTO>();
            PedidoDAO oPedidoDAO = new PedidoDAO();
            lstAsignadoPedidoRequeridoDTO = oPedidoDAO.ListarProductosAsignadosxProveedorDetalle(IdProveedor,TipoItem, IdObra,EsSubContrato, BaseDatos,ref mensaje_error);
            return JsonConvert.SerializeObject(lstAsignadoPedidoRequeridoDTO);
        }

        public string updatedInsertConformidadPedido(ConformidadPedidoDTO oConformidadPedidoDTO,int UsuarioConformidad)
        {
            UsuarioConformidad = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PedidoDAO oPedidoDAO = new PedidoDAO();
            string mensaje_error = "";
            int respuesta = oPedidoDAO.UpdateInsertPedidoConformidadPedido(oConformidadPedidoDTO, UsuarioConformidad,BaseDatos,ref mensaje_error);
            if (respuesta >= 0 && oConformidadPedidoDTO.Conformidad == 2)
            {
                EnviarCorreoObservaciones(oConformidadPedidoDTO.IdPedido);

            }
            return respuesta.ToString();
        }

        public string updatedInsertConformidadPedidoAuth(ConformidadPedidoDTO oConformidadPedidoDTO, int UsuarioConformidad, int IdModelo)
        {
            UsuarioConformidad = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PedidoDAO oPedidoDAO = new PedidoDAO();
            string mensaje_error = "";
            int respuesta = oPedidoDAO.UpdateInsertPedidoConformidadPedidoAuth(oConformidadPedidoDTO, UsuarioConformidad,IdModelo, BaseDatos, ref mensaje_error);
            if (respuesta >= 0 && oConformidadPedidoDTO.Conformidad == 2)
            {
                EnviarCorreoObservaciones(oConformidadPedidoDTO.IdPedido);

            }
            if (respuesta >= 0 && oConformidadPedidoDTO.Conformidad == 3)
            {
                ConfiguracionSociedadController homeController = new ConfiguracionSociedadController();

                CorreoDAO oCorreoDAO = new CorreoDAO();
                CorreoDTO oCorreoDTO = oCorreoDAO.ObtenerDatosCorreo("GENERAL", BaseDatos, ref mensaje_error);
                PedidoDTO oPedidoDTO = new PedidoDTO();
                ObraDAO obraDao = new ObraDAO();
                oPedidoDTO = oPedidoDAO.ObtenerPedidoxId(oConformidadPedidoDTO.IdPedido, BaseDatos, ref mensaje_error);
                List<ObraDTO> datosObra = obraDao.ObtenerDatosxID(oPedidoDTO.IdObra, BaseDatos, ref mensaje_error);
                string correoObra = "";

                List<string> Correos = new List<string>();
                Correos.Add("garrieta@concyssa.com");

                if (datosObra.Count > 0)
                {
                    correoObra = datosObra[0].CorreoObra;
                    Correos.Add(correoObra);
                }

                string HTML = TemplateCierreAnulacion(oPedidoDTO, "Rechazado");
                string Asunto = "SE RECHAZO EL PEDIDO " + oPedidoDTO.NombSerie + "-" + oPedidoDTO.Correlativo;

                homeController.EnviarCorreo2025(oCorreoDTO, Asunto, Correos, HTML, BaseDatos, ref mensaje_error);      

            }
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

            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            //System.Net.ServicePointManager.SecurityProtocol =  SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            PedidoDAO oPedidoDAO = new PedidoDAO();
            PedidoDTO oPedidoDTO = new PedidoDTO();
            ObraDAO obraDao = new ObraDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            string mensaje_error = "";
            oPedidoDTO = oPedidoDAO.ObtenerPedidoxId(IdPedido, BaseDatos, ref mensaje_error);
            List<ObraDTO> datosObra = obraDao.ObtenerDatosxID(oPedidoDTO.IdObra, BaseDatos, ref mensaje_error);
            CorreoDAO oCorreoDAO = new CorreoDAO();
            CorreoDTO oCorreoDTO = oCorreoDAO.ObtenerDatosCorreo("COMPRAS", BaseDatos, ref mensaje_error);
            string errorCode = "OK";
            try
            {

                using (SmtpClient SmtpServer = new SmtpClient(oCorreoDTO.Servidor))
                {
                    using (MailMessage message = new MailMessage())
                    {
                        string Asunto = "CONCYSSA " + oPedidoDTO.NombSerie + "-" + oPedidoDTO.Correlativo;
                        message.From = new MailAddress(oCorreoDTO.Email, Asunto);

                        string correoObra = "";
                        if (datosObra.Count > 0) correoObra = datosObra[0].CorreoObra;

                        int count = oPedidoDTO.EmailProveedor.Split(";").Count();
                        for (int i = 0; i < count; i++)
                        {
                            string email = oPedidoDTO.EmailProveedor.Split(';')[i].Trim();
                            message.To.Add(email);
                        }

                        message.To.Add(correoObra);
                        message.To.Add("garrieta@concyssa.com");
                        //message.To.Add("cristhian.chacaliaza@smartcode.pe");

                        message.Subject = Asunto;
                        message.IsBodyHtml = true;
                        message.Body = TemplateEmail(correoObra);

                        SmtpServer.Port = oCorreoDTO.Puerto;
                        SmtpServer.EnableSsl = oCorreoDTO.SSL;
                        SmtpServer.UseDefaultCredentials = false;
                        SmtpServer.Credentials = new NetworkCredential(oCorreoDTO.Email, oCorreoDTO.Clave);

                        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                        WebResponse webResponse;
                        HttpWebRequest request;
                        Uri uri;
                        string cadenaUri;
                        string response;

                        IConfiguration _IConfiguration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .Build();


                        string strNew = "NombreReporte=OrdenCompra&Formato=PDF&Id=" + IdPedido + "&BaseDatos=" + BaseDatos;
                        cadenaUri = _IConfiguration["Reporte:UrlBase"] + "/ReportCrystal.asmx/ObtenerReportCrystal";
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
                        string base64 = xDoc.ChildNodes[1].ChildNodes[2].InnerText;
                        Byte[] archivopdf = Convert.FromBase64String(base64);
                        Attachment att = new Attachment(new MemoryStream(archivopdf), "OrdenCompra.pdf");
                        message.Attachments.Add(att);

                        bool exception = false;
                        try
                        {
                            SmtpServer.Send(message);
                            oPedidoDAO.UpdateEnvioCorreoPedido(IdPedido, BaseDatos, ref mensaje_error);
                        }
                        catch (SmtpFailedRecipientsException ex)
                        {
                            exception = true;
                            for (int i = 0; i < ex.InnerExceptions.Length; i++)
                            {
                                SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                                errorCode = status.ToString();
                                if (status == SmtpStatusCode.MailboxBusy)
                                {
                                    mensaje_error = "Receptor ocupado";
                                }
                                if (status == SmtpStatusCode.MailboxUnavailable)
                                {
                                    mensaje_error = "Dirección de correo no disponible";
                                }
                                else
                                {
                                    mensaje_error = "Falló el envío" ;
                                }
                            }
                        }
                        catch (SmtpException ex)
                        {
                            errorCode = ex.StatusCode.ToString();
                            exception = true;
                            mensaje_error =  "Error " + ex.Message + "(" + errorCode + ")";
                        }


                        catch (Exception ex)
                        {
                            errorCode = "**";
                            exception = true;
                            mensaje_error = "Límite de reitentos: {0}" + ex.ToString() + errorCode;
                        }

                       
                    }
                }
              
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


        public string GenerarReporte(string NombreReporte, string Formato, int Id,string BaseDatos)
        {
            if(BaseDatos == "" || BaseDatos==null)
            {
                BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            }

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
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&Id=" + Id +"&BaseDatos=" + BaseDatos;
               //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReportCrystal";
                cadenaUri = _IConfiguration["Reporte:UrlBase"] + "/ReportCrystal.asmx/ObtenerReportCrystal";
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



        public string TemplateEmail(string correoObra)
        {
            return @"
<html><head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
	<title></title>
	
</head>
<body>
<p>Estimado proveedor</p>

<p>Se adjuntan órdenes de compra/servicio, por favor coordinar las entregas con Almacén y tener presente los siguientes criterios de compra al momento del despacho:</p>

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
	<li>Enviar su factura electrónica al correo: <b>"+ correoObra + @"</b></li>
</ul>

</body></html>";
        }


        public string CerrarPedido(PedidoDTO oPedidoDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int respuesta = oPedidoDAO.CerrarPedido(oPedidoDTO, IdUsuario,BaseDatos, ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta == 1)
                {
                    EnviarCorreoAnulacionCierre(oPedidoDTO.IdPedido, "cerrado");
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int respuesta = oPedidoDAO.LiberarPedido(oPedidoDTO, IdUsuario,BaseDatos, ref mensaje_error);

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int respuesta = oPedidoDAO.AnularPedido(oPedidoDTO, IdUsuario, BaseDatos,ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta == 1)
                {
                    EnviarCorreoAnulacionCierre(oPedidoDTO.IdPedido, "anulado");
                    return "1";
                }
                else if (respuesta == 2) {
                    return "2";
                }
                else
                {
                    return "error";
                }
            }

        }

        public int UpdatePrecioProveedorArticulo(ProveedoresPrecioProductoDTO oProveedoresPrecioProductoDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int Resultado = oPedidoDAO.UpdatePrecioProveedorArticulo(oProveedoresPrecioProductoDTO,BaseDatos,ref mensaje_error);
            return Resultado;
        }

        public string ObtenerOcAbiertasxArtAlmacen(int IdArticulo, int IdAlmacen)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PedidoDAO oPedidoDAO = new PedidoDAO();
            List<PedidoDTO> lstPedidoDTO = oPedidoDAO.ObtenerOcAbiertasxArtAlmacen(IdArticulo, IdAlmacen, BaseDatos, ref mensaje_error);
            if (lstPedidoDTO.Count > 0)
            {
             
                return JsonConvert.SerializeObject(lstPedidoDTO);

            }
            else
            {
                
                return "SIN DATOS";

            }
            return "ERROR";
        }

        public int EnviarCorreoObservaciones(int IdPedido)
        {

            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            try
            {

                string base64;
                string html = @"";
                string mensaje_error = "";
                PedidoDAO oPedidoDAO = new PedidoDAO();
                PedidoDTO oPedidoDTO = new PedidoDTO();
                int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));


                oPedidoDTO = oPedidoDAO.ObtenerPedidoxId(IdPedido, BaseDatos, ref mensaje_error);
                string body;
                body = "BASE PRUBAS";

                ObraDAO obraDao = new ObraDAO();
                List<ObraDTO> datosObra = obraDao.ObtenerDatosxID(oPedidoDTO.IdObra, BaseDatos, ref mensaje_error);
                string correoObra = "";
                if (datosObra.Count > 0) correoObra = datosObra[0].CorreoObra;
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
                string displayName = "SE OBSERVÓ EL PEDIDO " + oPedidoDTO.NombSerie + "-" + oPedidoDTO.Correlativo;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, displayName);

                //mail.To.Add(correoObra);
                //mail.To.Add("garrieta@concyssa.com");
                mail.To.Add(correoObra);
                mail.To.Add("compras@concyssa.com");
   
                //mail.To.Add("cristhian.chacaliaza@smartcode.pe");


                mail.Subject = "SE OBSERVÓ EL PEDIDO " + oPedidoDTO.NombSerie + "-" + oPedidoDTO.Correlativo;
                //mail.CC.Add(new MailAddress("camala145@gmail.com"));
                mail.Body = TemplateObservacion(oPedidoDTO);

                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Aquí debes sustituir tu servidor SMTP y el puerto
                client.Credentials = new NetworkCredential(from, password);
                client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false



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

        public int EnviarCorreoAnulacionCierre(int IdPedido,string CierreAnulacion)
        {

            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            try
            {

                string base64;
                string html = @"";
                string mensaje_error = "";
                PedidoDAO oPedidoDAO = new PedidoDAO();
                PedidoDTO oPedidoDTO = new PedidoDTO();
                int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));


                oPedidoDTO = oPedidoDAO.ObtenerPedidoxId(IdPedido, BaseDatos, ref mensaje_error);
                string body;
                body = "BASE PRUBAS";


                if (oPedidoDTO.EnvioCorreo == false)
                {
                    return 0;
                }

                ObraDAO obraDao = new ObraDAO();
                List<ObraDTO> datosObra = obraDao.ObtenerDatosxID(oPedidoDTO.IdObra, BaseDatos, ref mensaje_error);
                string correoObra = "";
                if (datosObra.Count > 0) correoObra = datosObra[0].CorreoObra;
                //body = "<body>" +
                //    "<h2>Se "+Estado+" una Solicitud</h2>" +
                //    "<h4>Detalles de Solicitud:</h4>" +
                //    "<span>N° Solicitud: " + Serie + "-" + Numero + "</span>" +
                //    "<br/><span>Solicitante: " + Solicitante + "</span>" +
                //    "</body>";

                string displayAsunto = "CERRÓ";
                if (CierreAnulacion == "anulado") { displayAsunto = "ANULÓ"; }

                string msge = "";
                string from = "concyssa.smc@gmail.com";
                string correo = from;
                string password = "tlbvngkvjcetzunr";
                string displayName = "SE "+ displayAsunto + " EL PEDIDO " + oPedidoDTO.NombSerie + "-" + oPedidoDTO.Correlativo;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, displayName);

                mail.To.Add(correoObra);
                mail.To.Add("compras@concyssa.com");
                mail.To.Add(oPedidoDTO.EmailProveedor);


                //mail.To.Add("cristhian.chacaliaza@smartcode.pe");


                mail.Subject = "SE "+ displayAsunto + " EL PEDIDO " + oPedidoDTO.NombSerie + "-" + oPedidoDTO.Correlativo;
                //mail.CC.Add(new MailAddress("camala145@gmail.com"));
                mail.Body = TemplateCierreAnulacion(oPedidoDTO, CierreAnulacion);

                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Aquí debes sustituir tu servidor SMTP y el puerto
                client.Credentials = new NetworkCredential(from, password);
                client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false



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



        public string TemplateObservacion(PedidoDTO oPedidoDTO)
        {
            string correo = @"<html><head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
	                <title></title>
	
                    </head>
                    <body>
                    <p>Estimado personal</p>

                    <p>Se notifica que el Pedido " + oPedidoDTO.NombSerie + "-" + oPedidoDTO.Correlativo + @" fué observado:</p>
                
                    </body></html>";

            return correo;


        }
        public string TemplateCierreAnulacion(PedidoDTO oPedidoDTO,string AnulacionCierre)
        {
            string correo = @"<html><head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
	                <title></title>
	
                    </head>
                    <body>
                    <p>Estimado Proveedor</p>

                    <p>Se notifica que el Pedido " + oPedidoDTO.NombSerie + "-" + oPedidoDTO.Correlativo + @" fué " + AnulacionCierre+@"</p>
                
                    </body></html>";

            return correo;


        }

        public void CorregirSolicitudRQSinModelo()
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            List<PedidoDTO> lstPedidoDTO = oPedidoDAO.ObtenerPedidoSinModelo(BaseDatos);
            for (int i = 0; i < lstPedidoDTO.Count; i++)
            {
                oPedidoDAO.CorregirPedidoSinModelo(lstPedidoDTO[i].IdPedido, lstPedidoDTO[i].IdUsuario, BaseDatos);
            }
        }

        public string ObtenerOcPendientes(int idObra, int idArticulo)
        {
            PedidoDAO oPedidoDAO = new PedidoDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<PedidoPendienteDTO> lstPedidoDTO = oPedidoDAO.ObtenerOCPendientes(IdSociedad, idObra, idArticulo, BaseDatos);
            if (lstPedidoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstPedidoDTO);
            }
            else
            {
                return "error";
            }
        }

        public int EliminarItemRQ(int IdDetalle)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            SolicitudDetalleDTO oSolicitudDetalleDTO = oSolicitudRQDAO.ObtenerSolicitudDetallexId(IdDetalle,BaseDatos,ref mensaje_error);
            int resultado = oPedidoDAO.Delete(IdDetalle, BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;

                EnviarCorreItemRQEliminado(oSolicitudDetalleDTO);

            }

            return resultado;
        }
        public string ActivarPedido(PedidoDTO oPedidoDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int respuesta = oPedidoDAO.ActivarPedido(oPedidoDTO, BaseDatos, ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta == 1)
                {
                    EnviarCorreoAnulacionCierre(oPedidoDTO.IdPedido, "cerrado");
                    return "1";
                }
                else
                {
                    return "error";
                }
            }

        }


        public int EnviarCorreItemRQEliminado(SolicitudDetalleDTO datos)
        {

            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            try
            {

                string base64;
                string html = @"";
                string mensaje_error = "";
                PedidoDAO oPedidoDAO = new PedidoDAO();
                PedidoDTO oPedidoDTO = new PedidoDTO();

                string body;
                body = "BASE PRUBAS";

           

                string msge = "";
                string from = "concyssa.smc@gmail.com";
                string correo = from;
                string password = "tlbvngkvjcetzunr";
                string displayName = "[SGC] "+datos.NumeroSerie+" ITEM DE REQUERIMIENTO RECHAZADO POR LOGISTICA";
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, displayName);

         
                mail.To.Add(datos.CorreoAlmacen);



                //mail.To.Add("cristhian.chacaliaza@smartcode.pe");


                mail.Subject = "[SGC] "+datos.NumeroSerie+" ITEM DE REQUERIMIENTO RECHAZADO POR LOGISTICA";
                //mail.CC.Add(new MailAddress("camala145@gmail.com"));
                mail.Body = @"<html><head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
	                            <title></title>
	
                            </head>
                            <br>

                            <p>Le informamos que un Item del requerimiento "+datos.NumeroSerie +@" fue rechazado por el área de logisitca, a continuación los detalles: </p>

                            <p><b>Articulo/Servicio: </b> "+datos.CodArticulo +"-"+ datos.DescripcionItem + @" </p></br>
                            <p><b>Cantidad: </b> "+ Math.Round(datos.CantidadNecesaria,2) +@" </p></br>
                         
                            <p>Si tiene inquietud respecto debe comunicarse con el área de logistica</p>

                            <label>---------------------------------------------------------------------------------------</label><br>
                            <label>Este es un sistema automático de notificaciones, por favor no responda este mensaje al correo.</label><br>
                            <label>Desarrollado por <a href=""https://smartcode.pe/"">SmartCode</a></label><br>
                            <label>---------------------------------------------------------------------------------------</label><br>

                            </body></html>";

                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Aquí debes sustituir tu servidor SMTP y el puerto
                client.Credentials = new NetworkCredential(from, password);
                client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false



                WebResponse webResponse;
                HttpWebRequest request;
                Uri uri;
                string cadenaUri;
                string response;
       
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

    }
}