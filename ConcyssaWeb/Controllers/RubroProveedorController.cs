using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class RubroProveedorController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        
        public string ObtenerRubroProveedor(int estado = 3)
        {
            string mensaje_error = "";
            RubroProveedorDAO oRubroProveedorDAO = new RubroProveedorDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<RubroProveedorDTO> lstRubroProveedorDTO = oRubroProveedorDAO.ObtenerRubroProveedor(IdSociedad, ref mensaje_error, estado);
            if (lstRubroProveedorDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstRubroProveedorDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdRubroProveedor)
        {
            string mensaje_error = "";
            RubroProveedorDAO oRubroProveedorDAO = new RubroProveedorDAO();
            List<RubroProveedorDTO> lstCodigoUbsoDTO = oRubroProveedorDAO.ObtenerDatosxID(IdRubroProveedor, ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertRubroProveedor(RubroProveedorDTO oRubroProveedorDTO)
        {

            string mensaje_error = "";
            RubroProveedorDAO oRubroProveedorDAO = new RubroProveedorDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oRubroProveedorDTO.IdSociedad = IdSociedad;
            int respuesta = oRubroProveedorDAO.UpdateInsertRubroProveedor(oRubroProveedorDTO, ref mensaje_error);

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
