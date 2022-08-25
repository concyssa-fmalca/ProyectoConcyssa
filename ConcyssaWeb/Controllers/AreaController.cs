using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class AreaController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerArea(int estado = 3)
        {
            string mensaje_error = "";
            AreaDAO oAreaDAO = new AreaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<AreaDTO> lstAreaDTO = oAreaDAO.ObtenerArea(IdSociedad, ref mensaje_error, estado);
            if (lstAreaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstAreaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdArea)
        {
            string mensaje_error = "";
            AreaDAO oAreaDAO = new AreaDAO();
            List<AreaDTO> lstCodigoUbsoDTO = oAreaDAO.ObtenerDatosxID(IdArea, ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertArea(AreaDTO oAreaDTO)
        {

            string mensaje_error = "";
            AreaDAO oAreaDAO = new AreaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oAreaDTO.IdSociedad = IdSociedad;
            int respuesta = oAreaDAO.UpdateInsertArea(oAreaDTO, ref mensaje_error);

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
