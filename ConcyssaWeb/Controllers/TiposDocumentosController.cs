using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class TiposDocumentosController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerTiposDocumentos(int estado = 3)
        {
            string mensaje_error = "";
            TiposDocumentosDAO oTiposDocumentosDAO = new TiposDocumentosDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<TiposDocumentosDTO> lstTiposDocumentosDTO = oTiposDocumentosDAO.ObtenerTiposDocumentos(IdSociedad, ref mensaje_error, estado);
            if (lstTiposDocumentosDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstTiposDocumentosDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdTipoDocumento)
        {
            string mensaje_error = "";
            TiposDocumentosDAO oTiposDocumentosDAO = new TiposDocumentosDAO();
            List<TiposDocumentosDTO> lstCodigoUbsoDTO = oTiposDocumentosDAO.ObtenerDatosxID(IdTipoDocumento, ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertTiposDocumentos(TiposDocumentosDTO oTiposDocumentosDTO)
        {

            string mensaje_error = "";
            TiposDocumentosDAO oTiposDocumentosDAO = new TiposDocumentosDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oTiposDocumentosDTO.IdSociedad = IdSociedad;
            int respuesta = oTiposDocumentosDAO.UpdateInsertTiposDocumentos(oTiposDocumentosDTO, ref mensaje_error);

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



        public int EliminarTipoDocumento(int IdTipoDocumento)
        {
            string mensaje_error = "";
            TiposDocumentosDAO oTiposDocumentosDAO = new TiposDocumentosDAO();
            int resultado = oTiposDocumentosDAO.Delete(IdTipoDocumento, ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
    }
}
