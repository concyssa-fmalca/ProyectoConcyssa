using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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



    }
}
