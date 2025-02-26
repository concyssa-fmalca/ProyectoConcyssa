using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class VehiculoController : Controller
    {
        // GET: VehiculoController
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerVehiculo(int estado = 3)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            VehiculoDAO oVehiculoDAO = new VehiculoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<VehiculoDTO> lstVehiculoDTO = oVehiculoDAO.ObtenerVehiculo(IdSociedad,BaseDatos,IdUsuario,ref mensaje_error, estado);
            if (lstVehiculoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstVehiculoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerVehiculoxIdUsuario()
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            VehiculoDAO oVehiculoDAO = new VehiculoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<VehiculoDTO> lstVehiculoDTO = oVehiculoDAO.ObtenerVehiculoxIdUsuario(IdSociedad, BaseDatos, IdUsuario, ref mensaje_error);
            if (lstVehiculoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstVehiculoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdVehiculo)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            VehiculoDAO oVehiculoDAO = new VehiculoDAO();
            List<VehiculoDTO> lstCodigoUbsoDTO = oVehiculoDAO.ObtenerDatosxID(IdVehiculo,BaseDatos,ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosConductorxPlaca(string Placa)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            VehiculoDAO oVehiculoDAO = new VehiculoDAO();
            List<VehiculoDTO> lstCodigoUbsoDTO = oVehiculoDAO.ObtenerDatosConductorxPlaca(Placa,BaseDatos);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string UpdateInsertVehiculo(VehiculoDTO oVehiculoDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            VehiculoDAO oVehiculoDAO = new VehiculoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oVehiculoDTO.IdSociedad = IdSociedad;
            int respuesta = oVehiculoDAO.UpdateInsertVehiculo(oVehiculoDTO,BaseDatos,ref mensaje_error, Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")));

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

        public int EliminarVehiculo(int IdVehiculo)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            VehiculoDAO oVehiculoDAO = new VehiculoDAO();
            int resultado = oVehiculoDAO.Delete(IdVehiculo,BaseDatos,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public string ObtenerVehiculosxIdCuadrilla(int IdCuadrilla)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            VehiculoDAO oVehiculoDAO = new VehiculoDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<VehiculoDTO> lstVehiculoDTO = oVehiculoDAO.ObtenerVehiculosxIdCuadrilla(IdCuadrilla, BaseDatos, IdUsuario, ref mensaje_error);

            object json = null;

            if (mensaje_error.Length > 0)
            {
                json = new { status = false, mensaje = mensaje_error, cantidad = 0,datos = lstVehiculoDTO };
                return JsonConvert.SerializeObject(json);
            }
            else { 
                json = new { status = true, mensaje = mensaje_error, cantidad = lstVehiculoDTO.Count, datos = lstVehiculoDTO };
                return JsonConvert.SerializeObject(json);
            }

        }

        public string ObtenerVehiculoTransferencia(int IdObraOrigen, int IdObraDestino)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            VehiculoDAO oVehiculoDAO = new VehiculoDAO();
            List<VehiculoDTO> lstVehiculoDTO = oVehiculoDAO.ObtenerVehiculoTransferencia(IdObraOrigen, IdObraDestino, BaseDatos, ref mensaje_error);
            if (lstVehiculoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstVehiculoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

    }
}
