using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class NumeracionDocumentoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerNumeracion(int estado = 3, int IdBase = 3)
        {
            string mensaje_error = "";
            NumeracionDocumentoDAO oNumeracionDocumentoDAO = new NumeracionDocumentoDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            if (IdBase == 1)
            {
                UsuarioDAO oUsuarioDAO = new UsuarioDAO();
                List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ObtenerBasesxIdUsuario(IdUsuario, ref mensaje_error);
                UsuarioDTO usuarioDTO = lstUsuarioDTO[0];
                IdBase = usuarioDTO.IdBase;   
            }        
            List<NumeracionDocumentoDTO> lstGlosaContableDTO = oNumeracionDocumentoDAO.Numeracion( IdBase, ref mensaje_error, estado);
            if (lstGlosaContableDTO.Count > 0 || mensaje_error =="")
            {
                return JsonConvert.SerializeObject(lstGlosaContableDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        

        public string ObtenerDatosxID(int IdNumeracionDocumento)
        {
            string mensaje_error = "";
            NumeracionDocumentoDAO oNumeracionDocumentoDAO = new NumeracionDocumentoDAO();
            List<NumeracionDocumentoDTO> lstGlosaContableDTO = oNumeracionDocumentoDAO.ObtenerDatosxID(IdNumeracionDocumento, ref mensaje_error);

            if (lstGlosaContableDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstGlosaContableDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertNumeracion(NumeracionDocumentoDTO oNumeracionDocumentoDTO)
        {

            string mensaje_error = "";
            NumeracionDocumentoDAO oGlosaContableDAO = new NumeracionDocumentoDAO();
          
            int respuesta = oGlosaContableDAO.UpdateInsertNumeracion(oNumeracionDocumentoDTO, ref mensaje_error);

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


        public int EliminarNumeracion(int IdNumeracionDocumento)
        {
            string mensaje_error = "";
            NumeracionDocumentoDAO oGlosaContableDAO = new NumeracionDocumentoDAO();
            int resultado = oGlosaContableDAO.Delete(IdNumeracionDocumento, ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


    }
}
