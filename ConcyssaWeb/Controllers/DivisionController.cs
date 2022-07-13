using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class DivisionController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerDivision(int estado = 3)
        {
            string mensaje_error = "";
            DivisionDAO oDivisionDAO = new DivisionDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<DivisionDTO> lstDivisionDTO = oDivisionDAO.ObtenerDivision(IdSociedad, ref mensaje_error, estado);
            if (lstDivisionDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstDivisionDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdDivision)
        {
            string mensaje_error = "";
            DivisionDAO oDivisionDAO = new DivisionDAO();
            List<DivisionDTO> lstCodigoUbsoDTO = oDivisionDAO.ObtenerDatosxID(IdDivision, ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertDivision(DivisionDTO oDivisionDTO)
        {

            string mensaje_error = "";
            DivisionDAO oDivisionDAO = new DivisionDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oDivisionDTO.IdSociedad = IdSociedad;
            int respuesta = oDivisionDAO.UpdateInsertDivision(oDivisionDTO, ref mensaje_error);

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
