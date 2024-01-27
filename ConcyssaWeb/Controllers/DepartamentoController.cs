using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class DepartamentoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string ObtenerDepartamentos()
        {
            DepartamentoDAO oDepartamentoDAO = new DepartamentoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<DepartamentoDTO> lstDepartamentoDTO = oDepartamentoDAO.ObtenerDepartamentos(IdSociedad.ToString(),BaseDatos);
            if (lstDepartamentoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstDepartamentoDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertDepartamento(DepartamentoDTO departamentoDTO)
        {

            DepartamentoDAO oDepartamentoDAO = new DepartamentoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int resultado = oDepartamentoDAO.UpdateInsertDepartamento(departamentoDTO, IdSociedad.ToString(),BaseDatos);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdDepartamento)
        {
            DepartamentoDAO oDepartamentoDAO = new DepartamentoDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<DepartamentoDTO> lstDepartamentoDTO = oDepartamentoDAO.ObtenerDatosxID(IdDepartamento,BaseDatos);

            if (lstDepartamentoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstDepartamentoDTO);
            }
            else
            {
                return "error";
            }

        }

        public string ObtenerDepartamentosxUsuario(int IdUsuario)
        {
            string valida = "";
            //valida = validarEmpresaActual();
            //if (valida != "")
            //{
            //    return valida;
            //}
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;


            DepartamentoDAO oDepartamentoDAO = new DepartamentoDAO();
            List<DepartamentoDTO> lstDepartamentoDTO = oDepartamentoDAO.ObtenerDepartamentosxUsuario(IdUsuario,BaseDatos);

            if (lstDepartamentoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstDepartamentoDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarDepartamento(int IdDepartamento)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            DepartamentoDAO oDepartamentoDAO = new DepartamentoDAO();
            int resultado = oDepartamentoDAO.Delete(IdDepartamento,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
    }
}
