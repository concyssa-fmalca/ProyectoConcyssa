using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class GlosaContableController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerGlosaContable(int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            GlosaContableDAO oGlosaContableDAO = new GlosaContableDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<GlosaContableDTO> lstGlosaContableDTO = oGlosaContableDAO.ObtenerGlosaContable(IdSociedad,BaseDatos,ref mensaje_error, estado);
            if (lstGlosaContableDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstGlosaContableDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ObtenerGlosaContableDivision(int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            GlosaContableDAO oGlosaContableDAO = new GlosaContableDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
           
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ObtenerBasesxIdUsuario(IdUsuario,BaseDatos,ref mensaje_error);

            UsuarioDTO usuarioDTO = lstUsuarioDTO[0];


            List<GlosaContableDTO> lstGlosaContableDTO = oGlosaContableDAO.ObtenerGlosaContableDivision(IdSociedad, usuarioDTO.IdBase,BaseDatos,ref mensaje_error, estado);
            if (lstGlosaContableDTO.Count > 0 || mensaje_error=="")
            {
                return JsonConvert.SerializeObject(lstGlosaContableDTO);
            }
            else
            {
                
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdGlosaContable)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            GlosaContableDAO oGlosaContableDAO = new GlosaContableDAO();
            List<GlosaContableDTO> lstGlosaContableDTO = oGlosaContableDAO.ObtenerDatosxID(IdGlosaContable,BaseDatos,ref mensaje_error);

            if (lstGlosaContableDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstGlosaContableDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertGlosaContable(GlosaContableDTO oGlosaContableDTO)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            GlosaContableDAO oGlosaContableDAO = new GlosaContableDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oGlosaContableDTO.IdSociedad = IdSociedad;
            int respuesta = oGlosaContableDAO.UpdateInsertGlosaContable(oGlosaContableDTO,BaseDatos,ref mensaje_error);

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


        public int EliminarGlosaContable(int IdGlosaContable)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            GlosaContableDAO oGlosaContableDAO = new GlosaContableDAO();
            int resultado = oGlosaContableDAO.Delete(IdGlosaContable,BaseDatos,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


    }
}
