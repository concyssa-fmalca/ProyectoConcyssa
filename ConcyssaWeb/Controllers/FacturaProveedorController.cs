using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class FacturaProveedorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listado()
        {
            return View();
        }

        public string ListarOPCHDT(string EstadoOPCH = "ABIERTO")
        {
            string mensaje_error = "";
            OpchDAO oOpchDAO = new OpchDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OpchDTO> lstOpchDTO = oOpchDAO.ObtenerOPCHxEstado(IdSociedad, ref mensaje_error, EstadoOPCH);
            if (lstOpchDTO.Count >= 0 && mensaje_error.Length==0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstOpchDTO.Count;
                oDataTableDTO.iTotalRecords = lstOpchDTO.Count;
                oDataTableDTO.aaData = (lstOpchDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            else
            {
                return mensaje_error;

            }
        }


        public string ObtenerOPCHDetalle(int IdOPCH)
        {
            string mensaje_error = "";
            OpchDAO oOpchDAO = new OpchDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OPCHDetalle> lstOPCHDetalle = oOpchDAO.ObtenerDetalleOpch(IdOPCH, ref mensaje_error);
            if (lstOPCHDetalle.Count > 0)
            {
                //oDataTableDTO.sEcho = 1;
                //oDataTableDTO.iTotalDisplayRecords = lstOPCHDetalle.Count;
                //oDataTableDTO.iTotalRecords = lstOPCHDetalle.Count;
                //oDataTableDTO.aaData = (lstOPCHDetalle);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(lstOPCHDetalle);

            }
            else
            {
                return mensaje_error;

            }
        }

        
        public string UpdateInsertMovimientoFacturaProveedor(OpchDTO oOpchDTO)
        {
            string mensaje_error = "";
            int IdSociedad = Convert.ToInt32((String.IsNullOrEmpty(oOpchDTO.IdSociedad.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oOpchDTO.IdSociedad);
            int IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(oOpchDTO.IdUsuario.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")) : oOpchDTO.IdUsuario);
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

            oOpchDTO.IdSociedad = IdSociedad;
            oOpchDTO.IdUsuario = IdUsuario;
            MovimientoDAO oMovimimientoDAO = new MovimientoDAO();
            OpchDAO oOpchDAO = new OpchDAO();
            int respuesta = oMovimimientoDAO.InsertUpdateMovimientoOPCH(oOpchDTO, ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {
                for (int i = 0; i < oOpchDTO.detalles.Count; i++)
                {
                    oOpchDTO.detalles[i].IdOPCH = respuesta;
                    int respuesta1 = oMovimimientoDAO.InsertUpdateOPCHDetalle(oOpchDTO.detalles[i], ref mensaje_error);
                }
                oOpchDAO.UpdateTotalesOPCH(respuesta, ref mensaje_error);


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

        public string ObtenerDatosxIdOpch(int IdOpch)
        {
            string mensaje_error = "";
            OpchDAO oOpchDAO = new OpchDAO();
            OpchDTO oOpchDTO = oOpchDAO.ObtenerDatosxIdOpch(IdOpch, ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {
                List<OPCHDetalle> lstOPCHDetalle = new List<OPCHDetalle>();
                lstOPCHDetalle = oOpchDAO.ObtenerDetalleOpch(IdOpch, ref mensaje_error);
                oOpchDTO.detalles = new OPCHDetalle[lstOPCHDetalle.Count()];
                for (int i = 0; i < lstOPCHDetalle.Count; i++)
                {
                    oOpchDTO.detalles[i] = lstOPCHDetalle[i];
                }
                return JsonConvert.SerializeObject(oOpchDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

    }
}
