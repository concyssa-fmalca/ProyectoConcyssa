using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerBase(int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            BaseDAO oBaseDAO = new BaseDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<BaseDTO> lstBaseDTO = oBaseDAO.ObtenerBase(IdSociedad,BaseDatos,ref mensaje_error, estado);
            if (lstBaseDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstBaseDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerBasexIdUsuario( int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            BaseDAO oBaseDAO = new BaseDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int IdPerfil = Convert.ToInt32(HttpContext.Session.GetInt32("IdPerfil"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<BaseDTO> lstAlmacenDTO = oBaseDAO.ObtenerBasexIdUsuario(IdPerfil,IdUsuario,BaseDatos,ref mensaje_error, estado);
            if (lstAlmacenDTO.Count > 0)
            {

                //return oDataTableDTO;
                return JsonConvert.SerializeObject(lstAlmacenDTO);

            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdBase)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            BaseDAO oBaseDAO = new BaseDAO();
            List<BaseDTO> lstCodigoUbsoDTO = oBaseDAO.ObtenerDatosxID(IdBase,BaseDatos,ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }

        }



        public string UpdateInsertBase(BaseDTO oBaseDTO)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            BaseDAO oBaseDAO = new BaseDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oBaseDTO.IdSociedad = IdSociedad;
            int respuesta = oBaseDAO.UpdateInsertBase(oBaseDTO,BaseDatos,ref mensaje_error, Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")));

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

        public int EliminarBase(int IdBase)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            BaseDAO oBaseDAO = new BaseDAO();
            int resultado = oBaseDAO.Delete(IdBase,BaseDatos,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
    }
}
