using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class IndicadorImpuestoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerIndicadorImpuestos(int estado = 3)
        {
            string mensaje_error = "";
            IndicadorImpuestoDAO oIndicadorImpuestoDAO = new IndicadorImpuestoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<IndicadorImpuestoDTO> lstIndicadorImpuestoDTO = oIndicadorImpuestoDAO.ObtenerIndicadorImpuestos(IdSociedad, ref mensaje_error, estado);
            if (lstIndicadorImpuestoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstIndicadorImpuestoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public int UpdateInsertIndicadorImpuesto(IndicadorImpuestoDTO IndicadorImpuestoDTO)
        {
            string mensaje_error = "";
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            IndicadorImpuestoDAO oIndicadorImpuestoDAO = new IndicadorImpuestoDAO();
            IndicadorImpuestoDTO.IdSociedad = IdSociedad;
            int resultado = oIndicadorImpuestoDAO.UpdateInsertIndicadorImpuesto(IndicadorImpuestoDTO,ref mensaje_error);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdIndicadorImpuesto)
        {
            string mensaje_error = "";
            IndicadorImpuestoDAO oIndicadorImpuestoDAO = new IndicadorImpuestoDAO();
            List<IndicadorImpuestoDTO> lstIndicadorImpuestoDTO = oIndicadorImpuestoDAO.ObtenerDatosxID(IdIndicadorImpuesto, ref mensaje_error);

            if (lstIndicadorImpuestoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstIndicadorImpuestoDTO);
            }
            else
            {
                return mensaje_error;
            }

        }

        public int EliminarIndicadorImpuesto(int IdIndicadorImpuesto)
        {
            IndicadorImpuestoDAO oIndicadorImpuestoDAO = new IndicadorImpuestoDAO();
            int resultado = oIndicadorImpuestoDAO.Delete(IdIndicadorImpuesto);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


    }
}
