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
            string mensaje_error = "";
            VehiculoDAO oVehiculoDAO = new VehiculoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<VehiculoDTO> lstVehiculoDTO = oVehiculoDAO.ObtenerVehiculo(IdSociedad, ref mensaje_error, estado);
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
            string mensaje_error = "";
            VehiculoDAO oVehiculoDAO = new VehiculoDAO();
            List<VehiculoDTO> lstCodigoUbsoDTO = oVehiculoDAO.ObtenerDatosxID(IdVehiculo, ref mensaje_error);

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

            string mensaje_error = "";
            VehiculoDAO oVehiculoDAO = new VehiculoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oVehiculoDTO.IdSociedad = IdSociedad;
            int respuesta = oVehiculoDAO.UpdateInsertVehiculo(oVehiculoDTO, ref mensaje_error);

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
            string mensaje_error = "";
            VehiculoDAO oVehiculoDAO = new VehiculoDAO();
            int resultado = oVehiculoDAO.Delete(IdVehiculo, ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

    }
}
