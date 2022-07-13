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
            List<DepartamentoDTO> lstDepartamentoDTO = oDepartamentoDAO.ObtenerDepartamentos(IdSociedad.ToString());
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
            int resultado = oDepartamentoDAO.UpdateInsertDepartamento(departamentoDTO, IdSociedad.ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdDepartamento)
        {
            DepartamentoDAO oDepartamentoDAO = new DepartamentoDAO();
            List<DepartamentoDTO> lstDepartamentoDTO = oDepartamentoDAO.ObtenerDatosxID(IdDepartamento);

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
            DepartamentoDAO oDepartamentoDAO = new DepartamentoDAO();
            int resultado = oDepartamentoDAO.Delete(IdDepartamento);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
    }
}
