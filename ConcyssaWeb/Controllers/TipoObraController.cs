using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class TipoObraController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public string ObtenerTipoObra(int estado = 3)
        {
            string mensaje_error = "";
            TipoObraDAO oTipoObraDAO = new TipoObraDAO();
            DataTableDTO oDataTableDTO = new DataTableDTO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<TipoObraDTO> lstTipoObraDTO = oTipoObraDAO.ObtenerTipoObra(IdSociedad, ref mensaje_error, estado);
            if (lstTipoObraDTO.Count > 0)
            {
 
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(lstTipoObraDTO);

            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerTipoObraDT(int estado = 3)
        {
            string mensaje_error = "";
            TipoObraDAO oTipoObraDAO = new TipoObraDAO();
            DataTableDTO oDataTableDTO = new DataTableDTO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<TipoObraDTO> lstTipoObraDTO = oTipoObraDAO.ObtenerTipoObra(IdSociedad, ref mensaje_error, estado);
            if (lstTipoObraDTO.Count > 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstTipoObraDTO.Count;
                oDataTableDTO.iTotalRecords = lstTipoObraDTO.Count;
                oDataTableDTO.aaData = (lstTipoObraDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerDatosxID(int IdTipoObra)
        {
            string mensaje_error = "";
            TipoObraDAO oTipoObraDAO = new TipoObraDAO();
            List<TipoObraDTO> lstCodigoUbsoDTO = oTipoObraDAO.ObtenerDatosxID(IdTipoObra, ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertTipoObra(TipoObraDTO oTipoObraDTO)
        {

            string mensaje_error = "";
            TipoObraDAO oTipoObraDAO = new TipoObraDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oTipoObraDTO.IdSociedad = IdSociedad;
            int respuesta = oTipoObraDAO.UpdateInsertTipoObra(oTipoObraDTO, ref mensaje_error);

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
