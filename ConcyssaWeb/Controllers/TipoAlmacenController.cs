using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class TipoAlmacenController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public string ObtenerTipoAlmacen(int estado = 3)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            TipoAlmacenDAO oTipoAlmacenDAO = new TipoAlmacenDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<TipoAlmacenDTO> lstTipoAlmacenDTO = oTipoAlmacenDAO.ObtenerTipoAlmacen(IdSociedad,BaseDatos,ref mensaje_error, estado);
            if (lstTipoAlmacenDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstTipoAlmacenDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdTipoAlmacen)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            TipoAlmacenDAO oTipoAlmacenDAO = new TipoAlmacenDAO();
            List<TipoAlmacenDTO> lstCodigoUbsoDTO = oTipoAlmacenDAO.ObtenerDatosxID(IdTipoAlmacen,BaseDatos,ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertTipoAlmacen(TipoAlmacenDTO oTipoAlmacenDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            TipoAlmacenDAO oTipoAlmacenDAO = new TipoAlmacenDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oTipoAlmacenDTO.IdSociedad = IdSociedad;
            int respuesta = oTipoAlmacenDAO.UpdateInsertTipoAlmacen(oTipoAlmacenDTO,BaseDatos,ref mensaje_error);

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
    }
}
