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
            BaseDAO oBaseDAO = new BaseDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<BaseDTO> lstBaseDTO = oBaseDAO.ObtenerBase(IdSociedad, ref mensaje_error, estado);
            if (lstBaseDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstBaseDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdBase)
        {
            string mensaje_error = "";
            BaseDAO oBaseDAO = new BaseDAO();
            List<BaseDTO> lstCodigoUbsoDTO = oBaseDAO.ObtenerDatosxID(IdBase, ref mensaje_error);

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
            BaseDAO oBaseDAO = new BaseDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oBaseDTO.IdSociedad = IdSociedad;
            int respuesta = oBaseDAO.UpdateInsertBase(oBaseDTO, ref mensaje_error, Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")));

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
            BaseDAO oBaseDAO = new BaseDAO();
            int resultado = oBaseDAO.Delete(IdBase, ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
    }
}
