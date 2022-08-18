using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class CentroCostoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerCentroCosto(int estado = 3)
        {
            string mensaje_error = "";
            CentroCostoDAO oCentroCostoDAO = new CentroCostoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<CentroCostoDTO> lstCentroCostoDTO = oCentroCostoDAO.ObtenerCentroCosto(IdSociedad, ref mensaje_error, estado);
            if (lstCentroCostoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCentroCostoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdCentroCosto)
        {
            string mensaje_error = "";
            CentroCostoDAO oCentroCostoDAO = new CentroCostoDAO();
            List<CentroCostoDTO> lstCodigoUbsoDTO = oCentroCostoDAO.ObtenerDatosxID(IdCentroCosto, ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertCentroCosto(CentroCostoDTO oCentroCostoDTO)
        {

            string mensaje_error = "";
            CentroCostoDAO oCentroCostoDAO = new CentroCostoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oCentroCostoDTO.IdSociedad = IdSociedad;
            int respuesta = oCentroCostoDAO.UpdateInsertCentroCosto(oCentroCostoDTO, ref mensaje_error);

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

        public int EliminarCentroCosto(int IdCentroCosto)
        {
            string mensaje_error = "";
            CentroCostoDAO oCentroCostoDAO = new CentroCostoDAO();
            int resultado = oCentroCostoDAO.Delete(IdCentroCosto, ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
    }
}
