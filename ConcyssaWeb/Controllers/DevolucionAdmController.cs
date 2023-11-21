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
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            var respuesta = oDevolucionAdmDAO.UpdateInsertDevolucionAdm(oDevolucionAdmDTO);
            
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
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            var respuesta = oDevolucionAdmDAO.UpdateInsertDevolucionAdmDetalle(oDevolucionAdmDetalle);

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
            DataTableDTO oDataTableDTO = new DataTableDTO();
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            List<DevolucionAdmDTO> lstSolicitudDespachoDTO = oDevolucionAdmDAO.ObtenerDevoluciones(IdUsuario, EstadoDevolucion);
            
            oDataTableDTO.sEcho = 1;
            oDataTableDTO.iTotalDisplayRecords = lstSolicitudDespachoDTO.Count;
            oDataTableDTO.iTotalRecords = lstSolicitudDespachoDTO.Count;
            oDataTableDTO.aaData = (lstSolicitudDespachoDTO);
            return JsonConvert.SerializeObject(oDataTableDTO);

        }

        public dynamic ObtenerSolicitudDespachoxId(int IdDevolucion)
        {
            DataTableDTO oDataTableDTO = new DataTableDTO();
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            List<DevolucionAdmDTO> lstSolicitudDespachoDTO = oDevolucionAdmDAO.ObtenerDevolucionxId(IdDevolucion);

            oDataTableDTO.sEcho = 1;
            oDataTableDTO.iTotalDisplayRecords = lstSolicitudDespachoDTO.Count;
            oDataTableDTO.iTotalRecords = lstSolicitudDespachoDTO.Count;
            oDataTableDTO.aaData = (lstSolicitudDespachoDTO);
            return JsonConvert.SerializeObject(oDataTableDTO);

        }

        public int UpdateDevolucionDetalle(int IdDevolucionDetalle, decimal Cantidad)
        {

            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            var respuesta = oDevolucionAdmDAO.UpdateDevolucionAdmDet(IdDevolucionDetalle, Cantidad);

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

            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            var respuesta = oDevolucionAdmDAO.CerrarDevolucionAdm(IdDevolucion);

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
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            List<DevolucionAdmDTO> lstDevolucionAdmDTO = oDevolucionAdmDAO.ObtenerDevolucionesAtender(IdBase, FechaInicio, FechaFin, EstadoDevolucion, SerieFiltro);

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
            string mensaje_error = "";
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            int respuesta = oDevolucionAdmDAO.AtencionConfirmada(Cantidad, IdDevolucion, IdArticulo, EstadoDevolucion, ref mensaje_error);

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
