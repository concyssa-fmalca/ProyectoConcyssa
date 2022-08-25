using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class AlmacenController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerAlmacen(int estado = 3)
        {
            string mensaje_error = "";
            AlmacenDAO oAlmacenDAO = new AlmacenDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<AlmacenDTO> lstAlmacenDTO = oAlmacenDAO.ObtenerAlmacen(IdSociedad, ref mensaje_error, estado);
            if (lstAlmacenDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstAlmacenDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdAlmacen)
        {
            string mensaje_error = "";
            AlmacenDAO oAlmacenDAO = new AlmacenDAO();
            List<AlmacenDTO> lstCodigoUbsoDTO = oAlmacenDAO.ObtenerDatosxID(IdAlmacen, ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertAlmacen(AlmacenDTO oAlmacenDTO)
        {

            string mensaje_error = "";
            AlmacenDAO oAlmacenDAO = new AlmacenDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oAlmacenDTO.IdSociedad = IdSociedad;
            int respuesta = oAlmacenDAO.UpdateInsertAlmacen(oAlmacenDTO, ref mensaje_error);

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

        public int EliminarAlmacen(int IdAlmacen)
        {
            string mensaje_error="";
            AlmacenDAO oAlmacenDAO = new AlmacenDAO();
            int resultado = oAlmacenDAO.Delete(IdAlmacen,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public string ObtenerAlmacenxIdObra(int IdObra)
        {
            string mensaje_error = "";
            AlmacenDAO oAlmacenDAO = new AlmacenDAO();
            List<AlmacenDTO> lstAlmacenDTO = oAlmacenDAO.ObtenerAlmacenxIdObra(IdObra, ref mensaje_error);

            if (lstAlmacenDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstAlmacenDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
    }
}
