using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class MarcaController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerMarca(int estado = 3)
        {
            string mensaje_error = "";
            MarcaDAO oMarcaDAO = new MarcaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<MarcaDTO> lstMarcaDTO = oMarcaDAO.ObtenerMarca(IdSociedad, ref mensaje_error, estado);
            if (lstMarcaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstMarcaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdMarca)
        {
            string mensaje_error = "";
            MarcaDAO oMarcaDAO = new MarcaDAO();
            List<MarcaDTO> lstCodigoUbsoDTO = oMarcaDAO.ObtenerDatosxID(IdMarca, ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertMarca(MarcaDTO oMarcaDTO)
        {

            string mensaje_error = "";
            MarcaDAO oMarcaDAO = new MarcaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oMarcaDTO.IdSociedad = IdSociedad;
            int respuesta = oMarcaDAO.UpdateInsertMarca(oMarcaDTO, ref mensaje_error);

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
