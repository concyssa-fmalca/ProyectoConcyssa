using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class MovimientosController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }


        public string ObtenerDatosxIdMovimiento(int IdMovimiento)
        {
            string mensaje_error = "";
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            MovimientoDTO oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosDetallexIdMovimiento(IdMovimiento, ref mensaje_error);

            if (mensaje_error.ToString().Length == 0 )
            {
                return JsonConvert.SerializeObject(oMovimientoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerMovimientosIngresos(int Estado=3)
        {
            string mensaje_error = "";
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List <MovimientoDTO> oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosIngresos(IdSociedad, ref mensaje_error, Estado);
            if (mensaje_error.ToString().Length == 0)
            {
                return JsonConvert.SerializeObject(oMovimientoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerMovimientosTranferencias(int Estado = 3)
        {
            string mensaje_error = "";
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<MovimientoDTO> oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosTransferencias(IdSociedad, ref mensaje_error, Estado);
            if (mensaje_error.ToString().Length == 0)
            {
                return JsonConvert.SerializeObject(oMovimientoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerMovimientosSalida(int Estado = 3)
        {
            string mensaje_error = "";
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<MovimientoDTO> oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosSalida(IdSociedad, ref mensaje_error, Estado);
            if (mensaje_error.ToString().Length == 0)
            {
                return JsonConvert.SerializeObject(oMovimientoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



    }
}
