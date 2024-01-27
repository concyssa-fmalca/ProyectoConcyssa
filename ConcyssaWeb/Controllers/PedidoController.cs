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
            List<PedidoDTO> lstPedidoDTO = oPedidoDAO.ObtenerPedidosEntregaLDT(IdSociedad,BaseDatos,ref mensaje_error, EstadoPedido, IdObra, IdUsuario);
            List<PedidoDTO> NewlstPedidoDTO = new List<PedidoDTO>();


            if (IdProveedor > 0)
            {
                for (int i = 0; i < lstPedidoDTO.Count; i++)
                {
                    if (lstPedidoDTO[i].IdProveedor == IdProveedor)
                    {
                        NewlstPedidoDTO.Add(lstPedidoDTO[i]);
                    }
                }
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = NewlstPedidoDTO.Count;
                oDataTableDTO.iTotalRecords = NewlstPedidoDTO.Count;
                oDataTableDTO.aaData = (NewlstPedidoDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);
            }

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
                            oPedidoDTO.AnexoDetalle[i].ruta = "/Anexos/" + oPedidoDTO.AnexoDetalle[i].NombreArchivo;
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

        public string ObtenerDatosProveedorXRQAsignados(int IdProveedor,int TipoItem,int IdObra)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<AsignadoPedidoRequeridoDTO> lstAsignadoPedidoRequeridoDTO = new List<AsignadoPedidoRequeridoDTO>();
            PedidoDAO oPedidoDAO = new PedidoDAO();
            lstAsignadoPedidoRequeridoDTO = oPedidoDAO.ListarProductosAsignadosxProveedorDetalle(IdProveedor,TipoItem, IdObra,BaseDatos,ref mensaje_error);
            return JsonConvert.SerializeObject(lstAsignadoPedidoRequeridoDTO);
        }

        public string updatedInsertConformidadPedido(ConformidadPedidoDTO oConformidadPedidoDTO,int UsuarioConformidad)
        {
            UsuarioConformidad = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PedidoDAO oPedidoDAO = new PedidoDAO();
            string mensaje_error = "";
            int respuesta = oPedidoDAO.UpdateInsertPedidoConformidadPedido(oConformidadPedidoDTO, UsuarioConformidad,BaseDatos,ref mensaje_error);
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
            try
            {

                string base64;
                string html = @"";
                string mensaje_error = "";
                PedidoDAO oPedidoDAO = new PedidoDAO();
                PedidoDTO oPedidoDTO = new PedidoDTO();
                int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));


                oPedidoDTO = oPedidoDAO.ObtenerPedidoxId(IdPedido,BaseDatos,ref mensaje_error);
                string body;
                body = "BASE PRUBAS";

                ObraDAO obraDao = new ObraDAO();
                List<ObraDTO> datosObra = obraDao.ObtenerDatosxID(oPedidoDTO.IdObra,BaseDatos,ref mensaje_error);
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
                string displayName = "CONCYSSA "+ oPedidoDTO.NombSerie + "-"+oPedidoDTO.Correlativo;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, displayName);

                
                mail.To.Add(oPedidoDTO.EmailProveedor);
                mail.To.Add(correoObra);
                mail.To.Add("compras@concyssa.com");
         

               //mail.To.Add("cristhian.chacaliaza@smartcode.pe");


                mail.Subject = "CONCYSSA " + oPedidoDTO.NombSerie + "-" + oPedidoDTO.Correlativo;
                //mail.CC.Add(new MailAddress("camala145@gmail.com"));
                mail.Body = TemplateEmail(correoObra);

                mail.IsBodyHtml = true; 
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Aquí debes sustituir tu servidor SMTP y el puerto
                client.Credentials = new NetworkCredential(from, password);
                client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false



                WebResponse webResponse;
                HttpWebRequest request;
                Uri uri;
                string cadenaUri;
                string response;
              
                string strNew = "NombreReporte=OrdenCompra&Formato=PDF&Id=" + IdPedido + "&BaseDatos=" + BaseDatos;
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


                oPedidoDAO.UpdateEnvioCorreoPedido(IdPedido,BaseDatos,ref mensaje_error);
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
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int respuesta = oPedidoDAO.CerrarPedido(oPedidoDTO,BaseDatos,ref mensaje_error);

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int respuesta = oPedidoDAO.LiberarPedido(oPedidoDTO,BaseDatos,ref mensaje_error);

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
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int respuesta = oPedidoDAO.AnularPedido(oPedidoDTO,BaseDatos,ref mensaje_error);

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


    }
}
