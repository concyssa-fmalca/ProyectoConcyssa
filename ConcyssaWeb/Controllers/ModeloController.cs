using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class ModeloController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerModelo(int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ModeloDAO oModeloDAO = new ModeloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ModeloDTO> lstModeloDTO = oModeloDAO.ObtenerModelo(IdSociedad,BaseDatos,ref mensaje_error, estado);
            if (lstModeloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstModeloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdModelo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ModeloDAO oModeloDAO = new ModeloDAO();
            List<ModeloDTO> lstCodigoUbsoDTO = oModeloDAO.ObtenerDatosxID(IdModelo,BaseDatos,ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertModelo(ModeloDTO oModeloDTO)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ModeloDAO oModeloDAO = new ModeloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oModeloDTO.IdSociedad = IdSociedad;
            int respuesta = oModeloDAO.UpdateInsertModelo(oModeloDTO,BaseDatos,ref mensaje_error, Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")));

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


        public int EliminarModelo(int IdModelo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ModeloDAO oModeloDAO = new ModeloDAO();
            int resultado = oModeloDAO.Delete(IdModelo,BaseDatos,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

    }
}
