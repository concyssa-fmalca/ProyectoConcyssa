using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class EmpleadoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerEmpleados()
        {
            EmpleadoDAO oEmpleadoDAO = new EmpleadoDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<EmpleadoDTO> lstEmpleadoDTO = oEmpleadoDAO.ObtenerEmpleados(IdSociedad.ToString(),BaseDatos);
            if (lstEmpleadoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstEmpleadoDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertEmpleado(EmpleadoDTO empleadoDTO)
        {

            EmpleadoDAO oEmpleadoDAO = new EmpleadoDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int resultado = oEmpleadoDAO.UpdateInsertEmpleado(empleadoDTO, IdSociedad.ToString(), Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")),BaseDatos);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdEmpleado)
        {
            EmpleadoDAO oEmpleadoDAO = new EmpleadoDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<EmpleadoDTO> lstEmpleadoDTO = oEmpleadoDAO.ObtenerDatosxID(IdEmpleado,BaseDatos);

            if (lstEmpleadoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstEmpleadoDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarEmpleado(int IdEmpleado)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            EmpleadoDAO oEmpleadoDAO = new EmpleadoDAO();
            int resultado = oEmpleadoDAO.Delete(IdEmpleado,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


        public string ObtenerEmpleadosxIdCuadrilla(int IdCuadrilla)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            EmpleadoDAO oEmpleadoDAO = new EmpleadoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<EmpleadoDTO> lstEmpleadoDTO = oEmpleadoDAO.ObtenerEmpleadosxIdCuadrilla(IdCuadrilla,BaseDatos);
            if (lstEmpleadoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstEmpleadoDTO);
            }
            else
            {
                return "error";
            }
        }
        public string ObtenerEmpleadosPorUsuarioBase()
        {
            EmpleadoDAO oEmpleadoDAO = new EmpleadoDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<EmpleadoDTO> lstEmpleadoDTO = oEmpleadoDAO.ObtenerEmpleadosPorUsuarioBase(IdUsuario,BaseDatos);
            if (lstEmpleadoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstEmpleadoDTO);
            }
            else
            {
                return "error";
            }
        }
        public string ObtenerCapatazXCuadrilla(int IdCuadrilla)
        {
            EmpleadoDAO oEmpleadoDAO = new EmpleadoDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<EmpleadoDTO> lstEmpleadoDTO = oEmpleadoDAO.ObtenerCapatazXCuadrilla(IdCuadrilla,BaseDatos);
            if (lstEmpleadoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstEmpleadoDTO);
            }
            else
            {
                return "error";
            }
        }

    }
}
