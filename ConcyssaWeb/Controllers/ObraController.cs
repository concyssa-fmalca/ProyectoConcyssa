using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class ObraController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public string ObtenerObra(int estado = 3)
        {
            string mensaje_error = "";
            ObraDAO oObraDAO = new ObraDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ObraDTO> lstObraDTO = oObraDAO.ObtenerObra(IdSociedad, ref mensaje_error, estado);
            if (lstObraDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstObraDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdObra)
        {
            string mensaje_error = "";
            ObraDAO oObraDAO = new ObraDAO();
            List<ObraDTO> lstCodigoUbsoDTO = oObraDAO.ObtenerDatosxID(IdObra, ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string UpdateInsertObra(ObraDTO oObraDTO)
        {

            string mensaje_error = "";
            ObraDAO oObraDAO = new ObraDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oObraDTO.IdSociedad = IdSociedad;
            int respuesta = oObraDAO.UpdateInsertObra(oObraDTO, ref mensaje_error);

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

        public string UpdateInsertObraCatalogoProducto(List<ObraCatalogoDTO> detalles)
        {
            string mensaje_error = "";
            ObraDAO oObraDAO = new ObraDAO();
            for (int i = 0; i < detalles.Count(); i++)
            {
                int respuesta = oObraDAO.UpdateInsertObraCatalogoProducto(detalles[i], ref mensaje_error);
            }

            if (mensaje_error.Length>0)
            {

                return mensaje_error;
            }
            else
            {
                return "1";
            }
        


        }

        public int EliminarObra(int IdObra)
        {
            string mensaje_error = "";
            ObraDAO oObraDAO = new ObraDAO();
            int resultado = oObraDAO.Delete(IdObra, ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


        public string CargarCatalogoxIdObra(int IdObra)
        {
            string mensaje_error = "";
            ObraDAO oObraDAO = new ObraDAO();
            DataTableDTO oDataTableDTO = new DataTableDTO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ObraCatalogoDTO> lstObraDTO = oObraDAO.ListarArticulosxIdSociedadObra(IdSociedad, IdObra, ref mensaje_error);
            if (lstObraDTO.Count > 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstObraDTO.Count;
                oDataTableDTO.iTotalRecords = lstObraDTO.Count;
                oDataTableDTO.aaData = (lstObraDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);


                //return JsonConvert.SerializeObject(lstObraDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerObraxIdBase(int IdBase)
        {
            string mensaje_error = "";
            ObraDAO oObraDAO = new ObraDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ObraDTO> lstObraDTO = oObraDAO.ObtenerObraxIdBase(IdBase, ref mensaje_error);
            if (lstObraDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstObraDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
    }
}
