using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class GrupoUnidadMedidaController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }


        public string ObtenerDefinicionUnidadMedidaxGrupo(int IdGrupoUnidadMedida)
        {
            string mensaje_error = "";
            DefinicionGrupoUnidadDAO oDefinicionGrupoUnidadDAO = new DefinicionGrupoUnidadDAO();
            List<DefinicionGrupoUnidadDTO> lstDefinicionGrupoUnidadDTO = oDefinicionGrupoUnidadDAO.ObtenerDefinicionesxIdGrupoUnidadMedida(IdGrupoUnidadMedida, ref mensaje_error);

            if (lstDefinicionGrupoUnidadDTO.Count > 0)
            {

                return JsonConvert.SerializeObject(lstDefinicionGrupoUnidadDTO);
            }
            else
            {
                return mensaje_error;
            }
            return "error";
        }

        public string ObtenerDatosxID(int IdGrupoUnidadMedida)
        {
            string mensaje_error = "";
            GrupoUnidadMedidaDAO oGrupoUnidadMedidaDAO = new GrupoUnidadMedidaDAO();
            DefinicionGrupoUnidadDAO oDefinicionGrupoUnidadDAO = new DefinicionGrupoUnidadDAO();

            List<GrupoUnidadMedidaDTO> lstGrupoUnidadMedidaDTO = oGrupoUnidadMedidaDAO.ObtenerDatosxID(IdGrupoUnidadMedida, ref mensaje_error);
            List<DefinicionGrupoUnidadDTO> lstDefinicionGrupoUnidadDTO = oDefinicionGrupoUnidadDAO.ObtenerDefinicionesxIdGrupoUnidadMedida(IdGrupoUnidadMedida, ref mensaje_error);

            string[] array1 = new string[2];
            if (lstGrupoUnidadMedidaDTO.Count > 0)
            {
                array1[0] = JsonConvert.SerializeObject(lstGrupoUnidadMedidaDTO);
                array1[1] = JsonConvert.SerializeObject(lstDefinicionGrupoUnidadDTO);

                return JsonConvert.SerializeObject(array1);
                //return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerGrupoUnidadMedida(int estado = 3)
        {
            string mensaje_error = "";
            GrupoUnidadMedidaDAO oGrupoUnidadMedidaDAO = new GrupoUnidadMedidaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<GrupoUnidadMedidaDTO> lstGrupoUnidadMedidaDTO = oGrupoUnidadMedidaDAO.ObtenerGrupoUnidadMedida(IdSociedad, ref mensaje_error, estado);
            if (lstGrupoUnidadMedidaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstGrupoUnidadMedidaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string UpdateInsertGrupoUnidadMedida(GrupoUnidadMedidaDTO oGrupoUnidadMedidaDTO,List<DefinicionGrupoUnidadDTO> lstDefinicionGrupoUnidadDTO)
        {
            //Cabecera
            string mensaje_error = "";
            GrupoUnidadMedidaDAO oGrupoUnidadMedidaDAO = new GrupoUnidadMedidaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oGrupoUnidadMedidaDTO.IdSociedad = IdSociedad;
            int respuesta = oGrupoUnidadMedidaDAO.UpdateInsertGrupoUnidadMedida(oGrupoUnidadMedidaDTO, ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }

            //END CABECERA

            //DETALLE
            DefinicionGrupoUnidadDAO oDefinicionGrupoUnidadDAO = new DefinicionGrupoUnidadDAO();
            for (int i = 0; i < lstDefinicionGrupoUnidadDTO.Count; i++)
            {
                lstDefinicionGrupoUnidadDTO[i].IdGrupoUnidadMedida = respuesta;
                int respuesta1 = oDefinicionGrupoUnidadDAO.UpdateInsertDefinicionGrupoUnidad(lstDefinicionGrupoUnidadDTO[i], ref mensaje_error);
            }
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                return "1";
            }
            //END DETALLE
        }


        
        public string ListarDefinicionGrupoxIdDefinicionSelect(int IdDefinicionGrupo)
        {
            string mensaje_error = "";
            DefinicionGrupoUnidadDAO oDefinicionGrupoUnidadDAO = new DefinicionGrupoUnidadDAO();
            List<DefinicionGrupoUnidadDTO> lstDefinicionGrupoUnidadDTO = oDefinicionGrupoUnidadDAO.ListarDefinicionGrupoxIdDefinicionSelect(IdDefinicionGrupo, ref mensaje_error);

            if (lstDefinicionGrupoUnidadDTO.Count > 0)
            {

                return JsonConvert.SerializeObject(lstDefinicionGrupoUnidadDTO);
            }
            else
            {
                return mensaje_error;
            }
            return "error";
        }
    }
}
