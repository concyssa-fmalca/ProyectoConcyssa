using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class Cargo2Controller : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public string ObtenerCargo(int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CargoDAO oCargoDAO = new CargoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<CargoDTO> lstCargoDTO = oCargoDAO.ObtenerCargo(IdSociedad,BaseDatos,ref mensaje_error, estado);
            if (lstCargoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCargoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdCargo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CargoDAO oCargoDAO = new CargoDAO();
            List<CargoDTO> lstCodigoUbsoDTO = oCargoDAO.ObtenerDatosxID(IdCargo,BaseDatos,ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertCargo(CargoDTO oCargoDTO)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CargoDAO oCargoDAO = new CargoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oCargoDTO.IdSociedad = IdSociedad;
            int respuesta = oCargoDAO.UpdateInsertCargo(oCargoDTO,BaseDatos,ref mensaje_error, Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")));

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

        public int EliminarCargo(int IdCargo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CargoDAO oCargoDAO = new CargoDAO();
            int resultado = oCargoDAO.Delete(IdCargo,BaseDatos,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
    }
}
