using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class IndicadorPeriodoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerIndicadorPeriodoDT(int Estado=3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PeriodoDAO oPeriodoDAO = new PeriodoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<IndicadorPeriodoDTO> lstIndicadorPeriodoDTO = oPeriodoDAO.ObtenerIndicadorPeriodo(IdSociedad, Estado,BaseDatos,ref mensaje_error);
            if (lstIndicadorPeriodoDTO.Count >= 0 && mensaje_error.Length == 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstIndicadorPeriodoDTO.Count;
                oDataTableDTO.iTotalRecords = lstIndicadorPeriodoDTO.Count;
                oDataTableDTO.aaData = (lstIndicadorPeriodoDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerIndicadorPeriodo(int Estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PeriodoDAO oPeriodoDAO = new PeriodoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
        
            List<IndicadorPeriodoDTO> lstIndicadorPeriodoDTO = oPeriodoDAO.ObtenerIndicadorPeriodo(IdSociedad, Estado,BaseDatos,ref mensaje_error);
            if (lstIndicadorPeriodoDTO.Count >= 0 && mensaje_error.Length == 0)
            {
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(lstIndicadorPeriodoDTO);

            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerIndicadorPeriodoxId(int IdIndicadorPeriodo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PeriodoDAO oPeriodoDAO = new PeriodoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            IndicadorPeriodoDTO oIndicadorPeriodoDTO = oPeriodoDAO.ObtenerIndicadorPeriodoxId(IdIndicadorPeriodo,BaseDatos,ref mensaje_error);
            if (mensaje_error.Length>0)
            {
                return mensaje_error;

            }
            return JsonConvert.SerializeObject(oIndicadorPeriodoDTO);
        }


        
        public string UpdateInsertIndicadorPeriodo(IndicadorPeriodoDTO oIndicadorPeriodoDTO)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32((String.IsNullOrEmpty(oIndicadorPeriodoDTO.IdSociedad.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oIndicadorPeriodoDTO.IdSociedad);
            int IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(oIndicadorPeriodoDTO.IdUsuario.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")) : oIndicadorPeriodoDTO.IdUsuario);
            if (IdSociedad == 0)
            {
                IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            }
            if (IdUsuario == 0)
            {
                IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            }
            oIndicadorPeriodoDTO.IdSociedad = IdSociedad;
            oIndicadorPeriodoDTO.IdUsuario = IdUsuario;
            PeriodoDAO oPeriodoDAO = new PeriodoDAO();
 
            int respuesta = oPeriodoDAO.UpdateInsertIndicadorPeriodo(oIndicadorPeriodoDTO,BaseDatos,ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta > 0)
                {
                    return respuesta.ToString();
                }
            }
            return respuesta.ToString();

        }

        public string EliminarInidicadorPeriodo(int IdIndicadorPeriodo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            PeriodoDAO oPeriodoDAO = new PeriodoDAO();
            int resultado = oPeriodoDAO.DeleteIndicadorPeriodo(IdIndicadorPeriodo,BaseDatos,ref mensaje_error);
            if (mensaje_error.Length>0)
            {
                return mensaje_error;
            }
            return resultado.ToString();
        }

    }
}
