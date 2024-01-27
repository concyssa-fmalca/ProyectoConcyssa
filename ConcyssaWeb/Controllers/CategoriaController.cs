using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerCategoria(int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CategoriaDAO oCategoriaDAO = new CategoriaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<CategoriaDTO> lstCategoriaDTO = oCategoriaDAO.ObtenerCategoria(IdSociedad,BaseDatos,ref mensaje_error, estado);
            if (lstCategoriaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCategoriaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdCategoria)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CategoriaDAO oCategoriaDAO = new CategoriaDAO();
            List<CategoriaDTO> lstCodigoUbsoDTO = oCategoriaDAO.ObtenerDatosxID(IdCategoria,BaseDatos,ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertCategoria(CategoriaDTO oCategoriaDTO)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CategoriaDAO oCategoriaDAO = new CategoriaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oCategoriaDTO.IdSociedad = IdSociedad;
            int respuesta = oCategoriaDAO.UpdateInsertCategoria(oCategoriaDTO,BaseDatos,ref mensaje_error, Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")));

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

        public int EliminarCategoria(int IdCategoria)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CategoriaDAO oCategoriaDAO = new CategoriaDAO();
            int resultado = oCategoriaDAO.Delete(IdCategoria,BaseDatos,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
    }
}
