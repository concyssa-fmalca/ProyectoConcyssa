using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class GrupoArticuloController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerGrupoArticulo(int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            GrupoArticuloDAO oGrupoArticuloDAO = new GrupoArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<GrupoArticuloDTO> lstGrupoArticuloDTO = oGrupoArticuloDAO.ObtenerGrupoArticulo(IdSociedad,BaseDatos,ref mensaje_error, estado);
            if (lstGrupoArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstGrupoArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdGrupoArticulo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            GrupoArticuloDAO oGrupoArticuloDAO = new GrupoArticuloDAO();
            List<GrupoArticuloDTO> lstCodigoUbsoDTO = oGrupoArticuloDAO.ObtenerDatosxID(IdGrupoArticulo,BaseDatos,ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }

        }



        public string UpdateInsertGrupoArticulo(GrupoArticuloDTO oGrupoArticuloDTO)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            GrupoArticuloDAO oGrupoArticuloDAO = new GrupoArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oGrupoArticuloDTO.IdSociedad = IdSociedad;
            int respuesta = oGrupoArticuloDAO.UpdateInsertGrupoArticulo(oGrupoArticuloDTO,BaseDatos,ref mensaje_error);

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

        public int EliminarGrupoArticulo(int IdGrupoArticulo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            GrupoArticuloDAO oGrupoArticuloDAO = new GrupoArticuloDAO();
            int resultado = oGrupoArticuloDAO.Delete(IdGrupoArticulo,BaseDatos,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
    }
}
