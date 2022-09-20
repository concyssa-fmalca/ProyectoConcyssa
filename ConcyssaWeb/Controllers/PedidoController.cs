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

        public string ObtenerPedidosEntregaLDT(string EstadoPedido="ABIERTO")
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

            if (IdSociedad==0)
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
                    return "1";
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


    }
}
