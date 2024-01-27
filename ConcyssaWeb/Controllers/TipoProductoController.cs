using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class TipoProductoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public string ObtenerTipoProducto(int estado = 3)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            TipoProductoDAO oTipoProductoDAO = new TipoProductoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<TipoProductoDTO> lstTipoProductoDTO = oTipoProductoDAO.ObtenerTipoProducto(IdSociedad,BaseDatos,ref mensaje_error, estado);
            if (lstTipoProductoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstTipoProductoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdTipoProducto)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            TipoProductoDAO oTipoProductoDAO = new TipoProductoDAO();
            List<TipoProductoDTO> lstCodigoUbsoDTO = oTipoProductoDAO.ObtenerDatosxID(IdTipoProducto,BaseDatos,ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertTipoProducto(TipoProductoDTO oTipoProductoDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            TipoProductoDAO oTipoProductoDAO = new TipoProductoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oTipoProductoDTO.IdSociedad = IdSociedad;
            int respuesta = oTipoProductoDAO.UpdateInsertTipoProducto(oTipoProductoDTO,BaseDatos,ref mensaje_error, Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")));

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

        public int EliminarTipoProducto(int IdTipoProducto)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            TipoProductoDAO oTipoProductoDAO = new TipoProductoDAO();
            int resultado = oTipoProductoDAO.Delete(IdTipoProducto,BaseDatos,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
    }
}
