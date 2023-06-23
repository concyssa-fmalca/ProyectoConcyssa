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
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<EmpleadoDTO> lstEmpleadoDTO = oEmpleadoDAO.ObtenerEmpleados(IdSociedad.ToString());
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
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int resultado = oEmpleadoDAO.UpdateInsertEmpleado(empleadoDTO, IdSociedad.ToString(), Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")));
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdEmpleado)
        {
            EmpleadoDAO oEmpleadoDAO = new EmpleadoDAO();
            List<EmpleadoDTO> lstEmpleadoDTO = oEmpleadoDAO.ObtenerDatosxID(IdEmpleado);

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
            EmpleadoDAO oEmpleadoDAO = new EmpleadoDAO();
            int resultado = oEmpleadoDAO.Delete(IdEmpleado);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


        public string ObtenerEmpleadosxIdCuadrilla(int IdCuadrilla)
        {
            EmpleadoDAO oEmpleadoDAO = new EmpleadoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<EmpleadoDTO> lstEmpleadoDTO = oEmpleadoDAO.ObtenerEmpleadosxIdCuadrilla(IdCuadrilla);
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
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<EmpleadoDTO> lstEmpleadoDTO = oEmpleadoDAO.ObtenerEmpleadosPorUsuarioBase(IdUsuario);
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
      
            List<EmpleadoDTO> lstEmpleadoDTO = oEmpleadoDAO.ObtenerCapatazXCuadrilla(IdCuadrilla);
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
