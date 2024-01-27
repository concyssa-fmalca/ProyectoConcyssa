using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class DevolucionAdmController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        [HttpPost]
        [Route("DevolucionAdm/AgregarDevolucionAdm")]
        public dynamic AgregarDevolucionAdm([FromBody] DevolucionAdmDTO oDevolucionAdmDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            if (BaseDatos == "")
            {
                BaseDatos = "AddonsConcyssa";
            }
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            var respuesta = oDevolucionAdmDAO.UpdateInsertDevolucionAdm(oDevolucionAdmDTO,BaseDatos);
            
            if (respuesta > 0)
            {
                return respuesta;
            }
            else
            {
                return 0;

            }
        }

        public int AgregarDevolucionAdmDetalle(DevolucionAdmDetalle oDevolucionAdmDetalle)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            var respuesta = oDevolucionAdmDAO.UpdateInsertDevolucionAdmDetalle(oDevolucionAdmDetalle,BaseDatos);

            if (respuesta > 0)
            {
                return respuesta;
            }
            else
            {
                return 0;

            }
        }

        public string ObtenerDevoluciones(int IdUsuario, int EstadoDevolucion)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            if (BaseDatos == "")
            {
                BaseDatos = "AddonsConcyssa";
            }
            DataTableDTO oDataTableDTO = new DataTableDTO();
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            List<DevolucionAdmDTO> lstSolicitudDespachoDTO = oDevolucionAdmDAO.ObtenerDevoluciones(IdUsuario, EstadoDevolucion,BaseDatos);
            
            oDataTableDTO.sEcho = 1;
            oDataTableDTO.iTotalDisplayRecords = lstSolicitudDespachoDTO.Count;
            oDataTableDTO.iTotalRecords = lstSolicitudDespachoDTO.Count;
            oDataTableDTO.aaData = (lstSolicitudDespachoDTO);
            return JsonConvert.SerializeObject(oDataTableDTO);

        }

        public dynamic ObtenerSolicitudDespachoxId(int IdDevolucion)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            if (BaseDatos == "")
            {
                BaseDatos = "AddonsConcyssa";
            }
            DataTableDTO oDataTableDTO = new DataTableDTO();
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            List<DevolucionAdmDTO> lstSolicitudDespachoDTO = oDevolucionAdmDAO.ObtenerDevolucionxId(IdDevolucion,BaseDatos);

            oDataTableDTO.sEcho = 1;
            oDataTableDTO.iTotalDisplayRecords = lstSolicitudDespachoDTO.Count;
            oDataTableDTO.iTotalRecords = lstSolicitudDespachoDTO.Count;
            oDataTableDTO.aaData = (lstSolicitudDespachoDTO);
            return JsonConvert.SerializeObject(oDataTableDTO);

        }

        public int UpdateDevolucionDetalle(int IdDevolucionDetalle, decimal Cantidad)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            if (BaseDatos == "")
            {
                BaseDatos = "AddonsConcyssa";
            }
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            var respuesta = oDevolucionAdmDAO.UpdateDevolucionAdmDet(IdDevolucionDetalle, Cantidad,BaseDatos);

            if (respuesta > 0)
            {
                return respuesta;
            }
            else
            {
                return 0;

            }

        }

        public int CerrarDevolucionAdm(int IdDevolucion)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            if (BaseDatos == "")
            {
                BaseDatos = "AddonsConcyssa";
            }
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            var respuesta = oDevolucionAdmDAO.CerrarDevolucionAdm(IdDevolucion,BaseDatos);

            if (respuesta > 0)
            {
                return respuesta;
            }
            else
            {
                return 0;

            }

        }
        public string ObtenerDevolucionAdmAtender(int IdBase, DateTime FechaInicio, DateTime FechaFin, int EstadoDevolucion, int SerieFiltro)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            List<DevolucionAdmDTO> lstDevolucionAdmDTO = oDevolucionAdmDAO.ObtenerDevolucionesAtender(IdBase, FechaInicio, FechaFin, EstadoDevolucion, SerieFiltro,BaseDatos);

            if (lstDevolucionAdmDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstDevolucionAdmDTO);
            }
            else
            {
                return "error";
            }

        }

        public int AtencionConfirmada(int Cantidad, int IdDevolucion, int IdArticulo, int EstadoDevolucion)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            int respuesta = oDevolucionAdmDAO.AtencionConfirmada(Cantidad, IdDevolucion, IdArticulo, EstadoDevolucion,BaseDatos,ref mensaje_error);

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
    }
}
