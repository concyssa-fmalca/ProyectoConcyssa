using DAO;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConcyssaWeb.Controllers
{
    public class TranferenciaStockFinalController : Controller
    {
        // GET: TranferenciaStockFinalController
        public ActionResult Listado()
        {
            return View();
        }

        public string UpdateInsertMovimientoFinal(MovimientoDTO oMovimientoDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            int ValidarSoloSalida = 0;

            if (oMovimientoDTO.IdSociedad == 0 && oMovimientoDTO.IdUsuario == 0)
            {
                int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
                int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
                oMovimientoDTO.IdSociedad = IdSociedad;
                oMovimientoDTO.IdUsuario = IdUsuario;
            }

            
            MovimientoDAO oMovimimientoDAO = new MovimientoDAO();
            int IdMovimientoInicial = oMovimientoDTO.IdMovimiento;
            oMovimientoDTO.IdMovimiento = 0;
            int respuesta = oMovimimientoDAO.RegistrarMovimientoCompleto(oMovimientoDTO, oMovimientoDTO.detalles[0].ValidarIngresoSalidaOAmbos, BaseDatos,ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {
                var rpt = oMovimimientoDAO.ActualziarJsonTranferencia(oMovimientoDTO, IdMovimientoInicial,BaseDatos,ref mensaje_error);
                //int respuesta = 0;
               

                

            }

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta > 0)
                {
                    return "1";
                }
                else
                {
                    return mensaje_error;
                }
            }
        }

    }
}
