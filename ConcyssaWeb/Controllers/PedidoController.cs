using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace ConcyssaWeb.Controllers
{
    public class PedidoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerPedidosEntregaLDT(string EstadoPedido = "ABIERTO")
        {
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<PedidoDTO> lstPedidoDTO = oPedidoDAO.ObtenerPedidosEntregaLDT(IdSociedad, ref mensaje_error, EstadoPedido);
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
            int respuesta = oPedidoDAO.UpdateInsertPedido(oPedidoDTO, ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {
                for (int i = 0; i < oPedidoDTO.detalles.Count; i++)
                {
                    oPedidoDTO.detalles[i].IdPedido = respuesta;
                    int respuesta1 = oPedidoDAO.InsertUpdatePedidoDetalle(oPedidoDTO.detalles[i], ref mensaje_error);
                }
                if (oPedidoDTO.AnexoDetalle!=null)
                {
                    for (int i = 0; i < oPedidoDTO.AnexoDetalle.Count; i++)
                    {
                        oPedidoDTO.AnexoDetalle[i].ruta = "/Anexos/" + oPedidoDTO.AnexoDetalle[i].NombreArchivo;
                        oPedidoDTO.AnexoDetalle[i].IdSociedad = oPedidoDTO.IdSociedad;
                        oPedidoDTO.AnexoDetalle[i].Tabla = "Pedido";
                        oPedidoDTO.AnexoDetalle[i].IdTabla = respuesta;

                        oMovimientoDAO.InsertAnexoMovimiento(oPedidoDTO.AnexoDetalle[i], ref mensaje_error);
                    }
                }
               
                

                oPedidoDAO.UpdateTotalesPedido(respuesta, ref mensaje_error);

            }

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta > 0)
                {
                    return respuesta.ToString();
                }
                else
                {
                    return mensaje_error;
                }
            }
        }

        public string ObtenerPedidoDT(int estado = 3)
        {
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<PedidoDTO> lstPedidoDTO = oPedidoDAO.ObtenerPedido(IdSociedad, ref mensaje_error, estado);
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
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<PedidoDetalleDTO> lstPedidoDetalleDTO = oPedidoDAO.ObtenerDetallePedido(IdPedido, ref mensaje_error);
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
            PedidoDAO oPedidoDAO = new PedidoDAO();
            PedidoDTO oPedidoDTO = new PedidoDTO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            oPedidoDTO = oPedidoDAO.ObtenerPedidoxId(IdPedido, ref mensaje_error);
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
            PedidoDAO oPedidoDAO = new PedidoDAO();
            List<ItemAprobadosDTO> lstItemAprobadosDTO = new List<ItemAprobadosDTO>();
            DataTableDTO oDataTableDTO = new DataTableDTO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            lstItemAprobadosDTO = oPedidoDAO.ListarItemAprobadosxSociedad(IdSociedad, ref mensaje_error);
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
            List<ArticuloStockDTO> lstArticuloStockDTO = new List<ArticuloStockDTO>();
            PedidoDAO oPedidoDAO = new PedidoDAO();
            lstArticuloStockDTO = oPedidoDAO.ObtenerStockxIdDetalleSolicitudRQ(IdDetalleRQ, ref mensaje_error);
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

        public string ObtenerProveedoresPrecioxProducto(int IdArticulo)
        {
            string mensaje_error = "";
            List<ProveedoresPrecioProductoDTO> lstProveedoresPrecioProductoDTO = new List<ProveedoresPrecioProductoDTO>();
            PedidoDAO oPedidoDAO = new PedidoDAO();
            lstProveedoresPrecioProductoDTO = oPedidoDAO.ObtenerProveedoresPrecioxProducto(IdArticulo, ref mensaje_error);
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

        public string ActualizarProveedorPrecio(int IdProveedor, decimal precionacional, decimal precioextranjero, int idproducto, int IdDetalleRq)
        {
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            oPedidoDAO.UpdateInsertPedidoAsignadoPedidoRQ(IdProveedor, precionacional, precioextranjero, idproducto, IdDetalleRq, ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            return "EXITO";
        }

        public string ListarProductosAsignadosxProveedorxIdUsuarioDT()
        {
            string mensaje_error = "";
            PedidoDAO oPedidoDAO = new PedidoDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<AsignadoPedidoRequeridoDTO> lstAsignadoPedidoRequeridoDTO = new List<AsignadoPedidoRequeridoDTO>();
            lstAsignadoPedidoRequeridoDTO = oPedidoDAO.ListarProductosAsignadosxProveedorxIdUsuario(IdUsuario, ref mensaje_error);
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

        public string ObtenerDatosProveedorXRQAsignados(int IdProveedor)
        {
            string mensaje_error = "";
            List<AsignadoPedidoRequeridoDTO> lstAsignadoPedidoRequeridoDTO = new List<AsignadoPedidoRequeridoDTO>();
            PedidoDAO oPedidoDAO = new PedidoDAO();
            lstAsignadoPedidoRequeridoDTO = oPedidoDAO.ListarProductosAsignadosxProveedorDetalle(IdProveedor, ref mensaje_error);
            return JsonConvert.SerializeObject(lstAsignadoPedidoRequeridoDTO);
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


    }
}
