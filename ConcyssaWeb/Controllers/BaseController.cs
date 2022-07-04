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
            List<BaseDTO> lstCodigoUbsoDTO = oCodigoUbsoDAO.ObtenerDatosxID(IdBase, ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }

        }



        public string UpdateInsertCodigoUbso(CodigoUbsoDTO oCodigoUbsoDTO)
        {

            string mensaje_error = "";
            CodigoUbsoDAO oCodigoUbsoDAO = new CodigoUbsoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oCodigoUbsoDTO.IdSociedad = IdSociedad;
            int respuesta = oCodigoUbsoDAO.UpdateInsertCodigoUbso(oCodigoUbsoDTO, ref mensaje_error);

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
