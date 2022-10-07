using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class CodigoUbsoController : Controller
    {
        public IActionResult Listado ()
        {
            return View();
        }

        public string ObtenerCodigoUbso(int estado=3)
        {
            string mensaje_error = "";
            CodigoUbsoDAO oCodigoUbsoDAO = new CodigoUbsoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<CodigoUbsoDTO> lstCodigoUbsoDTO  = oCodigoUbsoDAO.ObtenerCodigoUbso(IdSociedad, ref mensaje_error,estado);
            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string listadocodigoubsodt(int estado = 3)
        {
            DataTableDTO oDataTableDTO = new DataTableDTO();
            string mensaje_error = "";
            CodigoUbsoDAO oCodigoUbsoDAO = new CodigoUbsoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<CodigoUbsoDTO> lstCodigoUbsoDTO = oCodigoUbsoDAO.ObtenerCodigoUbso(IdSociedad, ref mensaje_error, estado);
            if (lstCodigoUbsoDTO.Count > 0)
            {
                oDataTableDTO.aaData=@JsonConvert.SerializeObject(lstCodigoUbsoDTO);

                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalRecords = lstCodigoUbsoDTO.Count;
                oDataTableDTO.iTotalDisplayRecords = lstCodigoUbsoDTO.Count;
                return JsonConvert.SerializeObject(oDataTableDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerDatosxID(int IdCodigoUbso)
        {
            string mensaje_error = "";
            CodigoUbsoDAO oCodigoUbsoDAO = new CodigoUbsoDAO();
            List<CodigoUbsoDTO> lstCodigoUbsoDTO = oCodigoUbsoDAO.ObtenerDatosxID(IdCodigoUbso,ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }

        }

        

        public string UpdateInsertCodigoUbso(CodigoUbsoDTO oCodigoUbsoDTO)
        {

            string mensaje_error = "";
            CodigoUbsoDAO oCodigoUbsoDAO = new CodigoUbsoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oCodigoUbsoDTO.IdSociedad = IdSociedad;
            int respuesta = oCodigoUbsoDAO.UpdateInsertCodigoUbso(oCodigoUbsoDTO, ref mensaje_error, Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")));

            if (mensaje_error.Length>0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta==1)
                {
                    return "1";
                }
                else
                {
                    return "error";
                }
            }

        }


        public string EliminarCodigoUbso(int IdCodigoUbso)
        {
            string mensaje_error = "";
            CodigoUbsoDAO oCodigoUbsoDAO = new CodigoUbsoDAO();
            string resultado = oCodigoUbsoDAO.Delete(IdCodigoUbso, ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }

            if (resultado == "0")
            {
                resultado = "1";
            }

            return resultado;
        }


    }
}
